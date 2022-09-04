using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldSearch.Helpers.Cache.Data
{
    [Serializable]
    public struct SearchCacheJson
    {
        public SearchCacheJson(Dictionary<int, SearchCacheObj> dict)
        {
            objects = dict.Select(x => x.Value).ToList();
        }

        public List<SearchCacheObj> objects;

        public Dictionary<int, SearchCacheObj> ToDictionary()
        {
            return objects.ToDictionary(x => x.id);
        }
    }
}