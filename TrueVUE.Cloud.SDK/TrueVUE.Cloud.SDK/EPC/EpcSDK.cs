/*******************************************************************************
* Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
* Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
*******************************************************************************/

using System;
using System.Collections.Generic;
using TrueVUE.Cloud.Models;
using TrueVUE.Cloud.SDK.Epc.Models;
using TrueVUE.Cloud.SDK.Epc.Models.Epc;
using TrueVUE.Cloud.SDK.Extensions;
using TrueVUE.Cloud.SDK.Models;

namespace TrueVUE.Cloud.SDK.Epc
{
    class EpcSDK : IEpcSDK
    {
        #region Constructors

        public EpcSDK(IEpcValidator epcValidator)
        {
            EpcValidator = epcValidator;
        }

        #endregion

        #region Properties

        IEpcValidator EpcValidator { get; }

        //ISettingsSDK SettingsSDK { get; }

        //IProductSDK ProductSDK { get; }

        //ProductIdMappingCache ProductIdMappingCache { get; }

        #endregion

        public long? TryGetSku(byte[] epc)
        {
            long? productId = null;
            if (epc != null)
            {
                productId = GetSku(epc);
            }

            return productId;
        }

        public IItemEpc GetEpc(TagType tagType, Dictionary<string, object> epcFields)
        {
            switch (tagType)
            {
                case TagType.VUESER:
                    return new VUESERIAL96(epcFields);
                case TagType.SGTIN_96:
                    return new SGTIN96(epcFields);
                case TagType.VUESERAUTH:
                    return new VUESERIALAUTH96(epcFields);
                default:
                    throw new NotImplementedException($"Tag type {tagType} has not been implemented.");
            }
        }

        public TagType GetTagType(string tagType)
        {
            switch (tagType)
            {
                case "SGTIN_96":
                    return TagType.SGTIN_96;
                case "VUESER":
                    return TagType.VUESER;
                case "VUESERAUTH":
                    return TagType.VUESERAUTH;
                default:
                    throw new NotImplementedException($"Tag type {tagType} has not been implemented.");
            }
        }

        public bool ValidateHeader(byte[] epc, ByteHeader[] headers)
        {
            if (headers == null)
            {
                return true;
            }

            foreach (var header in headers)
            {
                if (epc.StartsWith(header.Mask, header.Header))
                {
                    return true;
                }
            }

            return false;
        }

        long? GetSku(byte[] epc)
        {
            if (EpcValidator.TryGetItemEpc(epc, out IItemEpc itemEpc)
                && long.TryParse(itemEpc.Code(), out long productIdEpc))
            {
                return productIdEpc;
            }

            return null;
        }
    }
}
