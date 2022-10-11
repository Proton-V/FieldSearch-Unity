using CodeGeneration;
using CodeGeneration.Data;
using FieldSearch.Core.GlobalEditor;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration.Templates
{
    [CreateAssetMenu(fileName = DEFAULT_OBJECT_NAME, 
        menuName = "ScriptableObjects/FieldSearch/EditorScriptGenerator/Templates/DefaultEditorScriptTemplate")]
    public partial class DefaultEditorScriptTemplate : BaseEditorScriptTemplate
    {
        public override GeneratedScript CreateScript(Type type, params object[] args)
        {
            try
            {
                var attribute = CodeGenerationUtils.GetFirstClassAttribute<CustomEditor>(type);
                if (attribute == null)
                {
                    return null;
                }
                var targetTypeField = CodeGenerationUtils.GetFirstAttributeFieldByType<Type>(attribute);
                var editorTargetType = targetTypeField.GetValue(attribute) as Type;

                var editorForChildClassesField = CodeGenerationUtils.GetAttributeFieldByName(attribute, "m_EditorForChildClasses");
                var isEditorForChildClasses = (bool)editorForChildClassesField.GetValue(attribute);
                var isEditorForChildClassesLine = isEditorForChildClasses ? ", true" : "";

                var customEditorAttributeLine = $"typeof({editorTargetType.Name}){isEditorForChildClassesLine}";

                if (editorTargetType != null)
                {
                    var newEditor = DefaultBaseEditorType;

                    var newEditorTypeNamespaceLine = string.IsNullOrEmpty(newEditor.Namespace) ?
                        string.Empty : $"using {newEditor.Namespace};";
                    var targetTypeNamespaceLine = string.IsNullOrEmpty(editorTargetType.Namespace) ?
                        string.Empty : $"using {editorTargetType.Namespace};";
                    var editorTypeNamespaceLine = string.IsNullOrEmpty(type.Namespace) ?
                        string.Empty : $"using {type.Namespace};";

                    var newFileName = string.Format(_scriptNameFormatString, type.Name); ;
                    var newScriptName = Path.GetFileNameWithoutExtension(newFileName);

                    object[] scriptArgs = new[]
                    {
                        newEditorTypeNamespaceLine,
                        targetTypeNamespaceLine,
                        editorTypeNamespaceLine,
                        customEditorAttributeLine,
                        newScriptName,
                        newEditor.Name,
                        type.Name,
                    };

                    var scriptStr = string.Format(_scriptFormatString, scriptArgs);

                    return new GeneratedScript(newFileName, scriptStr);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return null;
        }

        public static DefaultEditorScriptTemplate CreateTemplateObject()
        {
            var template = CreateInstance<DefaultEditorScriptTemplate>();

            if (!Directory.Exists(DIRECTORY_PATH))
            {
                Directory.CreateDirectory(DIRECTORY_PATH);
            }

            template._scriptNameFormatString = DEFAULT_SCRIPT_NAME_FORMAT;
            template._scriptFormatString = DEFAULT_SCRIPT_FORMAT;
            template._defaultBaseEditorTypeName = typeof(DefaultFieldSearchGlobalEditor).AssemblyQualifiedName;

            string path = $"{DIRECTORY_PATH}/{DEFAULT_OBJECT_NAME}.asset";
            AssetDatabase.CreateAsset(template, path);
            AssetDatabase.SaveAssets();

            return template;
        }
    }
}
