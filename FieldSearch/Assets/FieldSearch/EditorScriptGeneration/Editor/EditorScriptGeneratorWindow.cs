using CodeGeneration;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration.Editor
{
    public class EditorScriptGeneratorWindow : EditorWindow
    {
        EditorScriptGenerator editorScriptGenerator;

        public static void Init(EditorScriptGeneratorSettings editorScriptGeneratorSettings)
        {
            EditorScriptGeneratorWindow window = (EditorScriptGeneratorWindow)
                GetWindow(typeof(EditorScriptGeneratorWindow));
            window.editorScriptGenerator = editorScriptGeneratorSettings.GeneratorInstance;
            window.Show();
        }

        void OnGUI()
        {
            if (GUILayout.Button("Try create new Editors for all classes"))
            {
                var inputClasses = CodeGenerationUtils.GetAllInheritedTypes(typeof(UnityEditor.Editor),
                    ValidateNamespaceFunc: (name) =>
                    {
                        if(name == null)
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

                editorScriptGenerator.CreateScripts(inputTypes: inputClasses);

                Close();
            }
        }
    }
}
