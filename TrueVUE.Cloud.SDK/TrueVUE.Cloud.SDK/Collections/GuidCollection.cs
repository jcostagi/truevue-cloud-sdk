/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.ObjectModel;

namespace TrueVUE.Cloud.SDK.Collections
{
    public class GuidCollection : KeyedCollection<Guid, Guid>
    {
        protected override Guid GetKeyForItem(Guid item)
        {
            return item;
        }
    }
}
