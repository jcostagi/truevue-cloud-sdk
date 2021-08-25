/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Epc
{
    public interface IEpcValidator
    {
        bool TryGetItemEpc(byte[] epc, out IItemEpc itemEpc);

        bool TryGetContainerEpc(byte[] epc, out IContainerEpc containerEpc);

        bool TryGetLocationEpc(byte[] epc, out ILocationEpc locationEpc);

        bool TryGetGeneralEpc(byte[] epc, out IGeneralEpc generalEpc);
    }
}
