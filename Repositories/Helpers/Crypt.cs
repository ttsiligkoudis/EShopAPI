using BCryptNet = BCrypt.Net;

namespace Repositories.Helpers
{
    public static class Crypt
    {
        public static string HashPassword(string password)
        {
            // Generate a random salt
            string salt = BCryptNet.BCrypt.GenerateSalt();

            // Hash the password with the salt
            string hashedPassword = BCryptNet.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hashed password
            bool isPasswordValid = BCryptNet.BCrypt.Verify(password, hashedPassword);

            return isPasswordValid;
        }
    }
}
