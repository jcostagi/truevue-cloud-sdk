/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.ObjectModel;

namespace TrueVUE.Cloud.SDK.Collections
{
    public class SelectableCollection<T> : KeyedCollection<T, Selectable<T>>
    {
        protected override T GetKeyForItem(Selectable<T> item)
        {
            return item.Item;
        }
    }
}
