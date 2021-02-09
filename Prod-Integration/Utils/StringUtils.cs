using System;
using System.Linq;

namespace Prod_Integration.Utils
{
    /// <summary>
    /// It's pretty important to have some random data generator. Especially in boundary value analysis.
    /// </summary>
    public class StringUtils
    {
        private static readonly Random Random = new Random();
        private const string AlphaNumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Creates pseudo random string of given length.
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(string chars, int length) =>
            new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());

        /// <summary>
        /// Creates random alpha numeric string of given length.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomAlphaNumericString(int length) =>
            RandomString(AlphaNumericChars, length);
    }
}