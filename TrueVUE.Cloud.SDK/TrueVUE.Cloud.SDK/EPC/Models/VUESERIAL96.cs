/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using TrueVUE.Cloud.SDK.Extensions;

namespace TrueVUE.Cloud.SDK.Epc.Models
{
    struct VUESERIAL96 : IItemEpc
    {
        #region fields

        const int PRODUCT_ID_BIT_LENGTH = 50;

        const int SERIAL_BIT_LENGTH = 34;

        const long MAX_PRODUCT_ID = (1L << PRODUCT_ID_BIT_LENGTH) - 1;

        const long MAX_SERIAL = (1L << SERIAL_BIT_LENGTH) - 1;

        const int OPTION_BIT_LENGTH = 4;

        static EpcFieldDefinition[] fields =
        {
            new EpcFieldDefinition(Constants.Constants.FIELD_ITEM_REFERENCE, 0, 15, "Product Value", true),
            new EpcFieldDefinition(Constants.Constants.FIELD_SERIAL_NUMBER, 0, 15, "Serial Number", true),
        };

        #endregion

        #region Constructors

        public VUESERIAL96(byte[] epc)
        {
            Epc = epc;
        }

        public VUESERIAL96(long productId, long serialNumber)
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

        public VUESERIAL96(Dictionary<string, object> epcFields)
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

        byte[] Pack(long productId, long serial)
        {
            var tagBytes = new byte[12];
            tagBytes[0] = Constants.Header.VUE;
            tagBytes[1] = Constants.Option.VUESERIAL96_CLOUD << 4;
            // shift left by 2 to align license plate on byte boundary within tag
            long license = productId << 2;
            // top 4 bits of license into bottom 4 bits of byte 1
            tagBytes[1] |= (byte)(((license & 0x0F000000000000L) >> 48) & 0xFF);
            // copy license
            tagBytes[2] = (byte)(((license & 0x00FF0000000000L) >> 40) & 0xFF);
            tagBytes[3] = (byte)(((license & 0x0000FF00000000L) >> 32) & 0xFF);
            tagBytes[4] = (byte)(((license & 0x000000FF000000L) >> 24) & 0xFF);
            tagBytes[5] = (byte)(((license & 0x00000000FF0000L) >> 16) & 0xFF);
            tagBytes[6] = (byte)(((license & 0x0000000000FF00L) >> 8) & 0xFF);
            tagBytes[7] = (byte)(((license & 0x000000000000FFL)) & 0xFF);

            // top 2 bits of serial into bottom 2 bits of byte 7
            tagBytes[7] |= (byte)(((serial & 0x300000000L) >> 32) & 0xFF);

            // copy serial
            tagBytes[8] = (byte)(((serial & 0x0FF000000L) >> 24) & 0xFF);
            tagBytes[9] = (byte)(((serial & 0x000FF0000L) >> 16) & 0xFF);
            tagBytes[10] = (byte)(((serial & 0x00000FF00L) >> 8) & 0xFF);
            tagBytes[11] = (byte)(((serial & 0x0000000FFL)) & 0xFF);
            return tagBytes;
        }

        public string Code()
        {
            return Sku().ToString();
        }

        public long SerialNumber()
        {
            long serial = 0;
            serial |= (long)Epc[11] & 0xFF;
            serial |= (long)Epc[11] & 0xFF;
            serial |= ((long)Epc[10] << 8) & 0xFF00;
            serial |= (long)Epc[11] & 0xFF;
            serial |= ((long)Epc[9] << 16) & 0xFF0000;
            serial |= (long)Epc[11] & 0xFF;
            serial |= ((long)Epc[8] << 24) & 0xFF000000;
            serial |= (long)Epc[11] & 0xFF;
            serial |= ((long)Epc[7] << 32) & 0x0300000000L;

            // zero out high bits to prevent negatives
            serial &= 0x3FFFFFFFFL;
            return serial;
        }

        public long Sku()
        {
            // grab 52 bits from byte 7..1
            long license = 0;
            license |= (long) Epc[7] & 0xFF;
            license &= 0xFF;
            license |= ((long)Epc[6] << 8) & 0xFF00;
            license &= 0xFFFF;
            license |= ((long)Epc[5] << 16) & 0xFF0000;
            license &= 0xFFFFFF;
            license |= ((long)Epc[4] << 24) & 0xFF000000;
            license &= 0xFFFFFFFFL;
            license |= ((long)Epc[3] << 32) & 0xFF00000000L;
            license &= 0xFFFFFFFFFFL;
            license |= ((long)Epc[2] << 40) & 0xFF0000000000L;
            license &= 0xFFFFFFFFFFFFL;
            license |= ((long)Epc[1] << 48) & 0x0F000000000000L;
            license &= 0xFFFFFFFFFFFFFFL;

            // bottom two bits belong to serial, shift right
            license >>= 2;

            // zero out high bits to prevent negatives
            license &= 0x3FFFFFFFFFFFFFL;
            return license;
        }

        public bool IsValid()
        {
            return Epc != null
                && Epc.Length == 12
                      && Header() == Constants.Header.VUE
                      && (Option() == Constants.Option.VUESERIAL96_CLOUD
                          || Option() == Constants.Option.VUESERIAL96_LEGACY);
        }

        byte Header()
        {
            return Epc[0];
        }

        // options for vueser holds which type of vueser is acceptable
        // we should accept legacy and cloud
        int Option()
        {
            return (Epc[1] >> 4) & 0xf;
        }

        public string RawTagValue()
        {
            return Epc.ToHexString();
        }

        #endregion
    }
}
