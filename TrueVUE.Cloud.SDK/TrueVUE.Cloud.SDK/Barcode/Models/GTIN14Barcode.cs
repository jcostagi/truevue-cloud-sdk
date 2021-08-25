/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using TrueVUE.Cloud.SDK.Barcode.Interfaces;
using TrueVUE.Cloud.SDK.Extensions;

namespace TrueVUE.Cloud.SDK.Barcode.Models
{
    class GTIN14Barcode : BaseGS1Barcode, IBarcode
    {
        #region Fields

        public const int LENGTH = 14;

        #endregion

        #region Properties

        public string Barcode { get; private set; }

        public int Filter { get; private set; }

        public int IndicatorDigit { get; private set; }
       
        public string CompanyPrefix { get; private set; }

        public long ItemReference { get; private set; }

        public int CompanyLength { get; private set; }

        public bool IsValid { get; private set; }

        #endregion

        #region Constructors

        public GTIN14Barcode(string barcode, int companyLength, int filter)
        {
            if (barcode == null)
            {
                throw new Exception("Unable to create barcode from null string");
            }

            if (barcode.Length != LENGTH)
            {
                throw new Exception("Invalid barcode length");
            }

            Filter = filter;

            ParseBarcode(barcode, companyLength);
        }

        #endregion

        #region Methods

        void ParseBarcode(string barcodeLocal, int companyLengthLocal)
        {
            // verify the check digit
            int checkDigit = int.Parse(barcodeLocal.Substring(13));
            int val = CalculateMod10CheckDigit(barcodeLocal.Substring(0, 13));

            IsValid = val == checkDigit;
            Barcode = barcodeLocal;
            CompanyLength = companyLengthLocal;
            IndicatorDigit = int.Parse(Barcode.Substring(0, 1));
            CompanyPrefix = Barcode.SubstringEndIdx(1, 1 + CompanyLength);
            ItemReference = long.Parse(Barcode.SubstringEndIdx(1 + CompanyLength, 13));
        }

        #endregion

    }
}
