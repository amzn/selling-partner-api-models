using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace Amazon.SellingPartnerAPIAA
{
    public static class Utils
    {
        public const string ValidUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        /// <summary>
        /// Returns URL encoded version of input data according to RFC-3986
        /// </summary>
        /// <param name="data">String to be URL-encoded</param>
        /// <returns>URL encoded version of input data</returns>
        public static string UrlEncode(string data)
        {
            StringBuilder encoded = new StringBuilder();
            foreach (char symbol in Encoding.UTF8.GetBytes(data))
            {
                if (ValidUrlCharacters.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%").Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)symbol));
                }
            }
            return encoded.ToString();
        }

        /// <summary>
        /// Returns hashed value of input data using SHA256
        /// </summary>
        /// <param name="data">String to be hashed</param>
        /// <returns>Hashed value of input data</returns>
        public static byte[] Hash(string data)
        {
            return new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// Returns lowercase hexadecimal string of input byte array
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <returns>Lowercase hexadecimal string</returns>
        public static string ToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Computes the hash of given string using the specified key with HMACSHA256
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">String to be hashed</param>
        /// <returns>Hashed value of input data</returns>
        public static byte[] GetKeyedHash(byte[] key, string value)
        {
            KeyedHashAlgorithm hashAlgorithm = new HMACSHA256(key);
            hashAlgorithm.Initialize();
            return hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}
