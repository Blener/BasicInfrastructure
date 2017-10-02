using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Globalization;

namespace BasicInfrastructure.Extensions
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static bool In(this string value, params string[] stringValues)
        {
            return stringValues.Any(otherValue => String.CompareOrdinal(value, otherValue) == 0);
        }
        
        [DebuggerStepThrough]
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        
        [DebuggerStepThrough]
        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }
        
        [DebuggerStepThrough]
        public static string Left(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }
        
        [DebuggerStepThrough]
        public static string ExtractNumbers(this string value)
        {
            return value == null ? null : string.Join(null, Regex.Split(value, "[^\\d]"));
        }
        
        [DebuggerStepThrough]
        public static string FormatNumber(this string value, string pattern)
        {
            var number = long.Parse(ExtractNumbers(value));
        
            return string.Format(pattern, number);
        }
        
        [DebuggerStepThrough]
        public static bool IsCEP(this string value)
        {
            return !value.IsEmpty() && (Regex.IsMatch(value, @"^\d{2}.\d{3}-\d{3}$") || Regex.IsMatch(value, @"^\d{5}-\d{3}$") || Regex.IsMatch(value, @"^\d{8}$"));
        }
        
        [DebuggerStepThrough]
        public static bool IsURL(this string value)
        {
            return Regex.IsMatch(value, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        
        [DebuggerStepThrough]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value.NullSafe());
        }
        
        [DebuggerStepThrough]
        public static string IsEmpty(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value.NullSafe()) ? defaultValue : value;
        }
        
        [DebuggerStepThrough]
        public static string NullSafe(this string value)
        {
            return (value ?? string.Empty).Trim();
        }
        
        [DebuggerStepThrough]
        public static bool IsEmail(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
        
        [DebuggerStepThrough]
        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }
        
        [DebuggerStepThrough]
        public static bool IsNumeric(this string source)
        {
            long lng;
        
            return long.TryParse(source, out lng);
        }
        
        [DebuggerStepThrough]
        public static string Sanitize(this string source)
        {
            if (source.IsEmpty()) return source;

            var r = string.Empty;
            var matches = Regex.Matches(source, "\\w");
            return matches.Cast<Match>().Aggregate(r, (current, match) => current + match.ToString()); 
        }
        
        [DebuggerStepThrough]
        public static byte[] GetFileData(this string fileName, string filePath)
        {
            var fullFilePath = string.Format("{0}/{1}", filePath, fileName);
            if (!File.Exists(fullFilePath)) throw new FileNotFoundException("The file does not exist.", fullFilePath);
        
            return File.ReadAllBytes(fullFilePath);
        }
        
        [DebuggerStepThrough]
        public static string NormalizeString(this string source)
        {
            var s = source.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in s)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark) sb.Append(ch);
            }
        
            return sb.ToString();
        }
        
        [DebuggerStepThrough]
        public static Guid ToGuid(this string target)
        {
            var result = Guid.Empty;
            
            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                var encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");
                
                try
                {
                    var base64 = Convert.FromBase64String(encoded);
                    result = new Guid(base64);
                }
                catch (FormatException)
                {
                }
            }
        
            return result;
        }

        [DebuggerStepThrough]
        public static string Limit(this string str, int characterCount)
        {
            if (str.IsEmpty()) return "";
            if (str.Length <= characterCount) return str;
            else return str.Substring(0, characterCount).TrimEnd(' ');
        }

        [DebuggerStepThrough]
        public static string LimitWithElipses(this string str, int characterCount)
        {
            if (str.IsEmpty()) return "";
            if (characterCount < 5) return str.Limit(characterCount);
            if (str.Length <= characterCount - 3) return str;
            else return str.Substring(0, characterCount - 3) + "...";
        }

        [DebuggerStepThrough]
        public static string LimitWithElipsesOnWordBoundary(this string str, int characterCount)
        {
            if (str.IsEmpty()) return "";
            if (characterCount < 5) return str.Limit(characterCount);
            if (str.Length <= characterCount - 3) return str;
            else
            {
                int lastspace = str.Substring(0, characterCount - 3).LastIndexOf(' ');
                if (lastspace > 0 && lastspace > characterCount - 10)
                {
                    return str.Substring(0, lastspace) + "...";
                }
                else
                {
                    // No suitable space was found
                    return str.Substring(0, characterCount - 3) + "...";
                }
            }
        }

        [DebuggerStepThrough]
        public static int ToInt(this string str, int? defaultValue = null)
        {
            try
            {
                return int.Parse(str);
            }
            catch
            {
                if(defaultValue.HasValue)                
                    return defaultValue.Value;

                throw;
            }
        }
		
		[DebuggerStepThrough]
        public static string RemoveStringTags(this string text)
        {
            return text.Replace("\\n", ((char)10).ToString())
                .Replace("\\r", ((char)13).ToString())
                    .Replace("\\\"", ((char)34).ToString());
        }
    }
}