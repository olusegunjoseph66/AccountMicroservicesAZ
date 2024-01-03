using Fare;
using System.Text;

namespace Shared.Utilities.Helpers
{
    public static class RandomValueGenerator
    {
        private static readonly Random random = new();
        private const string nums = "0123456789";
        private const string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ@#$&%";
        private const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        private const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "123456789";
        private const string SPECIALS = @"!@£$%^&*()#€";
        
        public static string RandomString(int length, bool isNum = false)
        {
            string chars = isNum ? nums : alpha;
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandomDigits(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            int character;
            for (int i = 0; i < size; i++)
            {
                character = random.Next(9);
                builder.Append(character);
            }
            return builder.ToString();
        }

        public static string GenerateRandomCode(int size = 6)
        {
            var builder = new StringBuilder();
            var randomNumber = new Random((int)DateTime.UtcNow.Ticks);

            for (var i = 0; i < size; i++)
            {
                var character = Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 18));
                builder.Append(character);
            }
            return builder.ToString();
        }

        public static string GenerateRandomString(bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSpecial = true,
             int stringSize = 15)
        {
            char[] _password = new char[stringSize];
            string charSet = ""; // Initialise to blank
            Random _random = new();
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;

            if (useUppercase) charSet += UPPER_CAES;

            if (useNumbers) charSet += NUMBERS;

            if (useSpecial) charSet += SPECIALS;

            for (counter = 0; counter < stringSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return String.Join(null, _password);
        }

        public static string GenerateRegexString(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return null;

            var xeger = new Xeger(pattern, new Random());
            var generatedString = xeger.Generate();
            return generatedString;
        }

    }
}
