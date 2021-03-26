using Microsoft.AspNetCore.Http;

namespace API.DTO.User {
    public class ChangeProfileImageDto {
        
        public IFormFile image { get; set; }
    }
    
    public class InvalidProfileImageSizeDto {
        public bool success => false;
        public string error => "invalid-image-size-error";
    }
}