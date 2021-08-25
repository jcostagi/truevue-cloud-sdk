/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using TrueVUE.Cloud.SDK.Barcode.Interfaces;
using TrueVUE.Cloud.SDK.Extensions;

namespace TrueVUE.Cloud.SDK.Barcode.Models
{
    class UCC12Barcode : BaseGS1Barcode, IBarcode
    {
        #region Fields

        public const int LENGTH = 12;

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

        public UCC12Barcode(string barcode, int companyLength)
        {
            if (barcode == null)
            {
                throw new Exception("Unable to create barcode from null string");
            }

            if (barcode.Length != LENGTH)
            {
                throw new Exception("Invalid barcode length");
            }

            ParseBarcode(barcode, companyLength);
        }

        public UCC12Barcode(string managerCode, long productCode)
        {
            if ((long.Parse(managerCode) == -1) || (productCode == -1))
            {
                CompanyPrefix = managerCode;
                ItemReference = productCode;
                return;
            }

            CompanyPrefix = managerCode;
            ItemReference = productCode;

            CompanyLength = CompanyPrefix.Length;

            var productLength = ItemReference.ToString().Length;

            // try to conform to the more "standard" length if possible
            if (CompanyLength < 6 && productLength <= 5)
            {
                CompanyLength = 6;
            }
        }

        #endregion

        #region Methods

        void ParseBarcode(string barcodeLocal, int companyLengthLocal)
        {
            string barcodeTemp = barcodeLocal;

            // verify the check digit
            if (barcodeTemp.Length == LENGTH)
            {
                int checkDigit = int.Parse(barcodeTemp.Substring(11));
                int val = CalculateMod10CheckDigit(barcodeTemp.Substring(0, 11));

                IsValid = val == checkDigit;
            }
            else
            {
                // create the check digit
                int val = CalculateMod10CheckDigit(barcodeTemp);
                barcodeTemp = barcodeTemp + val;
            }

            Barcode = barcodeLocal;
            CompanyLength = companyLengthLocal;
            CompanyPrefix = barcodeTemp.Substring(0, CompanyLength);
            ItemReference = long.Parse(barcodeTemp.SubstringEndIdx(CompanyLength, 11));
        }

        #endregion

    }
}
