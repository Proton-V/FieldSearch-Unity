using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FieldSearch.Attributes
{
    /// <summary>
    /// Dropdown list attribute for type, inherited from BaseType
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TypeRefDropdownAttribute : PropertyAttribute
    {
        public TypeRefDropdownAttribute(Type baseType, params string[] ignoredNamespaces)
        {
            BaseType = baseType;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = GetInheritedTypes(BaseType, assemblies)
                .Where(x => !x.IsAbstract
                && !ignoredNamespaces.Contains(x.Namespace));

            InheritedTypeNameArray = types.Select(x => x.AssemblyQualifiedName).ToArray();
            ShortInheritedTypeNameArray = types.Select(x => x.Name).ToArray();
        }

        /// <summary>
        /// Base type
        /// </summary>
        public Type BaseType { get; private set; }

        /// <summary>
        /// Array with Type.AssemblyQualifiedName
        /// </summary>
        public string[] InheritedTypeNameArray { get; private set; }

        /// <summary>
        /// Array with Type.Name
        /// </summary>
        public string[] ShortInheritedTypeNameArray { get; private set; }

        /// <summary>
        /// Get all Inherited from <paramref name="baseType"/> Types
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static List<Type> GetInheritedTypes(Type baseType, params Assembly[] assemblies)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in
                assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.IsSubclassOf(baseType)
                && x.IsClass))
            {
                types.Add(type);
            }
            types.Sort(new TypeComparer());

            return types;
        }
    }
}
