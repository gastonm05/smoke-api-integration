using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_Integration.Utils
{
    public class TimeUtils
    {
        /// <summary>
        /// Get system current time zone name
        /// </summary>
        /// <returns></returns>
        public static string GetLocalTimeZone()
        {
            var localTimeZone =  TimeZoneInfo.Local;
            return localTimeZone.ToString();
        }
    }
}
