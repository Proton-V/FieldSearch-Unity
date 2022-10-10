using CodeGeneration.Data;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CodeGeneration
{
    public class CodeGenerationUtils
    {
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

        public static FieldInfo GetFirstAttributeFieldByType<T>(Attribute attribute) where T : Type
        {
            var field = attribute.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.FirstOrDefault(x => x.GetValue(attribute) is T);
            return field;
        }

        public static T GetFirstClassAttribute<T>(Type type) where T : Attribute
        {
            var attribute = type.GetCustomAttributes(typeof(T), true)
            ?.FirstOrDefault() as T;
            return attribute;
        }

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
