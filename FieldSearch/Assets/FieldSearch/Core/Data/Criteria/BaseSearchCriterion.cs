using FieldSearch.Helpers.StringFormatter;
using System;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Data.Criteria.Base
{
	public abstract class BaseSearchCriterion
	{
		public BaseSearchCriterion()
		{
			Init();
		}

		protected SearchFilter criterionFilter;

		public abstract bool HasResult<T>(SearchFilter currentFlags, params T[] input);

		protected abstract SearchFilter GetCriterionSearchFilter();

		protected bool Compare(string name, string rawSearchText, SearchFilter currentSearchFilter)
        {
			var finalString = SearchStringFormatter.GetFinalString(name, currentSearchFilter);
			var finalSearchText = SearchStringFormatter.GetFinalString(rawSearchText, currentSearchFilter);

			var startWith = criterionFilter.HasFlag(SearchFilter.StartWith);
			return startWith ?
			finalString.StartsWith(finalSearchText)
			: finalString.Contains(finalSearchText);
		}

		protected virtual bool IsActive(SearchFilter currentFlags)
        {
			return currentFlags.HasFlag(criterionFilter);
		}

		protected virtual void Init()
        {
			this.criterionFilter = GetCriterionSearchFilter();
		}
	}
}
