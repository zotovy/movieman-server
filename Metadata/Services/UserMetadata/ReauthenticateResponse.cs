
using Metadata.Objects;

namespace Metadata.Services.UserMetadata {
    public sealed record ReauthenticateResponse {
        public bool Success { get; set; }
        public AuthTokens Tokens { get; set; }
    }
}