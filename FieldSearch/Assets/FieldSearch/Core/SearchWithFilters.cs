using FieldSearch.Core.Base;
using FieldSearch.Core.Data.Criteria;
using UnityEngine;

namespace FieldSearch.Core
{
	/// <summary>
	/// Default search with filters
	/// </summary>
	public class SearchWithFilters : BaseSearch
	{
		public SearchWithFilters(SearchFilter currentFlags) : base(currentFlags)
		{
			CreateCriteria(currentFlags);
		}

		protected override bool CreateCriteria(SearchFilter currentFlags)
		{
			try
			{
				if (currentFlags.HasFlag(ByFieldNameSearchCriterion.CRITERION_SEARCH_FILTER))
                {
					AddCriterion(new ByFieldNameSearchCriterion());
                }

                if(currentFlags.HasFlag(ByObjNameSearchCriterion.CRITERION_SEARCH_FILTER))
                {
					AddCriterion(new ByObjNameSearchCriterion());
				}
				return true;
			}
			catch (System.Exception e)
			{
				Debug.LogError($"CreateCriteria fail: {e}");
				return false;
			}
		}
	}
}
