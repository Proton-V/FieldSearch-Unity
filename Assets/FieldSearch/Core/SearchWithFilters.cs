using FieldSearch.Core.Base;
using FieldSearch.Data.Criteria;
using UnityEngine;

namespace FieldSearch.Core
{
	public class SearchWithFilters : BaseSearch
	{
		public SearchWithFilters(ref SearchFilter searchFilter) : base()
		{
			CreateCriteria(ref searchFilter);
		}

		public override bool CreateCriteria(ref SearchFilter searchFilter)
		{
			try
			{
				if (searchFilter.HasFlag(SearchFilter.ByFieldName))
				{
					AddCriterion(new ByFieldNameSearchCriterion(ref searchFilter));
				}

				if (searchFilter.HasFlag(SearchFilter.ByObjName))
				{
					AddCriterion(new ByObjNameSearchCriterion(ref searchFilter));
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
