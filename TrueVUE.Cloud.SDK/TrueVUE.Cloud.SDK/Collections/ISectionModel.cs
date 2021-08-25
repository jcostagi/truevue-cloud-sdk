/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.Generic;
using System.Collections.Immutable;

namespace TrueVUE.Cloud.SDK.Collections
{
    public interface ISectionModel<THeader, TItem>
    {
        THeader Header { get; }

        IImmutableList<TItem> Items { get; }

        void Initialize(THeader header, IEnumerable<TItem> items);
    }
}
