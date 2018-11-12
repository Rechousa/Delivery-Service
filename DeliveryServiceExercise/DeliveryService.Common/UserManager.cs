using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.Common
{
    public class UserManager : IUserManager
    {
        private readonly List<User> _users = new List<User>
        {
            new User("John Doe", "admin", "password", true),
            new User("Mark Down", "basic", "demo", false),
        };

        public AuthenticationResult Authenticate(string login, string password)
        {
            if(!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password))
            {
                var user = _users.SingleOrDefault(t => t.Login == login && t.Password == password);
                if(user != null)
                {
                    user.ProtectsSensibleData();
                    if(user.IsAdmin)
                    {
                        return AuthenticationResult.AdminAuthenticationResult(user);
                    }
                    return AuthenticationResult.BasicAuthenticationResult(user);
                }
            }
            return AuthenticationResult.AuthenticationFailedResult();
        }
    }
}
