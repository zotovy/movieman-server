namespace Metadata.Services.UserMetadata {
    public class SignupResponse {
        public bool Success { get; set; }
        public long UserId { get; set; }
        public SignupResponseError Error { get; set; }

        public SignupResponse(bool success, SignupResponseError error) {
            Success = success;
        }

        public SignupResponse(bool success, long userId) {
            Success = success;
            UserId = userId;
        }
    }

    public enum SignupResponseError {
        EmailUniqueness
    }
}