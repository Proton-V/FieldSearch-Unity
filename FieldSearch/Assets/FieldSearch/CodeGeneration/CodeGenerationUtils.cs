using CodeGeneration.Data;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CodeGeneration
{
    /// <summary>
    /// Utils for CodeGeneration
    /// </summary>
    public class CodeGenerationUtils
    {
        /// <summary>
        /// Get all <see cref="UnityEditor.Editor"/> types.
        /// Excluding default or sample types.
        /// </summary>
        /// <returns></returns>
        public static Type[] GetAllAvailableEditorTypes()
        {
            var types = GetAllInheritedTypes(typeof(UnityEditor.Editor),
                ValidateNamespaceFunc: (name) =>
                {
                    if (name == null)
                    {
                        return false;
                    }

                    // Dev debug lines
                    if (new DirectoryInfo(Environment.CurrentDirectory).Name == nameof(FieldSearch)
                        && name.StartsWith("FieldSearch.Samples"))
                    {
                        return true;
                    }

                    // Default exclude namespaces
                    if (name.Contains(nameof(UnityEditor))
                    || name.Contains(nameof(UnityEngine))
                    || name.Contains(nameof(FieldSearch))
                    || name.Contains("TMP"))
                    {
                        return false;
                    }

                    return true;
                });
            return types;
        }

        /// <summary>
        /// Get all inherited types for <paramref name="baseType"/>
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="assemblies"></param>
        /// <param name="ValidateNamespaceFunc"></param>
        /// <returns></returns>
        public static Type[] GetAllInheritedTypes(Type baseType, Assembly[] assemblies = default, 
            Func<string, bool> ValidateNamespaceFunc = default)
        {
            if(assemblies == default)
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            if(ValidateNamespaceFunc == default)
            {
                ValidateNamespaceFunc = (name) => true;
            }

            var types = 
                assemblies
                .SelectMany(x => x
                    .GetTypes()
                    .Where(type =>
                        type.IsPublic
                        && type.IsClass
                        && !type.IsAbstract
                        && type.IsSubclassOf(baseType)
                        && ValidateNamespaceFunc(type.Namespace)));
            return types.ToArray();
        }

        /// <summary>
        /// Get first FieldInfo for <paramref name="attribute"/> by <paramref name="fieldName"/>
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static FieldInfo GetAttributeFieldByName(Attribute attribute, string fieldName)
        {
            var field = attribute.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.FirstOrDefault(x => x.Name == fieldName);
            return field;
        }

        /// <summary>
        /// Get first FieldInfo for <paramref name="attribute"/> by type of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static FieldInfo GetFirstAttributeFieldByType<T>(Attribute attribute) where T : Type
        {
            var field = attribute.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.FirstOrDefault(x => x.GetValue(attribute) is T);
            return field;
        }

        /// <summary>
        /// Get first class <typeparamref name="T"/> attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T GetFirstClassAttribute<T>(Type type) where T : Attribute
        {
            var attribute = type.GetCustomAttributes(typeof(T), true)
            ?.FirstOrDefault() as T;
            return attribute;
        }

        /// <summary>
        /// Save <paramref name="generatedScript"/> to folder with <paramref name="folderPath"/>
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="generatedScript"></param>
        /// <returns></returns>
        public static bool SaveToFile(string folderPath, GeneratedScript generatedScript)
        {
            try
            {
                if(generatedScript?.fileName == null)
                {
                    return false;
                }

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var path = Path.Combine(folderPath, generatedScript.fileName);
                File.WriteAllText(path, generatedScript.scriptStr);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }
}
