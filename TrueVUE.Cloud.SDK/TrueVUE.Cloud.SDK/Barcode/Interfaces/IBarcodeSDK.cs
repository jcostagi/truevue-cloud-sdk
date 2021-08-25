/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using TrueVUE.Cloud.Models;

namespace TrueVUE.Cloud.SDK.Barcode.Interfaces
{
    public interface IBarcodeSDK
    {
        IBarcode GetBarcode(string barcode, int? companyPrefixLength, TagType tagType);

        bool IsCompanyPrefixValid(IBarcode barcode, TagType tagType, string[] companyPrefixes = null);
    }
}
