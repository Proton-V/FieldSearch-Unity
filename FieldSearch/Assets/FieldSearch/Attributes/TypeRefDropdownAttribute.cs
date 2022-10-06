﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FieldSearch.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TypeRefDropdownAttribute : PropertyAttribute
    {
        public Type BaseType { get; private set; }
        public string[] InheritedTypeNameArray { get; private set; }
        public string[] ShortInheritedTypeNameArray { get; private set; }

        public TypeRefDropdownAttribute(Type baseType)
        {
            BaseType = baseType;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = GetInheritedTypes(BaseType, assemblies);
            InheritedTypeNameArray = types.Select(x => x.AssemblyQualifiedName).ToArray();
            ShortInheritedTypeNameArray = types.Select(x => x.Name).ToArray();
        }

        public static List<Type> GetInheritedTypes(Type baseType, params Assembly[] assemblies)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in
                assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.IsSubclassOf(baseType)
                && x.IsClass
                && !x.IsAbstract))
            {
                types.Add(type);
            }
            types.Sort();

            return types;
        }
    }
}