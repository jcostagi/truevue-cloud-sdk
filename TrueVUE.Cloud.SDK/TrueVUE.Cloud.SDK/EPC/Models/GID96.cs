/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Epc
{
    // ===================
    // Binary Coding Table
    // ===================
    //
    // Header | General Manager Num | Object Class | Serial
    // -------|---------------------|--------------|-------
    //    8   |          28         |      24      |   36

    struct GID96 : IGeneralEpc
    {
        #region Constructors

        public GID96(byte[] epc)
        {
            Epc = epc;
        }

        #endregion

        #region Properties

        public byte[] Epc { get; }

        #endregion

        #region Methods

        public int GeneralManagerNumber()
        {
            int value = Epc[1];
            value = (value << 8) | Epc[2];
            value = (value << 8) | Epc[3];
            value = (value << 8) | Epc[4];
            value >>= 4;

            return value;
        }

        public int ObjectClass()
        {
            int value = Epc[4] & 0b0000_1111;
            value = (value << 8) | Epc[5];
            value = (value << 8) | Epc[6];
            value = (value << 8) | Epc[7];
            value >>= 4;

            return value;
        }

        public long SerialNumber()
        {
            long value = Epc[7] & 0b0000_1111;
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
                && Header() == 0b0011_0101;
        }

        byte Header()
        {
            return Epc[0];
        }

        #endregion
    }
}
