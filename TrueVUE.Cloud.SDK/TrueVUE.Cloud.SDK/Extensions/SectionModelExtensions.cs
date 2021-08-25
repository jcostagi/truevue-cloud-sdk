/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.Generic;
using TrueVUE.Cloud.SDK.Collections;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class SectionModelExtensions
    {
        #region Methods

        public static int ToFlatIndex<TSection, TItem>(this IEnumerable<ISectionModel<TSection, TItem>> sectionModels, int parentPosition, int childPosition)
        {
            int currentIndex = 0;
            int currentHeader = 0;

            foreach (var sectionModel in sectionModels)
            {
                if (parentPosition == currentHeader)
                {
                    currentIndex += childPosition;
                    return currentIndex;
                }

                currentHeader += 1;
                currentIndex += sectionModel.Items?.Count ?? 0;
            }

            return -1;
        }

        public static int ToVisibleFlatIndex<THeader, TItem>(this IList<ISectionModel<Expandable<THeader>, TItem>> sectionModels, (int parent, int child) position)
        {
            int count = 0;
            int headersPosition = 0;
            for (int i = 0; i <= position.parent; i++)
            {
                if (!sectionModels[headersPosition].Header.IsExpanded)
                {
                    headersPosition++;
                }
                else if (sectionModels[headersPosition].Header.IsExpanded && i != position.parent)
                {
                    count += sectionModels[i].Items.Count;
                    headersPosition++;
                }
                else
                {
                    count += position.child + ++headersPosition;
                }
            }

            return count;
        }

        public static (int parentPosition, int childPosition) ToParentChildIndex<TSection, TItem>(this IEnumerable<ISectionModel<TSection, TItem>> sectionModels, int index)
        {
            int parentPosition = 0;
            int childPosition;

            int currentIndex = 0;

            foreach (var sectionModel in sectionModels)
            {
                childPosition = 0;
                foreach (var child in sectionModel.Items)
                {
                    if (currentIndex == index)
                    {
                        return (parentPosition, childPosition);
                    }

                    currentIndex += 1;
                    childPosition += 1;
                }

                parentPosition += 1;
            }

            return (-1, -1);
        }

        #endregion
    }
}
