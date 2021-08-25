/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Collections
{
    public class ByteArrayComparer : IByteArrayComparer
    {
        public bool Equals(byte[] x, byte[] y)
        {
            if (x == y)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            if (x.Length != y.Length)
            {
                return false;
            }

            for (var i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(byte[] obj)
        {
            if (obj == null || obj.Length == 0)
            {
                return 0;
            }

            unchecked
            {
                var hashCode = 0;
                for (var i = 0; i < obj.Length; i++)
                {
                    hashCode = (hashCode << 3) | (hashCode >> 29) ^ obj[i];
                }

                return hashCode;
            }
        }
    }
}
