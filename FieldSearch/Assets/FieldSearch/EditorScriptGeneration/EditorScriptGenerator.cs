﻿using CodeGeneration;
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

        public override void CreateScripts(BaseEditorScriptTemplate scriptTemplate = null, params Type[] inputTypes)
        {
            if (scriptTemplate == null)
            {
                scriptTemplate = _settings.DefaultScriptTemplate;
            }

            foreach (var editorType in inputTypes)
            {
                if (!editorType.IsSubclassOf(typeof(UnityEditor.Editor)))
                {
                    continue;
                }

                var script = scriptTemplate.CreateScript(editorType);

                CodeGenerationUtils.SaveToFile(_settings.DefaultFileFolder, script);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ForceReserializeAssets(
                assetPaths: new[] { _settings.DefaultFileFolder },
                options: ForceReserializeAssetsOptions.ReserializeAssets);
        }
    }
}