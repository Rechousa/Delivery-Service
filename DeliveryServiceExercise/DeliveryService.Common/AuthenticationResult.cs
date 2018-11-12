namespace DeliveryService.Common
{
    public class AuthenticationResult
    {
        public bool IsSuccessful { get; private set; }

        public User User { get; private set; }

        public bool IsAdmin { get; private set; }

        private AuthenticationResult() { }

        public static AuthenticationResult AuthenticationFailedResult()
        {
            return new AuthenticationResult
            {
                IsSuccessful = false,
                User = null,
                IsAdmin = false
            };
        }

        public static AuthenticationResult AdminAuthenticationResult(User user)
        {
            return new AuthenticationResult
            {
                IsSuccessful = true,
                User = user,
                IsAdmin = true
            };
        }

        public static AuthenticationResult BasicAuthenticationResult(User user)
        {
            return new AuthenticationResult
            {
                IsSuccessful = true,
                User = user,
                IsAdmin = false
            };
        }
    }
}