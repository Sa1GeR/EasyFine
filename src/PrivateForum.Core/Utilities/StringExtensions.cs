using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{
    public static class StringExtensions
    {
        public static string Capitalise(this string str)
        {
            try
            {
                if (String.IsNullOrEmpty(str))
                    return String.Empty;

                if (str.Length > 1)
                {
                    var splitted = str.Split(' ');
                    if (splitted.Length > 0)
                    {
                        return string.Join(" ", splitted.Select(s => Char.ToUpper(s[0]) + s.Substring(1).ToLower()));
                    }

                    return Char.ToUpper(str[0]) + str.Substring(1).ToLower();
                }

                return Char.ToUpper(str[0]).ToString();
            }
            catch
            {
                return Char.ToUpper(str[0]).ToString();
            }
        }
        
        public static string StripTags(this string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string ToEllipsis(this string element, int length = 30)
        {
            if (element.Length <= length) return element;
            int pos = element.IndexOf(" ", length);
            if (pos >= 0)
                return element.Substring(0, pos) + "...";
            return element;
        }

        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public static Dictionary<string, int> Base64ToParams(this string hash, List<string> outputParams, char delimiter = '/')
        {
            if (!hash.IsBase64String())
                return null;

            byte[] bytes = Convert.FromBase64String(hash);
            string parsed = System.Text.Encoding.UTF8.GetString(bytes);

            if (!parsed.Contains(delimiter.ToString()) && outputParams.Count() > 1)
                return null;

            var dictionary = new Dictionary<string, int>();
            string[] splited = parsed.Split(delimiter);
            for (int i = 0; i < splited.Length; i++)
            {
                if (int.TryParse(splited[i], out int output))
                    dictionary.Add(outputParams[i], output);
            }

            return dictionary;
        }


        public static string ConvertCourseNameToClassName(this string name)
        {
            string name_low = name.ToLower();
            string replacedString = name_low.Replace("+ ", String.Empty);
            replacedString = replacedString.Replace(" ", "_");
            return replacedString;
        }
    }
}
