/*******************************************************************************
 * Copyright (c) 2021 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Linq;
using TrueVUE.Cloud.Models;
using TrueVUE.Cloud.SDK.Barcode.Interfaces;
using TrueVUE.Cloud.SDK.Barcode.Models;

namespace TrueVUE.Cloud.SDK.Barcode
{
    public class BarcodeSDK : IBarcodeSDK
    {
        #region Fields

        private const int NormalizedBarcodeLen = 14;

        #endregion

        #region Methods

        public bool IsCompanyPrefixValid(IBarcode barcode, TagType tagType, string[] companyPrefixes = null)
        {
            if (companyPrefixes == null
                || string.IsNullOrEmpty(barcode.CompanyPrefix))
            {
                return true;
            }

            return companyPrefixes.Any(barcode.CompanyPrefix.Equals);
        }

        public IBarcode GetBarcode(string barcode, int? companyLength, TagType tagType)
        {
            string normalizedBarcode = barcode = barcode.PadLeft(NormalizedBarcodeLen, '0'); // normalize barcode

            if (tagType == TagType.VUESER || tagType == TagType.VUESERAUTH)
            {
                return new DefaultBarcode(normalizedBarcode);
            }

            switch (barcode.Length)
            {
                case EAN13Barcode.LENGTH:
                    return new EAN13Barcode(barcode, companyLength.Value);

                case GTIN14Barcode.LENGTH:
                    return new GTIN14Barcode(barcode, companyLength.Value, 0); // Will always go here

                case UCC12Barcode.LENGTH:
                    return new UCC12Barcode(barcode, companyLength.Value);

                case UPCEBarcode.LENGTH:
                    return new UPCEBarcode(barcode);

                default: throw new Exception("Illegal barcode length: " + barcode.Length);
            }
        }

        #endregion
    }
}
