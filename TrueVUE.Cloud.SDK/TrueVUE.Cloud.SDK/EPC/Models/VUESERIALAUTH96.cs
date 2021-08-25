/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using TrueVUE.Cloud.SDK.Extensions;

namespace TrueVUE.Cloud.SDK.Epc.Models.Epc
{
    struct VUESERIALAUTH96 : IItemEpc
    {
        #region fields

        const int PRODUCT_ID_BIT_LENGTH = 48;

        const int SERIAL_BIT_LENGTH = 32;

        const int AUTHORITY_BIT_LENGTH = 4;

        const long MAX_PRODUCT_ID = (1L << PRODUCT_ID_BIT_LENGTH) - 1;

        const long MAX_SERIAL = (1L << SERIAL_BIT_LENGTH) - 1;

        const int OPTION_BIT_LENGTH = 4;

        const byte VUESERAUTH_CLOUD_AUTHORITY = 0x0;

        static readonly EpcFieldDefinition[] fields =
        {
            new EpcFieldDefinition(Constants.Constants.FIELD_ITEM_REFERENCE, 0, 15, "Product Value", true),
            new EpcFieldDefinition(Constants.Constants.FIELD_SERIAL_NUMBER, 0, 15, "Serial Number", true),
        };

        #endregion

        #region Constructors

        public VUESERIALAUTH96(byte[] epc)
        {
            Epc = epc;
        }

        public VUESERIALAUTH96(long productId, long serialNumber)
            : this()
        {
            if (productId > MAX_PRODUCT_ID)
            {
                throw new Exception("Invalid product id for this tag type: Vueser96 ");
            }

            if (serialNumber > MAX_SERIAL)
            {
                throw new Exception("Invalid serial for this tag type Vueser96");
            }

            Epc = Pack(productId, serialNumber);
        }

        public VUESERIALAUTH96(Dictionary<string, object> epcFields)
            : this()
        {
            // validate fields contain required fields
            if (!fields.Where(w => w.IsRequired).All(f => epcFields.ContainsKey(f.FieldName)))
            {
                throw new KeyNotFoundException("Missing required field(s)");
            }

            var productId = (long)epcFields[Constants.Constants.FIELD_ITEM_REFERENCE];
            var serialNumber = (long)epcFields[Constants.Constants.FIELD_SERIAL_NUMBER];

            if (productId > MAX_PRODUCT_ID)
            {
                throw new Exception("Invalid product id for this tag type: Vueser96 ");
            }

            if (serialNumber > MAX_SERIAL)
            {
                throw new Exception("Invalid serial for this tag type Vueser96");
            }

            Epc = Pack(productId, serialNumber);
        }

        #endregion

        #region Properties

        public byte[] Epc { get; }

        #endregion

        #region Methods

        byte[] Pack(long license, long serialNumber)
        {
            var tagBytes = new byte[12];

            tagBytes[0] = Constants.Header.VUE;
            tagBytes[1] = Constants.Option.VUESERIALAUTH_CLOUD << 4;
            // AUTHORITY constant is 0000 so obmitting to add those 4 bits to byte array possition  1

            tagBytes[2] = (byte)(((license & 0xFF0000000000L) >> 40) & 0xFF);
            tagBytes[3] = (byte)(((license & 0x00FF00000000L) >> 32) & 0xFF);
            tagBytes[4] = (byte)(((license & 0x0000FF000000L) >> 24) & 0xFF);
            tagBytes[5] = (byte)(((license & 0x000000FF0000L) >> 16) & 0xFF);
            tagBytes[6] = (byte)(((license & 0x00000000FF00L) >> 8) & 0xFF);
            tagBytes[7] = (byte)((license & 0x0000000000FFL) & 0xFF);

            tagBytes[8] = (byte)(((serialNumber & 0xFF000000L) >> 24) & 0xFF);
            tagBytes[9] = (byte)(((serialNumber & 0x00FF0000L) >> 16) & 0xFF);
            tagBytes[10] = (byte)(((serialNumber & 0x0000FF00L) >> 8) & 0xFF);
            tagBytes[11] = (byte)((serialNumber & 0x000000FFL) & 0xFF);

            return tagBytes;
        }

        int Option()
        {
            return (Epc[1] >> 4) & 0xf;
        }

        byte Header()
        {
            return Epc[0];
        }

        #endregion

        #region IItemEpc

        public string Code()
        {
            return Sku().ToString();
        }

        public bool IsValid()
        {
            return Epc != null
                && Epc.Length == 12
                      && Header() == Constants.Header.VUE
                      && (Option() == Constants.Option.VUESERIALAUTH_CLOUD
                          || Option() == Constants.Option.VUESERIALAUTH_LEGACY);
        }

        public string RawTagValue()
        {
            return Epc.ToHexString();
        }

        public long SerialNumber()
        {
            long serial = 0;

            serial = (serial << 8) | ((long)Epc[8] & 0xFF);
            serial = (serial << 8) | ((long)Epc[9] & 0xFF);
            serial = (serial << 8) | ((long)Epc[10] & 0xFF);
            serial = (serial << 8) | ((long)Epc[11] & 0xFF);

            return serial;
        }

        public long Sku()
        {
            long license = 0;
            
            license = (license << 8) | ((long)Epc[2] & 0xFF);
            license = (license << 8) | ((long)Epc[3] & 0xFF);
            license = (license << 8) | ((long)Epc[4] & 0xFF);
            license = (license << 8) | ((long)Epc[5] & 0xFF);
            license = (license << 8) | ((long)Epc[6] & 0xFF);
            license = (license << 8) | ((long)Epc[7] & 0xFF);

            return license;
        }

        #endregion
    }
}
