// /*******************************************************************************
//  * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
//  * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
//  *******************************************************************************/

using System;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class UriExtensions
    {
        static readonly string envFormat = "tvc-{0}";
        static readonly string defaultEnv = "prod";

        /// <summary>
        /// Will search using current URLs schemas for an Env in case of not env found default will be prod
        ///   Current Not Prod Schema 
        ///      http(s) ://[bu]-[bucode]-[random]-[env].truevue.shoppertrak.com
        ///   Current Prod Schema
        ///      https://[bucode].truevuecloud.com
        /// </summary>
        public static string ToEnviroment(this Uri uri)
        {
            string env = string.Format(envFormat, defaultEnv);

            string[] subdomains = uri.Authority.Split('.');
            if (subdomains?.Length > 0)
            {
                string[] alias = subdomains[0].Split('-');
                if (alias?.Length > 3)
                {
                    env = string.Format(envFormat, string.Join("-", alias, 3, Math.Max(1, alias.Length - 3)));
                }
            }

            return env;
        }

        public static Uri Append(this Uri uri, string relativePath)
        {
            //avoid the use of Uri as it's not needed, and adds a bit of overhead.
            var absoluteUri = uri.AbsoluteUri; //a calculated property, better cache it
            var baseUri = absoluteUri.EndsWith("/") ? absoluteUri : absoluteUri + '/';
            var relative = relativePath.StartsWith("/") ? relativePath.Substring(1) : relativePath;
            return new Uri(baseUri + relative);
        }
    }
}