/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Barcode.Interfaces
{
    public interface IBarcode
    {
        string Barcode { get; }
        
        string CompanyPrefix { get; }

        long ItemReference { get; }

        bool IsValid { get; }
    }
}
