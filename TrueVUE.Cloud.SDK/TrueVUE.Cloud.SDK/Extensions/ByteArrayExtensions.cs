/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Linq;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class ByteArrayExtensions
    {
        #region Properties

        static uint[] Lookup32 { get; } = Enumerable.Range(0, 256).Select(i =>
        {
            string s = i.ToString("X2");
            return s[0] + ((uint)s[1] << 16);
        }).ToArray();

        #endregion

        #region Methods

        public static string ToHexString(this byte[] bytes)
        {
            var result = new char[bytes.Length * 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                var val = Lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }

            return new string(result);
        }

        public static bool StartsWith(this byte[] bytes, byte[] mask, byte[] values)
        {
            if (bytes.Length < values.Length)
            {
                return false;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if ((bytes[i] & mask[i]) != values[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
