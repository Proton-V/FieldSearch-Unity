using System;

namespace FieldSearch.Helpers.Data
{
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
