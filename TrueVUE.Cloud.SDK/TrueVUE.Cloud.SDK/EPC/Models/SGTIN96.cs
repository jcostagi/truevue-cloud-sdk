/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrueVUE.Cloud.SDK.Extensions;
using TrueVUE.Cloud.SDK.Epc.Models;

namespace TrueVUE.Cloud.SDK.Epc
{
    // ===================
    // Binary Coding Table
    // ===================
    //
    // Header | Filter | Partition | Company | Indicator / Item Ref | Serial
    // -------|--------|-----------|---------|----------------------|-------
    //    8   |    3   |     3     |  20-40  |        24-4          |   38

    // ===============
    // Partition Table
    // ===============
    //
    // Partition |   Company     | Indicator / Item Ref
    // ----------|------|--------|----------|----------
    //           | Bits | Digits |   Bits   |  Digits
    // ----------|------|--------|----------|----------
    //     0     |  40  |   12   |     4    |     1
    //     1     |  37  |   11   |     7    |     2
    //     2     |  34  |   10   |    10    |     3
    //     3     |  30  |    9   |    14    |     4
    //     4     |  27  |    8   |    17    |     5
    //     5     |  24  |    7   |    20    |     6
    //     6     |  20  |    6   |    24    |     7

    struct SGTIN96 : IItemEpc
    {
        #region Fields

        const int maxGtinPlusItemValue = 14;

        const int minCompanyLenght = 6;

        static readonly int[] companyRange = { 12, 11, 10, 9, 8, 7, 6 };

        static readonly int[] bCompanyRange = { 40, 37, 34, 30, 27, 24, 20 };

        static readonly int[] bItemRange = { 4, 7, 10, 14, 17, 20, 24 };

        static readonly int[] itemRange = { 1, 2, 3, 4, 5, 6, 7 };

        static readonly byte bit0 = 1;

        static readonly byte bit1 = 1 << 1;

        static readonly byte bit2 = 1 << 2;

        static readonly byte bit3 = 1 << 3;

        static readonly byte bit4 = 1 << 4;

        static readonly byte bit5 = 1 << 5;

        static readonly byte bit6 = 1 << 6;

        static readonly byte bit7 = 1 << 7;

        static readonly EpcFieldDefinition[] fields =
        {
            new EpcFieldDefinition(Constants.Constants.FIELD_FILTER, 1, 1, "Filter value describing the type of object.", false),
            new EpcFieldDefinition(Constants.Constants.FIELD_COMPANY_PREFIX, 0, 12, "Company number assigned to the item.",  true),
            new EpcFieldDefinition(Constants.Constants.FIELD_IND_DIGIT, 0, 1, "Indicator Digit for the SGTIN encoding.", false),
            new EpcFieldDefinition(Constants.Constants.FIELD_ITEM_REFERENCE, 0, 7, "Product Id / Item Reference.", true),
            new EpcFieldDefinition(Constants.Constants.FIELD_SERIAL_NUMBER, 0, 12, "Serial Number of the item.", true),
        };

        #endregion

        #region Constructors

        public SGTIN96(byte[] epc)
        {
            Epc = epc;
        }

        public SGTIN96(Dictionary<string, object> epcFields)
            : this()
        {
            // validate fields contain required fields
            if (!fields.Where(w => w.IsRequired).All(f => epcFields.ContainsKey(f.FieldName)))
            {
                throw new KeyNotFoundException("Missing required field(s)");
            }
            
            int filter = epcFields.ContainsKey(Constants.Constants.FIELD_FILTER) ? (int)epcFields[Constants.Constants.FIELD_FILTER] : 1;
            int indicatorDigit = epcFields.ContainsKey(Constants.Constants.FIELD_IND_DIGIT) ? (int)epcFields[Constants.Constants.FIELD_IND_DIGIT] : 0;
            long serialNumber = (long)epcFields[Constants.Constants.FIELD_SERIAL_NUMBER];
            long productId = (long)epcFields[Constants.Constants.FIELD_ITEM_REFERENCE]; 
            string companyPrefix = (string)epcFields[Constants.Constants.FIELD_COMPANY_PREFIX];

            companyPrefix = companyPrefix.PadLeft(minCompanyLenght, '0');

            try
            {
                Epc = Pack(filter, companyPrefix, productId, serialNumber, indicatorDigit);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to encode tag: {serialNumber}", ex);
            }
        }

        public SGTIN96(string companyPrefix, long productId, long serial, int filter = 1, int indicatorDigit = 0)
            : this()
        {
            companyPrefix = companyPrefix.PadLeft(minCompanyLenght, '0');
            try
            {
                Epc = Pack(filter, companyPrefix, productId, serial, indicatorDigit);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to encode tag: {serial}", ex);
            }
        }

        #endregion

        #region Properties

        public byte[] Epc { get; }

        #endregion

        #region Methods

        public string Code()
        {
            var partition = Partition();

            var company = Company()
                .ToString()
                .PadLeft(12 - partition, '0');

            var indicatorItemRef = Indicator_ItemReference()
                .ToString()
                .PadLeft(partition + 1, '0');

            var code = $"{indicatorItemRef[0]}{company}{indicatorItemRef.Substring(1)}";

            var sum1 = (code[0] - '0') + (code[2] - '0') + (code[4] - '0') + (code[6] - '0') + (code[8] - '0') + (code[10] - '0') + (code[12] - '0');
            var sum2 = (code[1] - '0') + (code[3] - '0') + (code[5] - '0') + (code[7] - '0') + (code[9] - '0') + (code[11] - '0');
            var checkDigit = (10 - (((3 * sum1) + sum2) % 10)) % 10;

            return code + checkDigit;
        }

        public byte[] Pack(int filter, string companyPrefix, long productId, long serial, int indicatorDigit)
        {
            if (indicatorDigit > 10)
            {
                throw new Exception($"IndicatorDigit > 10 : {indicatorDigit}");
            }

            if (filter < 0 || filter > 8)
            {
                throw new Exception($"! 0 < Filter > 8 : {filter}");
            }

            long lngCompanyPrefix = long.Parse(companyPrefix);

            var partition = DeterminePartition(companyPrefix);

            int m = bCompanyRange[partition];
            int n = bItemRange[partition];

            string h = "00110000";
            string f = Convert.ToString(filter, 2).PadLeft(3, '0');
            string p = Convert.ToString(partition, 2).PadLeft(3, '0');
            string c = Convert.ToString(lngCompanyPrefix, 2).PadLeft(m, '0');
            string s = Convert.ToString(serial, 2).PadLeft(38, '0');

            string decimalValue = GetExtensionDigit(partition, productId, indicatorDigit);

            // now take that new number we created and make it a binary string
            // representation
            string i = Convert.ToString(long.Parse(decimalValue), 2).PadLeft(n, '0');

            string pack = (h + f + p + c + i + s).PadRight(96, '0');

            return ToRawBitSet(Encoding.UTF8.GetBytes(pack));
        }

        public long SerialNumber()
        {
            long value = Epc[7] & 0b0011_1111;
            value = (value << 8) | Epc[8];
            value = (value << 8) | Epc[9];
            value = (value << 8) | Epc[10];
            value = (value << 8) | Epc[11];
            return value;
        }

        public bool IsValid()
        {
            return Epc != null
                && Epc.Length == 12
                && Header() == 0b0011_0000
                && Partition() != 0b0000_0111
                && Code().Length <= maxGtinPlusItemValue;
        }

        byte Header()
        {
            return Epc[0];
        }

        int Filter()
        {
            return Epc[1] >> 5;
        }

        int Partition()
        {
            return (Epc[1] >> 2) & 0b0000_0111;
        }

        long Company()
        {
            var partition = Partition();

            long value = Epc[1] & 0b0000_0011;
            value = (value << 8) | Epc[2];
            value = (value << 8) | Epc[3];
            value = (value << 8) | Epc[4];

            switch (partition)
            {
                case 0:
                    value = (value << 8) | Epc[5];
                    value = (value << 8) | Epc[6];
                    value >>= 2;
                    break;
                case 1:
                    value = (value << 8) | Epc[5];
                    value = (value << 8) | Epc[6];
                    value >>= 5;
                    break;
                case 2:
                    value = (value << 8) | Epc[5];
                    break;
                case 3:
                    value = (value << 8) | Epc[5];
                    value >>= 4;
                    break;
                case 4:
                    value = (value << 8) | Epc[5];
                    value >>= 7;
                    break;
                case 5:
                    value >>= 2;
                    break;
                case 6:
                    value >>= 6;
                    break;
                default:
                    throw new Exception("Invalid partition");
            }

            return value;
        }

        int Indicator_ItemReference()
        {
            var partition = Partition();

            var value = 0;
            switch (partition)
            {
                case 0:
                    value = Epc[6] & 0b0000_0011;
                    break;
                case 1:
                    value = Epc[6] & 0b0001_1111;
                    break;
                case 2:
                    value = Epc[6];
                    break;
                case 3:
                    value = Epc[5] & 0b0000_1111;
                    value = (value << 8) | Epc[6];
                    break;
                case 4:
                    value = Epc[5] & 0b0111_1111;
                    value = (value << 8) | Epc[6];
                    break;
                case 5:
                    value = Epc[4] & 0b0000_0011;
                    value = (value << 8) | Epc[5];
                    value = (value << 8) | Epc[6];
                    break;
                case 6:
                    value = Epc[4] & 0b0011_1111;
                    value = (value << 8) | Epc[5];
                    value = (value << 8) | Epc[6];
                    break;
                default:
                    throw new Exception("Invalid partition");
            }

            value = (value << 8) | Epc[7];
            value >>= 6;

            return value;
        }

        public long Sku()
        {
            return Indicator_ItemReference();
        }

        /*
         * com/vue/epcglobal/iteminterface/encoding/SGTIN96EpcTag.java
         */

        #region Product Library

        int DeterminePartition(string companyPrefix)
        {
            return LookUpPartitionByCompanyPrefixLength(companyPrefix);
        }

        int LookUpPartitionByCompanyPrefixLength(string strCompanyPrefix)
        {
            if (strCompanyPrefix.Length < minCompanyLenght)
            {
                throw new Exception("CompPfx out of range");
            }

            int partitionLocal = 0;
            int i = 0;
            for (i = 0; i < companyRange.Length; i++)
            {
                if (companyRange[i] == strCompanyPrefix.Length)
                {
                    partitionLocal = i;
                    break;
                }
            }

            return partitionLocal;
        }

        string GetExtensionDigit(int partition, long productId, int indicatorDigit)
        {
            string decimalValue;
            string itemReference = productId.ToString();
            if (partition != 0)
            {
                decimalValue = indicatorDigit + itemReference.PadLeft(itemRange[partition] - 1, '0');
            }
            else
            {
                // No space for item if partiton is 0.
                decimalValue = indicatorDigit.ToString();
            }

            return decimalValue;
        }

        byte[] ToRawBitSet(byte[] bitSet)
        {
            // get length/8 times bytes with 3 bit shifts to the right of the length
            byte[] raw = new byte[bitSet.Length >> 3];

            for (int ii = 0, jj = 0; ii < raw.Length; ii++, jj += 8)
            {
                if (bitSet[jj] == '1')
                {
                    raw[ii] |= bit7;
                }

                if (bitSet[jj + 1] == '1')
                {
                    raw[ii] |= bit6;
                }

                if (bitSet[jj + 2] == '1')
                {
                    raw[ii] |= bit5;
                }

                if (bitSet[jj + 3] == '1')
                {
                    raw[ii] |= bit4;
                }

                if (bitSet[jj + 4] == '1')
                {
                    raw[ii] |= bit3;
                }

                if (bitSet[jj + 5] == '1')
                {
                    raw[ii] |= bit2;
                }

                if (bitSet[jj + 6] == '1')
                {
                    raw[ii] |= bit1;
                }

                if (bitSet[jj + 7] == '1')
                {
                    raw[ii] |= bit0;
                }
            }

            return raw;
        }

        public string RawTagValue()
        {
            return Epc.ToHexString();
        }

        #endregion

        #endregion
    }
}
