/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class GuidExtensions
    {
        #region Methods

        public static string ToStringSql(this Guid id)
        {
            return $"X'{id.ToString().Replace("-", string.Empty)}'";
        }

        #endregion
    }
}
