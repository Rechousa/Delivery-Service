namespace DeliveryService.Common
{
    public interface IUserManager
    {
        AuthenticationResult Authenticate(string login, string password);
    }
}