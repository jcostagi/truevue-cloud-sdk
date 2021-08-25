/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using TrueVUE.Cloud.SDK.Barcode.Interfaces;

namespace TrueVUE.Cloud.SDK.Barcode.Models
{
    class DefaultBarcode : IBarcode
    {
        #region Methods

        public string Barcode { get; private set; }

        public string CompanyPrefix => null;

        public long ItemReference => -1;

        public bool IsValid => true;

        #endregion

        #region Constructors

        public DefaultBarcode(string barcode)
        {
            Barcode = barcode;
        }

        #endregion
    }
}
