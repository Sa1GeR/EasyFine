using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{
    public static class AlphanumericConverter
    {
        /// <summary>
        /// Converts alphanumber to int
        /// </summary>
        /// <param name="alphanumeric">e.g. 1a, 1, 2ab</param>
        /// <returns></returns>
        public static decimal ConvertAlphanumericToDecimalValue(string alphanumeric)
        {
            var value = 0m;
            if (string.IsNullOrWhiteSpace(alphanumeric)) return 0;
            //split the 

            var match = Regex.Match(alphanumeric, @"(\d*)([a-zA-Z]*)");
            var numberString = match.Groups[1].Value;
            if (!string.IsNullOrEmpty(numberString))
            {
                value = int.Parse(numberString);
            }

            var alphaString = match.Groups[2].Value;
            if (!string.IsNullOrEmpty(alphaString))
            {
                foreach (var letter in alphaString)
                {
                    value += ((int)letter) / 1000m;
                }
            }

            return value;
        }

        public static int ConvertAlphanumericToInt32Value(string alphanumeric)
        {
            return Convert.ToInt32(ConvertAlphanumericToDecimalValue(alphanumeric));
        }
    }
}
