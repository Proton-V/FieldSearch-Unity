using FieldSearch.Core.Data.Criteria.Base;
using System.Collections.Generic;
using System.Linq;

namespace FieldSearch.Core.Base
{
	/// <summary>
	/// Base search class with filter implementation
	/// </summary>
	public abstract class BaseSearch
	{
		[System.Flags]
		public enum SearchFilter
		{
			None = 0,
			StartWith = 1,
			IgnoreCase = 2,
			ByFieldName = 4,
			ByObjName = 8
		}

		public BaseSearch(SearchFilter currentFlags)
		{
			baseSearchCriteria = new List<BaseSearchCriterion>();
			CreateCriteria(currentFlags);
		}

		private List<BaseSearchCriterion> baseSearchCriteria;

        public void UpdateCriteria(SearchFilter currentFlags)
        {
            ClearCriteria();
            CreateCriteria(currentFlags);
        }

        protected abstract bool CreateCriteria(SearchFilter currentFlags);

		public bool GetResult(SearchFilter currentFlags, bool any = false, params object[] input)
		{
			return any ? baseSearchCriteria.Any(x => x.HasResult(currentFlags, input))
				: baseSearchCriteria.All(x => x.HasResult(currentFlags, input));
		}

		protected void AddCriterion(BaseSearchCriterion baseSearchCriterion)
		{
			baseSearchCriteria.Add(baseSearchCriterion);
		}

		public void ClearCriteria()
		{
			baseSearchCriteria.Clear();
		}
	}
}
