/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class FloatExtensions
    {
        public static string ToPercentageString(this float value)
        {
            return string.Format("{0}%", value.ToString("####0.00"));
        }
    }
}