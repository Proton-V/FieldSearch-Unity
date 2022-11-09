using FieldSearch.Helpers.StringFormatter;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Data.Criteria.Base
{
	/// <summary>
	/// Base search criterion for <see cref="Core.Base.BaseSearch"/>
	/// </summary>
	public abstract class BaseSearchCriterion
	{
		public BaseSearchCriterion()
		{
			Init();
		}

		/// <summary>
		/// Criterion <see cref="SearchFilter"/> flag
		/// </summary>
		protected SearchFilter criterionFilter;

		/// <summary>
		/// Check results
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="currentFlags"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		public abstract bool HasResult<T>(SearchFilter currentFlags, params T[] input);

		/// <summary>
		/// Get Criterion <see cref="SearchFilter"/> flag
		/// </summary>
		/// <returns></returns>
		protected abstract SearchFilter GetCriterionSearchFilter();

		/// <summary>
		/// Compare result with search text
		/// </summary>
		/// <param name="name"></param>
		/// <param name="rawSearchText"></param>
		/// <param name="currentSearchFilter"></param>
		/// <returns></returns>
		protected bool Compare(string name, string rawSearchText, SearchFilter currentSearchFilter)
        {
			var finalString = SearchStringFormatter.GetFinalString(name, currentSearchFilter);
			var finalSearchText = SearchStringFormatter.GetFinalString(rawSearchText, currentSearchFilter);

			var startWith = criterionFilter.HasFlag(SearchFilter.StartWith);
			return startWith ?
			finalString.StartsWith(finalSearchText)
			: finalString.Contains(finalSearchText);
		}

		/// <summary>
		/// If criterion is active for current search flag
		/// </summary>
		/// <param name="currentFlags"></param>
		/// <returns></returns>
		protected virtual bool IsActive(SearchFilter currentFlags)
        {
			return currentFlags.HasFlag(criterionFilter);
		}

		/// <summary>
		/// Init method with base logic
		/// </summary>
		protected virtual void Init()
        {
			this.criterionFilter = GetCriterionSearchFilter();
		}
	}
}
