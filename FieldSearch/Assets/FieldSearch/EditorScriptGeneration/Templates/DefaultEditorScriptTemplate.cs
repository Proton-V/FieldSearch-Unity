﻿using CodeGeneration;
using CodeGeneration.Data;
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
                var field = CodeGenerationUtils.GetFirstAttributeFieldByType<Type>(attribute);
                var editorTargetType = field.GetValue(attribute) as Type;

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
                        editorTargetType.Name,
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

            string path = $"{DIRECTORY_PATH}/{DEFAULT_OBJECT_NAME}.asset";
            AssetDatabase.CreateAsset(template, path);
            AssetDatabase.SaveAssets();

            return template;
        }
    }
}
