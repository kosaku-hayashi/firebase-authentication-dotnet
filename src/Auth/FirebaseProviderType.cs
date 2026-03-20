using System.Runtime.Serialization;

namespace Firebase.Auth
{
    /// <summary>
    /// Type of authentication provider.
    /// </summary>
    public enum FirebaseProviderType
    {
        Unknown,
        Facebook,
        Google,
        Github,
        Twitter,
        Microsoft,
        Apple,
        EmailAndPassword,
        Phone,
        Anonymous
    }
}
