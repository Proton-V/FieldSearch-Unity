using UnityEngine;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Helpers
{
	public class SearchStringFormatter
	{
		public static string GetFinalString(string str, bool ignoreCase = false)
		{
			return ignoreCase ? str.ToLower() : str;
		}

		public static string GetFinalString(string str, SearchFilter searchFilter = default)
		{
			var ignoreCase = false;
			if (searchFilter != default)
            {
				ignoreCase = searchFilter.HasFlag(SearchFilter.IgnoreCase); ;
			}

			return ignoreCase ? str.ToLower() : str;
		}
	}
}
