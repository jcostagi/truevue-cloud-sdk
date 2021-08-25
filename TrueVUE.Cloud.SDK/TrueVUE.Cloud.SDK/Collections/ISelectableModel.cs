/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Collections
{
    public interface ISelectableModel<THeader, TItem>
    {
        THeader Header { get; }

        TItem Items { get; }

        void Initialize(THeader header, TItem items);
    }
}
