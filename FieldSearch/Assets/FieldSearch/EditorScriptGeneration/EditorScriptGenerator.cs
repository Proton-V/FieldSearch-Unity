using CodeGeneration;
using CodeGeneration.Base;
using FieldSearch.EditorScriptGeneration.Templates;
using System;
using UnityEditor;

namespace FieldSearch.EditorScriptGeneration
{
    public class EditorScriptGenerator : BaseCodeGenerator<BaseEditorScriptTemplate>
    {
        public EditorScriptGenerator(BaseCodeGeneratorSettings<BaseEditorScriptTemplate> settings)
            : base(settings) { }

        public override void CreateScripts(BaseEditorScriptTemplate scriptTemplate, params Type[] inputTypes)
        {
            foreach (var editorType in inputTypes)
            {
                if (editorType != typeof(Editor))
                {
                    continue;
                }

                var script = scriptTemplate.CreateScript(editorType);

                CodeGenerationUtils.SaveToFile(_settings.DefaultFileFolder, script);
            }
        }
    }
}
