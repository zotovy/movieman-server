namespace API.DTO.User {
    public class SignupResponseDTO {
        public bool success { get; set; }
        public long userId { get; set; }

        public SignupResponseDTO(bool success, long userId) {
            this.success = success;
            this.userId = userId;
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