/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TrueVUE.Cloud.SDK.Collections
{
    public class SectionModel<THeader, TItem> : ISectionModel<THeader, TItem>
    {
        #region Constructors

        public SectionModel()
        {
        }

        public SectionModel(THeader header, IEnumerable<TItem> items)
        {
            Header = header;
            Items = items.ToImmutableList();
        }

        #endregion

        #region Properties

        public THeader Header { get; private set; }

        public IImmutableList<TItem> Items { get; private set; }

        #endregion

        #region Methods

        public void Initialize(THeader header, IEnumerable<TItem> items)
        {
            Header = header;
            Items = items.ToImmutableList();
        }

        public void Initialize(THeader header, SelectableCollection<TItem> items)
        {
            Header = header;
            Items = items.Select(a => a.Item).ToImmutableList(); 
        }

        #endregion
    }
}
