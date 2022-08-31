using System.Collections.Generic;

namespace FieldSearch.Helpers
{
	public class SearchInspectorCache
	{
		private static Dictionary<int, (string searchText, int flags)> inspectorsDict
			= new Dictionary<int, (string searchText, int flags)>();

		public static void AddValue(int id, (string searchText, int flags) val)
		{
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

		public static (string searchText, int flags) TryGetValue(int id)
		{
			(string searchText, int flags) res;
			inspectorsDict.TryGetValue(id, out res);
			return res;
		}
	}
}
