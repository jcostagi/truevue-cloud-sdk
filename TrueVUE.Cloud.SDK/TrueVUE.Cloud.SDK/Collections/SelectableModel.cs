/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Collections
{
    public class SelectableModel<THeader, TItem> : ISelectableModel<THeader, TItem>
    {
        #region Constructors

        public SelectableModel(THeader header, TItem items)
        {
            Header = header;
            Items = items;
        }

        #endregion

        #region Properties

        public THeader Header { get; private set; }

        public TItem Items { get; private set; }

        #endregion

        #region Methods

        public void Initialize(THeader header, TItem items)
        {
            Header = header;
            Items = items;
        }

        #endregion
    }
}
