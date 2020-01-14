using System;
using System.Text;

namespace Tests.Utility.Extensions
{
    public static class RandomExtensions
    {
        public const string AlphanumericCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string AlphabeticalCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string HexadecimalCharacters = "abcdef0123456789";
        public const string WhiteSpaceCharacters = " \x0\t\r\n\f";

        public static string NextString(this Random random, int len)
        {
            return random.NextString(AlphanumericCharacters, len);
        }

        public static string NextAlphabeticalString(this Random random, int len)
        {
            return random.NextString(AlphabeticalCharacters, len);
        }

        public static string NextHexString(this Random random, int len)
        {
            return random.NextString(HexadecimalCharacters, len);
        }

        public static string NextString(this Random random, string characters, int len)
        {
            if (random == null)
            {
                throw new NullReferenceException();
            }
            if (characters == null)
            {
                throw new ArgumentNullException(nameof(characters));
            }

            if (len == 0)
            {
                return string.Empty;
            }
            if (len == 1)
            {
                return new string(SelectCharacter(random, characters), 1);
            }
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; ++i)
            {
                sb.Append(SelectCharacter(random, characters));
            }
            return sb.ToString();
        }

        public static bool NextBool(this Random random)
        {
            if (random is null)
            {
                throw new NullReferenceException();
            }
            return random.Next(2) == 0;
        }

        private static char SelectCharacter(Random random, string characters)
        {
            return characters[random.Next(characters.Length)];
        }
    }
}
