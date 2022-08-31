using FieldSearch.Data.Criteria.Base;
using FieldSearch.Helpers;
using UnityEditor;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Data.Criteria
{
	public class ByFieldNameSearchCriterion : BaseSearchCriterion
	{
		public ByFieldNameSearchCriterion(ref SearchFilter searchFilter)
			: base(ref searchFilter) { }

		protected bool StartWith => searchFilter.HasFlag(SearchFilter.StartWith);
		protected bool ByFieldName => searchFilter.HasFlag(SearchFilter.ByFieldName);

		public override bool HasResult<T>(params T[] input)
		{
			var rawSearchText = input[0] as string;
			if(rawSearchText is null)
            {
				return false;
            }

			var finalSearchText = SearchStringFormatter.GetFinalString(rawSearchText, searchFilter);
			var serializedProperty = input[1] as SerializedProperty;

			if (serializedProperty == null)
			{
				return false;
			}

			if (ByFieldName)
			{
				return StartWith ?
				SearchStringFormatter.GetFinalString(serializedProperty.displayName, searchFilter)
				.StartsWith(finalSearchText)
				: SearchStringFormatter.GetFinalString(serializedProperty.name, searchFilter)
				.Contains(finalSearchText);
			}

			return false;
		}
	}
}
