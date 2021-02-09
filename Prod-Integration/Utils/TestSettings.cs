using System;
using System.Configuration;

namespace Utils
{
    public static class TestSettings
    {
        /// <summary>
        /// Gets the environment variable if exists or the app.config default value. 
        /// Throws an Exception if value is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>String value of variable</returns>
        public static string GetConfigValue(string key)
        {
            var value = Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key];

            if (value == null)
            {
                throw new ArgumentException($"'{key}' does not exist in Environment Variables or App.Config.");
            }
            return value;
        }

        /// <summary>
        /// Determines whether the test is currently executing on the grid or locally.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if test is executing on the grid; otherwise, <c>false</c> if running locally.
        /// </returns>
        public static bool IsOnGrid => GetConfigValue("ExecuteOnGrid").ToLower().Equals("true");
    }
}
