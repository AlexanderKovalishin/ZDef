using System;
using System.Security.Cryptography;
using System.Text;

namespace ZDef.Bootsrtap
{
    internal static class PasswordGenerator
    {
        private const int PasswordByteLength = 3;

        public static string Generate()
        {
            using var rng = RandomNumberGenerator.Create();
            var buffer = new byte[PasswordByteLength];
            rng.GetBytes(buffer);
            var value = ToHex(buffer);
            return value;
        }

        private static string ToHex(byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            
            var stringBuilder = new StringBuilder();
            foreach (var b in bytes)
            {
                stringBuilder.Append(GetHexValue(b / 16));
                stringBuilder.Append(GetHexValue(b % 16));
            }
            return stringBuilder.ToString();
        }

        private static string GetHexValue(int i)
        {
            return i.ToString("x");
        }
    }
}