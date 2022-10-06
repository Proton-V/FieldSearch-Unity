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
