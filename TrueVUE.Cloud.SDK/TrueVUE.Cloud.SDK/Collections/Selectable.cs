/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;

namespace TrueVUE.Cloud.SDK.Collections
{
    public class Selectable<T> : IEquatable<Selectable<T>>
    {
        #region Constructors

        public Selectable(T item)
        {
            Item = item;
        }

        #endregion

        #region Properties

        public T Item { get; }

        public bool IsSelected { get; set; }

        public bool IsEnabled { get; set; } = true;

        public bool IsSelectable { get; set; } = true;

        #endregion

        #region Methods

        public bool Equals(Selectable<T> other)
        {
            return Item.Equals(other.Item);
        }

        #endregion
    }
}
