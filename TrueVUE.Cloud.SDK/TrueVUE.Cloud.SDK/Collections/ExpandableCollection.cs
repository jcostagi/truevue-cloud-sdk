/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.ObjectModel;

namespace TrueVUE.Cloud.SDK.Collections
{
    public class ExpandableCollection<T> : KeyedCollection<T, Expandable<T>>
    {
        protected override T GetKeyForItem(Expandable<T> item)
        {
            return item.Item;
        }
    }
}
