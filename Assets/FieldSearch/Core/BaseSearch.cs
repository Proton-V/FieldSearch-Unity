using FieldSearch.Data.Criteria.Base;
using FieldSearch.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace FieldSearch.Core.Base
{
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

		public BaseSearch()
		{
			baseSearchCriteria = new List<BaseSearchCriterion>();
		}

		private List<BaseSearchCriterion> baseSearchCriteria;

		public void UpdateCriteria(ref SearchFilter searchFilter)
		{
			ClearCriteria();
			CreateCriteria(ref searchFilter);
		}

		public abstract bool CreateCriteria(ref SearchFilter searchFilter);

		public bool GetResult(bool any = false, params object[] input)
		{
			return any ? baseSearchCriteria.Any(x => x.HasResult(input))
				: baseSearchCriteria.All(x => x.HasResult(input));
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
