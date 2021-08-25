/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] MergeArrays<T>(this T[] array1, T[] array2) where T : class
        {
            if (array1 == null || array2 == null)
            {
                return new T[0];
            }

            T[] newArray = new T[array1.Length + array2.Length];
            Array.Copy(array1, newArray, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);
            return newArray;
        }
    }
}
