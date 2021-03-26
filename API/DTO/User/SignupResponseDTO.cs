using Metadata.Objects;

namespace API.DTO.User {
    public class SignupResponseDTO {
        public bool success { get; set; }
        public long id { get; set; }
        public TokensDto tokens { get; set; }

        public SignupResponseDTO(bool success, long userId, AuthTokens tokens) {
            this.success = success;
            this.id = userId;
            this.tokens = new() {
                access = tokens.Access,
                refresh = tokens.Refresh,
            };
        }

        public static BadRequestSignupResponseDTO BadRequest() => new ();
        public static EmailUniquenessErrorSignupResponseDTO EmailError() => new ();
    }

    public sealed record BadRequestSignupResponseDTO {
        public bool success => false;
        public string error => "validation-error";
    }
    
    public sealed record EmailUniquenessErrorSignupResponseDTO {
        public bool success => false;
        public string error => "email-already-exists-error";
    }
}
