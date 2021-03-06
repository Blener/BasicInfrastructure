﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BasicInfrastructureExtensions.Helpers;

namespace BasicInfrastructureExtensions.Extensions
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
        public static string Format(this string value, object arg0)
        {
            return string.Format(value, arg0);
        }

        [DebuggerStepThrough]
        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
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
            return !value.IsEmpty()
                   && Regex.IsMatch(value, @"(^\d{2}[.]?\d{3}[-]?\d{3}$)");
        }

        [DebuggerStepThrough]
        public static bool IsURL(this string value)
        {
            return Regex.IsMatch(value, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

        [DebuggerStepThrough]
        public static bool IsCPF(this string source)
        {
            return Cpf.Validar(source);
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
        public static bool IsCNPJ(this string source)
        {
            return Cnpj.ValidaCNPJ(source);
        }

        [DebuggerStepThrough]
        public static bool IsCpfOrCnpj(this string source)
        {
            return source.IsCNPJ() || source.IsCPF();
        }

        [DebuggerStepThrough]
        public static string ToSeparatedWords(this string value)
        {
            value = Regex.Replace(value, "([_]|[-])([A-Z]|[a-z])", " $2").Trim();
            value = Regex.Replace(value, "(?<=[^ ])([A-Z][a-z])", " $1").Trim();
            return value;
        }



        [DebuggerStepThrough]
        public static string FormatCpfOrCnpj(this string source)
        {
            if (source.IsCNPJ())
                return FormatCNPJ(source);
            if (source.IsCPF())
                return FormatCPF(source);

            return source;
        }

        [DebuggerStepThrough]
        public static string FormatCPF(this string source)
        {
            if (source.IsEmpty()) return source;

            var cpf = long.Parse(source.ExtractNumbers());

            return string.Format(@"{0:000\.000\.000\-00}", cpf);
        }

        [DebuggerStepThrough]
        public static string FormatCNPJ(this string source)
        {
            if (source.IsEmpty()) return source;

            var cnpj = long.Parse(source.ExtractNumbers());

            return string.Format(@"{0:00\.000\.000\/0000\-00}", cnpj);
        }

        [DebuggerStepThrough]
        public static string FormatCEP(this string source)
        {
            if (source.IsEmpty()) return source;

            var cep = long.Parse(source.ExtractNumbers());

            return string.Format(@"{0:00000\-000}", cep);
        }

        [DebuggerStepThrough]
        public static string FormatPhone(this string source)
        {
            if (source.IsEmpty()) return source;

            var phone = long.Parse(source.ExtractNumbers());

            if (phone.ToString().Length == 10) return string.Format(@"{0:(##)####-####}", phone);

            return string.Format(@"{0:(##)####-#####}", phone);
        }

        [DebuggerStepThrough]
        public static bool IsNumeric(this string source)
        {
            long lng;

            return long.TryParse(source, out lng);
        }

        [DebuggerStepThrough]
        public static string Sanitize(this string source) 
            => Regex.Replace(source, "[^a-zA-Z0-9]|\\s", "").Trim();

        [DebuggerStepThrough]
        public static string Sanitize(this string source, bool ignoreSpacing) 
            => ignoreSpacing? Regex.Replace(source, "[^a-zA-Z0-9\\s]", "").Trim() : source.Sanitize();

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
                if (defaultValue.HasValue)
                    return defaultValue.Value;

                throw;
            }
        }

        public static double ToDouble(this string str, double? defaultValue = null)
        {
            try
            {
                var d = double.Parse(str);
                if( d.IsNanOrInfinity())
                    throw new FormatException();
                return d;
            }
            catch
            {
                if (defaultValue.HasValue)
                    return defaultValue.Value;

                throw;
            }
        }

        public static bool IsNanOrInfinity(this double value)
        {
            return double.IsNaN(value) || double.IsInfinity(value);
        }

        [DebuggerStepThrough]
        public static long ToLong(this string str, long? defaultValue = null)
        {
            try
            {
                return long.Parse(str);
            }
            catch
            {
                if (defaultValue.HasValue)
                    return defaultValue.Value;

                throw;
            }
        }

        [DebuggerStepThrough]
        public static long ToDateTimeTicks(this string str)
        {
            return DateTime.Parse(str).Ticks;
        }

        [DebuggerStepThrough]
        public static DateTime ToDateTime(this string str)
        {
            return DateTime.Parse(str);
        }


        [DebuggerStepThrough]
        public static bool EqualsIgnoreCase(this string s, string value)
        {
            return s.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
        [DebuggerStepThrough]
        public static bool ContainsIgnoreCase(this string s, string value)
        {
            return s.ToLowerInvariant().Contains(value.ToLowerInvariant());
        }
        [DebuggerStepThrough]
        public static bool StartsWithIgnoreCase(this string s, string value)
        {
            return s.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }
        [DebuggerStepThrough]
        public static bool EndsWithIgnoreCase(this string s, string value)
        {
            return s.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }


        [DebuggerStepThrough]
        public static string RemoveStringTags(this string text)
        {
            return text.Replace("\\n", ((char)10).ToString())
                .Replace("\\r", ((char)13).ToString())
                    .Replace("\\\"", ((char)34).ToString());
        }
        [DebuggerStepThrough]
        public static bool IsPIS(this string text)
        {
            return Pis.Validate(text);
        }

        /// <summary>
        /// Converte uma string em "camelHump", "PascalCase", "lisp-case" ou "snake_case" ou separada por espaço em PascalCase,
        /// </summary>
        /// <param name="value">A instância da string</param>
        /// <returns>Uma palavra separada por espaço caso a original se aplique às regras mencionadas</returns>
        [DebuggerStepThrough]
        public static string ToPascalCase(this string value)
        {
            var matches = Regex.Matches(value, "([A-Z][a-z]{1,})|(?<=[a-z])[A-Z]{1,}");
            foreach (Match match in matches)
            {
                var pascal = match.Value;
                value = value.Replace(pascal, $"{pascal.Substring(0, 1).ToUpper()}{pascal.Substring(1).ToLower()}");
            }

            value = $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";

            matches = Regex.Matches(value, "([_]|[-]|[ ])([a-z])*");
            foreach (Match match in matches)
            {
                var pascal = match.Value;
                if (pascal.Length > 1)
                    value = value.Replace(pascal, $"{pascal.Substring(1, 1).ToUpper()}{pascal.Substring(2).ToLower()}");
            }

            matches = Regex.Matches(value, "([_]|[-]|[ ])([A-Z])*");
            foreach (Match match in matches)
            {
                var pascal = match.Value;
                if (pascal.Length > 1)
                    value = value.Replace(pascal, $"{pascal.Substring(1, 1).ToUpper()}{pascal.Substring(2).ToLower()}");
            }

            value = $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";

            if (value.IsAllUpper())
                value = $"{value.Substring(0, 1).ToUpper()}{value.Substring(1).ToLower()}";

            value = Regex.Replace(value, "[_]|[-]|[ ]", "");

            value = Regex.Replace(value, "(?<=^[A-Z])[A-Z]{1,}(?=[A-Z][a-z])", x => x.Value.ToLower());
            return value;
        }

        [DebuggerStepThrough]
        public static bool IsAllUpper(this string str)
        {
            return str.ToUpper() == str;
        }
        [DebuggerStepThrough]
        public static bool IsAllLower(this string str)
        {
            return str.ToLower() == str;
        }

    }
}