using System.Text.Json;

namespace Firebase.Auth.Requests
{
    public class RefreshTokenRequest
    {
        public string GrantType { get; set; }

        public string RefreshToken { get; set; }
    }

    public class RefreshTokenResponse
    {
        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string IdToken { get; set; }

        public string UserId { get; set; }
    }

    /// <summary>
    /// Refreshes IdToken using a refresh token.
    /// </summary>
    public class RefreshToken : FirebaseRequestBase<RefreshTokenRequest, RefreshTokenResponse>
    {
        private static readonly JsonSerializerOptions SnakeCaseSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public RefreshToken(FirebaseAuthConfig config) : base(config)
        {
        }

        protected override JsonSerializerOptions JsonSettingsOverride => SnakeCaseSettings;

        protected override string UrlFormat => Endpoints.GoogleRefreshAuth;
    }
}
