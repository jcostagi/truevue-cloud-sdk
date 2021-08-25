/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Collections
{
    public class Expandable<T>
    {
        #region Constructors

        public Expandable(T item)
        {
            Item = item;
        }

        #endregion

        #region Properties

        public T Item { get; }

        public bool IsExpanded { get; set; }

        public bool IsEnabled { get; set; } = true;

        #endregion
    }
}
