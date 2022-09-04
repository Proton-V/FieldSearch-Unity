using FieldSearch.Helpers.Cache.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace FieldSearch.Helpers.Cache
{
	public class SearchInspectorCache
	{
		public static float GetCurrentSize() => inspectorsDict.Sum(x => Marshal.SizeOf(x.Value));

		protected static Dictionary<int, SearchCacheObj> inspectorsDict
			= new Dictionary<int, SearchCacheObj>();

		public static void AddValue(SearchCacheObj val)
		{
			var id = val.id;

			if (inspectorsDict.ContainsKey(id))
			{
				inspectorsDict[id] = val;
			}
			else
			{
				inspectorsDict.Add(id, val);
			}
		}

		public static void RemoveValue(int id)
		{
			if (inspectorsDict.ContainsKey(id))
			{
				inspectorsDict.Remove(id);
			}
		}

		public static SearchCacheObj TryGetValue(int id)
		{
			SearchCacheObj res;
			inspectorsDict.TryGetValue(id, out res);
            return res;
		}
	}
}
