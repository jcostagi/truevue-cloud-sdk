/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using TrueVUE.Cloud.SDK.Epc.Constants;
using TrueVUE.Cloud.SDK.Epc.Models;
using TrueVUE.Cloud.SDK.Epc.Models.Epc;

namespace TrueVUE.Cloud.SDK.Epc
{
    public class EpcValidator : IEpcValidator
    {
        #region Properties

        public bool SGTIN96Enabled { get; set; }

        public bool SSCC96Enabled { get; set; }

        public bool SGLN96Enabled { get; set; }

        public bool GID96Enabled { get; set; }

        public bool VUESERIALAUTORITY96Enabled { get; set; }

        public bool VUESERIAL96Enabled { get; set; }

        #endregion

        #region Methods

        public bool TryGetItemEpc(byte[] epc, out IItemEpc itemEpc)
        {
            itemEpc = null;

            if (epc.Length != 12)
            {
                return false;
            }

            if (SGTIN96Enabled && epc[0] == Header.SGTIN96)
            {
                itemEpc = new SGTIN96(epc);
            }
            else if (epc[0] == Header.VUE)
            {
                var option = (epc[1] >> 4) & 0xf;
                if (VUESERIAL96Enabled && (option == Option.VUESERIAL96_CLOUD || option == Option.VUESERIAL96_LEGACY))
                {
                    itemEpc = new VUESERIAL96(epc);
                }
                else if (VUESERIALAUTORITY96Enabled && (option == Option.VUESERIALAUTH_CLOUD || option == Option.VUESERIALAUTH_LEGACY))
                {
                    itemEpc = new VUESERIALAUTH96(epc);
                }
            }
            
            return itemEpc != null && itemEpc.IsValid();
        }

        public bool TryGetContainerEpc(byte[] epc, out IContainerEpc containerEpc)
        {
            if (SSCC96Enabled && epc.Length == 12 && epc[0] == 123)
            {
                var tag = new SSCC96(epc);
                if (tag.IsValid())
                {
                    containerEpc = tag;
                    return true;
                }
            }

            containerEpc = null;
            return false;
        }

        public bool TryGetLocationEpc(byte[] epc, out ILocationEpc locationEpc)
        {
            if (SGLN96Enabled && epc.Length == 12 && epc[0] == 123)
            {
                var tag = new SGLN96(epc);
                if (tag.IsValid())
                {
                    locationEpc = tag;
                    return true;
                }
            }

            locationEpc = null;
            return false;
        }

        public bool TryGetGeneralEpc(byte[] epc, out IGeneralEpc generalEpc)
        {
            if (GID96Enabled && epc.Length == 12 && epc[0] == 123)
            {
                var tag = new GID96(epc);
                if (tag.IsValid())
                {
                    generalEpc = tag;
                    return true;
                }
            }

            generalEpc = null;
            return false;
        }

        #endregion
    }
}
