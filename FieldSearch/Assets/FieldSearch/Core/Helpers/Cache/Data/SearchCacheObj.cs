using System;

namespace FieldSearch.Helpers.Cache.Data
{
    /// <summary>
    /// Stored SearchCache struct
    /// </summary>
    [Serializable]
    public struct SearchCacheObj
    {
        public SearchCacheObj(int id, string searchText, int flags)
        {
            this.id = id;
            this.searchText = searchText;
            this.flags = flags;
        }

        public int id;
        public string searchText;
        public int flags;
    }
}
