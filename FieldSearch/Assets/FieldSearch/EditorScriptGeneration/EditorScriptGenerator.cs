using CodeGeneration;
using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.EditorScriptGeneration.Templates;
using System;
using UnityEditor;

namespace FieldSearch.EditorScriptGeneration
{
    public class EditorScriptGenerator : BaseCodeGenerator<BaseEditorScriptTemplate>
    {
        public override void CreateScripts(BaseEditorScriptTemplate scriptTemplate, params Type[] inputTypes)
        {
            foreach (var editorType in inputTypes)
            {
                if (editorType != typeof(Editor))
                {
                    continue;
                }

                var script = scriptTemplate.CreateScript(editorType);
            }
        }
    }
}
