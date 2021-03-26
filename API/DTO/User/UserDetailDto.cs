using System.Collections.Generic;
using System.Linq;

namespace API.DTO.User {
    public sealed record UserDetailDto {
        public long id { get; set; } 
        public string name { get; set; } 
        public List<long> movies  { get; set; } 
        public List<long> reviews  { get; set; } 
        public List<long> comments  { get; set; } 
        public string email  { get; set; } 
        public string createdAt  { get; set; } 
        public string profileImagePath  { get; set; }

        public UserDetailDto(Domain.User user) {
            this.id = user.Id;
            this.name = user.Name.Value;
            this.movies = user.Movies.Select(x => x.Id).ToList();
            this.reviews = user.Reviews.Select(x => x.Id).ToList();
            this.comments = user.Comments.Select(x => x.Id).ToList();
            this.email = user.Email.Value;
            this.createdAt = user.CreatedAt.ToUniversalTime().ToString("O");
            this.profileImagePath = user.ProfileImagePath.Value;
        }
    }
}