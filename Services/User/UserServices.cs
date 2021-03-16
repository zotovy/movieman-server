using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Database.User;
using Domain.ValueObjects.User;
using Metadata.Objects;
using Metadata.Services.UserMetadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Data;

namespace Services.User {
    public class UserServices : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserServices(IUserRepository userRepository, IConfiguration configuration) {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public IEnumerable<Domain.User> GetUsers() {
            var models = _userRepository.GetModels();
            return models.Select(UserModel.ToDomain);
        }

        private string _createToken(TokenType type, long id) {
            var claims = new List<Claim> {
                new(ClaimsIdentity.DefaultNameClaimType, id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType
            );

            // Calculate token life time
            var now = DateTime.UtcNow;
            DateTime expires = type == TokenType.Access
                ? now.Add(TimeSpan.FromMinutes(10))
                : now.Add(TimeSpan.FromDays(365));

            var jwt = new JwtSecurityToken(
                "Movieman",
                "Favourite users",
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:secret"])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public LoginResponse LoginUser(Email email, Password password) {
            // Check is user exists and credentials are fine
            var user = _userRepository.FoundWithSameEmailAndPassword(email, password);
            if (user == null) {
                return new LoginResponse { Success = false };
            }
            
            // Create tokens for this user
            string access = _createToken(TokenType.Access, user.Id);
            string refresh = _createToken(TokenType.Refresh, user.Id);
            
            return new LoginResponse {
                Success = true,
                AuthTokens = new AuthTokens(access, refresh),
                UserId = user.Id,
            };
        }
    }
}