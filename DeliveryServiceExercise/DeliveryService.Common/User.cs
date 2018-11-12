using System;

namespace DeliveryService.Common
{
    public class User
    {
        public string Name { get; set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public bool IsAdmin { get; private set; }

        public User(string name, string login, string password, bool isAdmin)
        {
            Name = name;
            Login = login;
            Password = password;
            IsAdmin = isAdmin;
        }

        internal void ProtectsSensibleData()
        {
            Password = null;
        }
    }
}