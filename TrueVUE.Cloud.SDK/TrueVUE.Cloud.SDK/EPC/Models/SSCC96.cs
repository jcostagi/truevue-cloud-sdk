/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;

namespace TrueVUE.Cloud.SDK.Epc
{
    // ===================
    // Binary Coding Table
    // ===================
    //
    // Header | Filter | Partition | Company | Extension / Serial | (Reserved)
    // -------|--------|-----------|---------|--------------------|-----------
    //    8   |    3   |     3     |  20-40  |       38-18        |    24

    // ===============
    // Partition Table
    // ===============
    //
    // Partition |   Company     |  Extension / Serial
    // ----------|------|--------|----------|----------
    //           | Bits | Digits |   Bits   |  Digits
    // ----------|------|--------|----------|----------
    //     0     |  40  |   12   |    18    |     5
    //     1     |  37  |   11   |    21    |     6
    //     2     |  34  |   10   |    24    |     7
    //     3     |  30  |    9   |    28    |     8
    //     4     |  27  |    8   |    31    |     9
    //     5     |  24  |    7   |    34    |    10
    //     6     |  20  |    6   |    38    |    11

    struct SSCC96 : IContainerEpc
    {
        #region Constructors

        public SSCC96(byte[] epc)
        {
            Epc = epc;
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

            var extensionSerial = Extension_Serial()
                .ToString()
                .PadLeft(partition + 5, '0');

            var code = $"{extensionSerial[0]}{company}{extensionSerial.Substring(1)}";

            var sum1 = (code[0] - '0') + (code[2] - '0') + (code[4] - '0') + (code[6] - '0') + (code[8] - '0') + (code[10] - '0') + (code[12] - '0') + (code[14] - '0') + (code[16] - '0');
            var sum2 = (code[1] - '0') + (code[3] - '0') + (code[5] - '0') + (code[7] - '0') + (code[9] - '0') + (code[11] - '0') + (code[13] - '0') + (code[15] - '0');
            var checkDigit = (10 - (((3 * sum1) + sum2) % 10)) % 10;

            return code + checkDigit;
        }

        public bool IsValid()
        {
            return Epc != null
                && Epc.Length == 12
                && Header() == 0b0011_0001
                && Partition() != 0b0000_0111;
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

        long Extension_Serial()
        {
            var partition = Partition();

            var value = 0L;
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
            value = (value << 8) | Epc[8];

            return value;
        }

        int Reserved()
        {
            int value = Epc[9];
            value = (value << 8) | Epc[10];
            value = (value << 8) | Epc[11];

            return value;
        }

        #endregion
    }
}
