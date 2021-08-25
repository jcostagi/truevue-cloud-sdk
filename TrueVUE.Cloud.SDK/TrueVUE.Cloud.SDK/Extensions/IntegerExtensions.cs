/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Globalization;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToStringLimited(this int value, int limit = 99)
        {
            return value > limit ? $"+{limit}" : value.ToString();
        }

        public static string LocalizedInt(this int number)
        {
            return number.ToString("N0", CultureInfo.CurrentCulture);
        }
    }
}