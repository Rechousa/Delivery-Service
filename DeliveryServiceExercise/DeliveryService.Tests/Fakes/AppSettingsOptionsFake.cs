using DeliveryService.API.Settings;
using Microsoft.Extensions.Options;

namespace DeliveryService.Tests
{
    public class AppSettingsOptionsFake : IOptions<AppSettings>
    {
        public AppSettings Value => new AppSettings
        {
            JWTAudience = "Audience",
            JWTExpiresInDays = 30,
            JWTIssuer = "Me",
            JWTSecret = "1231231283091283902183901283901289032189",
            RedisKeyTimeToLiveInSeconds = 30
        };
    }
}
