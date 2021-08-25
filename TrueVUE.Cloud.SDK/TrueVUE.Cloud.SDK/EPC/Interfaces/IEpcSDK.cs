/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using TrueVUE.Cloud.Models;
using TrueVUE.Cloud.SDK.Models;

namespace TrueVUE.Cloud.SDK.Epc
{
    public interface IEpcSDK
    {
        long? TryGetSku(byte[] epc);

        IItemEpc GetEpc(TagType tagType, Dictionary<string, object> epcFields);

        TagType GetTagType(string tagType);

        bool ValidateHeader(byte[] epc, ByteHeader[] headers);
    }
}
