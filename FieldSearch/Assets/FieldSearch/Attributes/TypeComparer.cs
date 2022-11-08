using System;
using System.Collections.Generic;

namespace FieldSearch.Attributes
{
    /// <summary>
    /// Type comparer.
    /// Used in <see cref="TypeRefDropdownAttribute"/>
    /// </summary>
    public class TypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
