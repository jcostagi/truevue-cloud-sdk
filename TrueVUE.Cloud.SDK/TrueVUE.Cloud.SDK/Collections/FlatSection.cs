/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Collections
{
    public class FlatSection<THeader, TItem>
    {
        #region Constructors

        private FlatSection(int parentPosition, int originalPosition)
        {
            ParentPosition = parentPosition;
            OriginalPosition = originalPosition;
        }

        private FlatSection(THeader header, int parentPosition, int originalPosition)
            : this(parentPosition, originalPosition)
        {
            Header = header;
        }

        private FlatSection(TItem item, int parentPosition, int originalPosition)
            : this(parentPosition, originalPosition)
        {
            Item = item;
        }

        #endregion

        #region Properties

        public THeader Header { get; }

        public TItem Item { get; }

        public bool IsHeader => Header != null;

        public bool IsItem => Item != null;

        public int ParentPosition { get; }

        public int OriginalPosition { get; }

        #endregion

        #region Builders

        public static FlatSection<THeader, TItem> FromHeader(THeader header, int parentPosition, int originalPosition)
        {
            return new FlatSection<THeader, TItem>(header, parentPosition, originalPosition);
        }

        public static FlatSection<THeader, TItem> FromItem(TItem item, int parentPosition, int originalPosition)
        {
            return new FlatSection<THeader, TItem>(item, parentPosition, originalPosition);
        }

        #endregion
    }
}
