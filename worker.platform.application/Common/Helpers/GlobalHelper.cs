using System.Text;

namespace worker.platform.application.Common.Helpers;

public static class GlobalHelper
{
    public static string GeneratePassword(string email = null)
    {
        const string upperChars = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        const string lowerChars = "abcdefghijkmnopqrstuvwxyz";
        const string numberChars = "23456789";
        const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        const int passwordLength = 12;

        // Create a deterministic but secure seed based on email and current time
        int seed;
        if (!string.IsNullOrEmpty(email))
        {
            // Combine email hash with timestamp for uniqueness
            var emailBytes = System.Text.Encoding.UTF8.GetBytes(email.ToLowerInvariant());
            var hashBytes = System.Security.Cryptography.SHA256.HashData(emailBytes);
            var timeBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            // Combine the two hashes
            for (int i = 0; i < 8 && i < hashBytes.Length; i++)
            {
                timeBytes[i % timeBytes.Length] ^= hashBytes[i];
            }

            seed = BitConverter.ToInt32(timeBytes, 0);
        }
        else
        {
            seed = Environment.TickCount;
        }

        var random = new Random(seed);
        var password = new StringBuilder();

        // Ensure at least one character of each type
        password.Append(upperChars[random.Next(upperChars.Length)]);
        password.Append(lowerChars[random.Next(lowerChars.Length)]);
        password.Append(numberChars[random.Next(numberChars.Length)]);
        password.Append(specialChars[random.Next(specialChars.Length)]);

        // Fill the rest of the password with random characters from all types
        var allChars = upperChars + lowerChars + numberChars + specialChars;
        for (int i = 4; i < passwordLength; i++)
        {
            password.Append(allChars[random.Next(allChars.Length)]);
        }

        // Shuffle the characters to avoid predictable pattern
        return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
    }
}
