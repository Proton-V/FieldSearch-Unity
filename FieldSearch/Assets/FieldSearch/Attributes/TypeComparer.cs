using System;
using System.Collections.Generic;

namespace FieldSearch.Attributes
{
    public class TypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
