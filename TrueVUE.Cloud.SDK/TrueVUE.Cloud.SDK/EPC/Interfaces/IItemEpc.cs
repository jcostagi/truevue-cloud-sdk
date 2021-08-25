/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Epc
{
    public interface IItemEpc
    {
        byte[] Epc { get; }
        
        string Code();

        long SerialNumber();

		long Sku();

        string RawTagValue();

        bool IsValid();
    }
}
