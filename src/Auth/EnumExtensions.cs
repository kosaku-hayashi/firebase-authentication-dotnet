namespace Firebase.Auth
{
    using System;

    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the string representation of <see cref="FirebaseProviderType"/> as defined
        /// by its <c>EnumMember</c> attribute values. Uses a switch expression to avoid reflection.
        /// </summary>
        public static string ToEnumString(this FirebaseProviderType type) => type switch
        {
            FirebaseProviderType.Facebook       => "facebook.com",
            FirebaseProviderType.Google         => "google.com",
            FirebaseProviderType.Github         => "github.com",
            FirebaseProviderType.Twitter        => "twitter.com",
            FirebaseProviderType.Microsoft      => "microsoft.com",
            FirebaseProviderType.Apple          => "apple.com",
            FirebaseProviderType.EmailAndPassword => "password",
            FirebaseProviderType.Phone          => "phone",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
