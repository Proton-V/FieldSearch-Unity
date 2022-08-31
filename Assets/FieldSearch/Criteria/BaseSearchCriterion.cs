using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Data.Criteria.Base
{
	public abstract class BaseSearchCriterion
	{
		public BaseSearchCriterion(ref SearchFilter searchFilter)
		{
			Init(ref searchFilter);
		}

		protected SearchFilter searchFilter;

		protected virtual void Init(ref SearchFilter searchFilter)
        {
			this.searchFilter = searchFilter;
		}

		public abstract bool HasResult<T>(params T[] input);
	}
}
