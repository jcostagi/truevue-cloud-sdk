/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using TrueVUE.Cloud.SDK.Barcode.Interfaces;
using TrueVUE.Cloud.SDK.Extensions;

namespace TrueVUE.Cloud.SDK.Barcode.Models
{
    class UPCEBarcode : IBarcode
    {
        #region Fields

        public const int LENGTH = 8;

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

        public UPCEBarcode(string barcode)
        {
            if (barcode == null)
            {
                throw new Exception("Unable to create barcode from null string");
            }

            if (barcode.Length != LENGTH)
            {
                throw new Exception("Invalid barcode length");
            }

            ParseBarcode(barcode);
        }

        #endregion

        #region Methods

        private void ParseBarcode(string barcodeLocal)
        {
            string manufacturerCode, productCodeLocal;
            int lastDigit = int.Parse(barcodeLocal.Substring(6, 1));
            switch (lastDigit)
            {
                // If the manufacturer code ends with 000, 100, or 200,
                // the UPC-E code consists of the first two characters of the manufacturer
                // code, the last three characters of the product code, followed by the
                // third character of the manufacturer code. Under this case, The product
                // code must be 00000 and 00999.
                case 0:
                case 1:
                case 2:
                    manufacturerCode = barcodeLocal.SubstringEndIdx(1, 3) + barcodeLocal.Substring(6, 1) + "00";
                    productCodeLocal = "00" + barcodeLocal.SubstringEndIdx(3, 6);
                    break;

                // If the manufacturer code ends with 00 but does not meet the #1 requirement,
                // The UPC-E code consists of the first three characters of the manufacturer
                // code, the last two characters of the product code, followed by digit "3".
                // The product code can only contain two digits (00000 to 00099).
                case 3:
                    manufacturerCode = barcodeLocal.SubstringEndIdx(1, 4) + "00";
                    productCodeLocal = "000" + barcodeLocal.SubstringEndIdx(4, 6);
                    break;

                // If the manufacturer code ends in 0 but non of the above qualifies, the UPC-E
                // consists of the first four digits manufacturer code and the last digit of
                // the product code, followed by the digit "4". The product code in this case
                // can only contain one digit (00000 to 00009).
                case 4:
                    manufacturerCode = barcodeLocal.SubstringEndIdx(1, 5) + "0";
                    productCodeLocal = "0000" + barcodeLocal.SubstringEndIdx(5, 6);
                    break;

                // If the manufacturer code ends with non-zero digit, the UPC-E code consists
                // of the manufacturer code and the last digit of the product code. In this
                // case the product case can only be one from 00005 to 00009 because 0 to 4 has
                // been used for the above cases.
                default:
                    manufacturerCode = barcodeLocal.SubstringEndIdx(1, 6);
                    productCodeLocal = "0000" + barcodeLocal.Substring(6, 1);
                    break;
            }

            Barcode = barcodeLocal;
            CompanyPrefix = manufacturerCode;
            ItemReference = long.Parse(productCodeLocal);

            // verify the check digit
            int checkDigit = int.Parse(barcodeLocal.Substring(7));
            UCC12Barcode ucc12 = new UCC12Barcode(CompanyPrefix, ItemReference);
            int val = int.Parse(ucc12.ToString().Substring(11));

            IsValid = val == checkDigit;
        }

        #endregion

    }
}
