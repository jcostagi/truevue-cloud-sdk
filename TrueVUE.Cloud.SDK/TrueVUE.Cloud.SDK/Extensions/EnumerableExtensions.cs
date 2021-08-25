/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToSeparatedCommaString<T>(this IEnumerable<T> values)
        {
            return string.Join(",", values);
        }

        public static string ToSeparatedCommaSqlString<T>(this IEnumerable<T> values)
        {
            if (typeof(T).IsEnum)
            {
                return string.Join(",", values.Select(a => $"'{a}'"));
            }

            return string.Join(",", values);
        }

        public static string ToSeparatedCommaSqlString(this IEnumerable<string> values) 
        {
            bool isFirst = true;

            StringBuilder sb = new StringBuilder();
            foreach (var value in values)
            {
                if (!isFirst)
                {
                    sb.Append(',');
                }
                else
                {
                    isFirst = false;
                }

                sb.Append('\'').Append(value.SqlEscape()).Append('\'');
            }

            return sb.ToString();
        }

        public static string ToSeparatedCommaSqlString(this IEnumerable<Guid> values) 
        {
            return string.Join(",", values.Select(s => s.ToStringSql()));
        }

        public static IEnumerable<T> OrderByNatural<T>(this IEnumerable<T> items, Func<T, string> selector, StringComparer stringComparer = null)
        {
            var regex = new Regex(@"\d+", RegexOptions.Compiled);

            int maxDigits = items
                          .SelectMany(i => regex.Matches(selector(i)).Cast<Match>().Select(digitChunk => (int?)digitChunk.Value.Length))
                          .Max() ?? 0;

            return items.OrderBy(i => regex.Replace(selector(i), match => match.Value.PadLeft(maxDigits, '0')), stringComparer ?? StringComparer.CurrentCulture);
        }
    }
}