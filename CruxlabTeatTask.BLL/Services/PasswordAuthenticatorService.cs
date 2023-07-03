using CruxlabTeatTask.DTO.ViewModels;
using System.Text.RegularExpressions;

namespace CruxlabTeatTask.BLL.Services
{
    public static partial class PasswordAuthenticatorService
    {
        public static PasswordAuthenticationResponseVM AuthenticateFile(string filePath)
        {
            var response = new PasswordAuthenticationResponseVM
            {
                FilePath = filePath,
                FileName = Path.GetFileName(filePath),
            };

            try
            {
                using StreamReader reader = new(filePath);
                string line;
                int checksCount = 0;
                int passedPasswords = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    checksCount++;
                    if (CheckPassword(line))
                    {
                        passedPasswords++;
                    }
                }

                response.ChecksCount = checksCount;
                response.PassedPasswords = passedPasswords;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return response;
        }


        private static bool CheckPassword(string line)
        {
            // Extract the condition and password from the line.
            // I wanted to use line.Substring(0, line.IndexOf(':')).Trim(); for this, but the compiler reminded me that I can use the range operator.
            // So it looks even better this way. XD
            string condition = line[..line.IndexOf(':')].Trim();
            string password = line[(line.IndexOf(':') + 1)..].Trim();

            // Parse the condition to get the letter and the minimum/maximum counts.
            Match match = MyRegex().Match(condition);
            if (match.Success)
            {
                char letter = match.Groups[1].Value[0];
                int minCount = int.Parse(match.Groups[2].Value);
                int maxCount = int.Parse(match.Groups[3].Value);

                // Count the occurrences of the letter in the password
                int letterCount = 0;
                foreach (char c in password)
                {
                    if (c == letter)
                    {
                        letterCount++;
                    }
                }

                // Check if the password meets the condition
                return letterCount >= minCount && letterCount <= maxCount;
            }

            // Invalid condition format, treat as failed password
            return false;
        }


        [GeneratedRegex("(\\w) (\\d+)-(\\d+)")]
        private static partial Regex MyRegex();
    }
}
