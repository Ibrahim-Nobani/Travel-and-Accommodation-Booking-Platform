using TravelBookingPlatform.Infrastructure.Interfaces;
namespace TravelBookingPlatform.Infrastructure.ExternalServices;
using BCrypt.Net;

public class PasswordHashBcryptService : IPasswordHashService
{
    public string HashPassword(string password)
    {
        string salt = BCrypt.GenerateSalt();
        string hashedPassword = BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Verify(password, hashedPassword);
    }
}