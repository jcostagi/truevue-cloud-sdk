/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrueVUE.Cloud.Models;
using TrueVUE.Cloud.SDK.Epc.Interfaces;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class FilterExtensions
    {
        #region Methods

        public static string GetFilterQuery(this IEnumerable<IFilter> filters, bool includeWhere = true)
        {
			if (filters == null)
			{
                return string.Empty;
            }

			StringBuilder filtersBuilder = new StringBuilder();

            filters = filters.Where(filterItem => filterItem.SelectedOptions.Any() && (string.IsNullOrEmpty(filterItem.Type) || !filterItem.Type.Equals(AttributeType.RECIPE_LOOKUP.ToString())));

			if (filters.Any())
			{
				if (includeWhere)
				{
					filtersBuilder.Append(" WHERE ");
				}

				filtersBuilder.Append(
					string.Join(
						" AND ",
						filters.Select(f =>
						{
							StringBuilder filterBuilder = new StringBuilder();
							if (f.Include)
							{
								filterBuilder.Append($" [{f.Id}] IN ");
							}
							else
							{
								filterBuilder.Append($" coalesce([{f.Id}], '') NOT IN ");
							}

							filterBuilder.Append($"({string.Join(", ", f.SelectedOptions.Select(option => f.IsBlob ? $"{option}" : $"'{option.SqlEscape()}'").ToArray())})");
							return filterBuilder.ToString();
						}).ToArray()));
            }

            return filtersBuilder.ToString();
        }

        #endregion
    }
}   
