using System;
using System.Threading.Tasks;

namespace Firebase.Auth.Repository
{
    /// <inherit />
    public class InMemoryRepository : IUserRepository
    {
        private static readonly Lazy<InMemoryRepository> lazy = new(() => new InMemoryRepository());

        private User user;

        private InMemoryRepository()
        {
        }

        public static InMemoryRepository Instance => lazy.Value;

        public bool UserExists()
        {
            return this.user != null;
        }

        public (UserInfo, FirebaseCredential) ReadUser()
        {
            return (this.user?.Info, this.user?.Credential);
        }

        public void SaveUser(User user)
        {
            this.user = user;
        }

        public void DeleteUser()
        {
            this.user = null;
        }
    }
}
