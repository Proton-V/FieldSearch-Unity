using FieldSearch.Helpers.Cache.Data;
using FieldSearch.Settings;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Helpers.Cache
{
	[InitializeOnLoad]
	public class SearchInspectorCache
	{
		static SearchInspectorCache()
        {
			ReadCacheFromDisk();
            AssemblyReloadEvents.beforeAssemblyReload += AssemblyReloadEvents_beforeAssemblyReload;
            EditorApplication.quitting += EditorApplication_quitting;
		}

        private static void EditorApplication_quitting()
        {
			EditorApplication.quitting -= EditorApplication_quitting;
			SaveCacheToDisk();
		}

		private static void AssemblyReloadEvents_beforeAssemblyReload()
        {
			AssemblyReloadEvents.beforeAssemblyReload -= AssemblyReloadEvents_beforeAssemblyReload;
			SaveCacheToDisk();
		}

		public const string FILE_NAME = "SearchInspectorCache.txt";

		public static string FilePath => $"{Application.temporaryCachePath}/{FILE_NAME}";

		public static float GetCurrentSize() => inspectorsDict.Sum(x => Marshal.SizeOf(x.Value));

		protected static Dictionary<int, SearchCacheObj> inspectorsDict
			= new Dictionary<int, SearchCacheObj>();

		protected static FieldSearchSettings Settings => FieldSearchSettings.Instance;

		public static void AddValue(SearchCacheObj val)
		{
            if (!HasFreeMemorySlots())
            {
				RemoveFirstValue();
			}

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

		private static void RemoveFirstValue()
        {
			inspectorsDict.Remove(inspectorsDict.Keys.First());
        }

		private static bool HasFreeMemorySlots()
        {
			if(Settings == null)
            {
				return true;
            }

			var currentSizeInMb = ConvertByteToMb(GetCurrentSize());
			return currentSizeInMb < Settings.MemoryLimitInMb;
		}

		private static double ConvertByteToMb(float byteCount)
		{
			return (byteCount / (double)Mathf.Pow(1024, 2));
		}

		private static void SaveCacheToDisk()
        {
			using (StreamWriter sw = File.CreateText(FilePath))
			{
				var obj = new SearchCacheJson(inspectorsDict);
				var json = JsonUtility.ToJson(obj);
				sw.Write(json);
			}
		}

		private static void ReadCacheFromDisk()
        {
            if (!File.Exists(FilePath))
            {
				return;
            }

			using (StreamReader sw = File.OpenText(FilePath))
			{
				var str = sw.ReadToEnd();
				if(string.IsNullOrEmpty(str)
					|| string.IsNullOrWhiteSpace(str))
                {
					return;
                }

				var obj = JsonUtility.FromJson<SearchCacheJson>(str);
				inspectorsDict = obj.ToDictionary();
			}
		}

		public static void ClearCache(bool fromDisk = true, bool fromMemory = true)
        {
            if (fromDisk)
            {
				using (StreamWriter sw = File.CreateText(FilePath))
				{
					var obj = new SearchCacheJson(inspectorsDict);
					var json = JsonUtility.ToJson(obj);
					sw.Write("");
				}
			}

            if (fromMemory)
            {
				inspectorsDict.Clear();
            }
		}
	}
}
