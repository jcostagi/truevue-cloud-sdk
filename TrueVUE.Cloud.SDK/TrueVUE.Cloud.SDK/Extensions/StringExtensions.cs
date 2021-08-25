/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Linq;
using System.Text;
using TrueVUE.Cloud.SDK.Models;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class StringExtensions
    {
        public static ByteHeader ToByteHeader(this string hex)
        {
            byte[] header = hex.ToByteArray(true);
            byte[] mask = new byte[header.Length];
            for (int i = 0; i < mask.Length; i++)
            {
                mask[i] = 0xFF;
            }

            if (hex.Length % 2 != 0)
            {
                mask[mask.Length - 1] = 0xF0;
            }

            return new ByteHeader
            {
                Header = header,
                Mask = mask
            };
        }

        public static byte[] ToByteArray(this string hex, bool allowFillOdds = false)
        {
            if (hex.Length % 2 == 1 && allowFillOdds)
            {
                hex = hex.PadRight(hex.Length + 1, '0');
            }

            var bytes = new byte[hex.Length >> 1];

            for (var i = 0; i < (hex.Length >> 1); ++i)
            {
                bytes[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return bytes;
        }

        static int GetHexVal(char hex)
        {
            return hex - (hex < 58 ? 48 : 55);

            //int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        const char TrueVUEURLDelimiter = '|';
        const string TrueVUEURLPrefix = "TrueVUE";

        public static string ToUsernameFormat(this string value)
        {
            return $"@{value}";
        }

        public static string ToShortcut(this string value)
        {
            string text = string.Empty;

            char[] delimiterChars = { ' ', ',', '.', ':', '_', '-' };

            if (!string.IsNullOrEmpty(value))
            {
                string[] words = value.Trim().Split(delimiterChars);
                text = words.Length > 0 ? words[0][0].ToString().ToUpper() : string.Empty;
                text += words.Length > 1 ? words[words.Length - 1][0].ToString().ToUpper() : string.Empty;
            }

            return text;
        }

        public static string SubstringEndIdx(this string value, int start, int end)
        {
            return value.Substring(start, end - start);
        }

        public static bool TryGetValidUrl(this string value, out Uri uri)
        {
            if (Uri.TryCreate(value, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }

            uri = null;
            return false;
        }

        public static string GetTrueVUEUrl(this string value)
        {
            string[] trueVUEArray = value.Split(TrueVUEURLDelimiter);

            if (trueVUEArray.Length != 2)
            {
                return null;
            }

            if (!trueVUEArray[0].Equals(TrueVUEURLPrefix))
            {
                return null;
            }

            return Uri.TryCreate(trueVUEArray[1], UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) ? trueVUEArray[1] : null;
        }

        public static string GuidToString(this string value)
        {
            return value.Replace("-", string.Empty);
        }

        public static string GuidToString(this Guid value)
        {
            return value.ToString().Replace("-", string.Empty);
        }

        public static string GetSubstringByString(this string c, string a, string b)
        {
            return c.Substring((c.IndexOf(a, StringComparison.Ordinal) + a.Length), (c.IndexOf(b, StringComparison.Ordinal) - c.IndexOf(a, StringComparison.Ordinal) - a.Length));
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string EnumToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static Guid ToGuid(this string value)
        {
            Guid.TryParse(value, out Guid result);
            return result;
        }

        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }

        public static bool Contains(this string target, string value, StringComparison comparison)
        {
            return target.IndexOf(value, comparison) >= 0;
        }

        public static string SqlEscape(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input.Replace("'", "''");
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "NA";
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string ToIdentifier(this string input)
        {
            char[] separators = { ' ', ';', ',', '\r', '\t', '\n', '|' };
            string[] temp = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            input = string.Join(string.Empty, temp);

            return input;
        }
    }
}
