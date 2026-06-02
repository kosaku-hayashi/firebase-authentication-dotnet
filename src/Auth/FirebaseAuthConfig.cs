using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Firebase.Auth
{
    /// <summary>
    /// Configuration of firebase auth.
    /// </summary>
    public class FirebaseAuthConfig
    {
        private UserManager userManager;

        public FirebaseAuthConfig(HttpClient httpClient = null)
        {
            this.HttpClient = httpClient ?? new HttpClient();
            this.UserRepository = InMemoryRepository.Instance;
            this.Providers = Array.Empty<FirebaseAuthProvider>();
            this.JsonSettings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            this.JsonSettings.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        /// <summary>
        /// The api key of your Firebase app. 
        /// </summary>
        public string ApiKey
        {
            get;
            set;
        }

        /// <summary>
        /// Repository of firebase tokens. Default is in-memory.
        /// </summary>
        public IUserRepository UserRepository 
        { 
            get;
            set;
        }

        /// <summary>
        /// Collection of auth providers (e.g. Google, Facebook etc.)
        /// </summary>
        public FirebaseAuthProvider[] Providers 
        { 
            get;
            set;
        }

        /// <summary>
        /// System.Text.Json serializer options.
        /// </summary>
        public JsonSerializerOptions JsonSettings 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// HttpClient to be used for web requests.
        /// </summary>
        public HttpClient HttpClient 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Auth domain of your firebase app, e.g. hello.firebaseapp.com
        /// </summary>
        public string AuthDomain
        {
            get;
            set;
        }

        public Func<IEnumerable<(string name,string value)>> CustomHeadersProvider { get; set; }

        /// <summary>
        /// Specifies the uri that oauth provider will navigate to to finish auth.
        /// </summary>
        public string RedirectUri => $"https://{this.AuthDomain}/__/auth/handler";

        public UserManager UserManager => this.userManager ?? (this.userManager = new UserManager(this.UserRepository));

        /// <summary>
        /// Get provider instance for given <paramref name="providerType"/>.
        /// </summary>
        public FirebaseAuthProvider GetAuthProvider(FirebaseProviderType providerType)
        {
            return this.Providers.FirstOrDefault(f => f.ProviderType == providerType)
                ?? throw new InvalidOperationException($"Provider {providerType} is not configured, you need to add it to your FirebaseAuthConfig");
        }
    }
}
