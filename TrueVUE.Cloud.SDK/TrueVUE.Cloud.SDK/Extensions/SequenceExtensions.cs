/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class SequenceExtensions
    {
        public static string JoinWith<T>(this IEnumerable<T> list, string separator)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            var sb = new StringBuilder();
            var enumerator = list.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return string.Empty;
            }

            while (true)
            {
                sb.Append(enumerator.Current);
                if (!enumerator.MoveNext())
                {
                    break;
                }

                sb.Append(separator);
            }

            return sb.ToString();
        }

        public static string JoinWith<T>(this IEnumerable<T> list, string initialString, string finalString, string separator)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            if (initialString == null)
            {
                throw new ArgumentNullException("initialString");
            }

            if (finalString == null)
            {
                throw new ArgumentNullException("finalString");
            }

            var sb = new StringBuilder();
            var enumerator = list.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return string.Empty;
            }

            while (true)
            {
                sb.Append(initialString);
                sb.Append(enumerator.Current);
                if (!enumerator.MoveNext())
                {
                    sb.Append(finalString);
                    break;
                }

                sb.Append(finalString);
                sb.Append(separator);
            }

            return sb.ToString();
        }
    }
}
