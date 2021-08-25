// /*******************************************************************************
//  * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
//  * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
//  *******************************************************************************/

using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this JsonSerializer serializer, Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    return await Task.FromResult(serializer.Deserialize<T>(jsonReader));
                }
            }
        }
    }
}
