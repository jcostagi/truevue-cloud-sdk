/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Models
{
    public class ByteHeader
    {
        public byte[] Header { get; set; }

        public byte[] Mask { get; set; }
    }
}