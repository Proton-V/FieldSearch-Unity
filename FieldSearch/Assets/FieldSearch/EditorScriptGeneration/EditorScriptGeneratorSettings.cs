using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.EditorScriptGeneration.Templates;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration
{
    [CreateAssetMenu(fileName = DEFAULT_OBJECT_NAME, menuName = "ScriptableObjects/FieldSearch/EditorScriptGenerator/Settings")]
    public partial class EditorScriptGeneratorSettings : BaseCodeGeneratorSettings<BaseEditorScriptTemplate>
    {
        public EditorScriptGenerator GeneratorInstance
        {
            get
            {
                if(generatorInstance == null)
                {
                    generatorInstance = (EditorScriptGenerator)CreateGeneratorInstance();
                }
                return generatorInstance;
            }
        }
        private EditorScriptGenerator generatorInstance;

        protected override string CodeGeneratorTypeName => _codeGeneratorTypeName;

        [SerializeField]
        [TypeRefDropdown(typeof(BaseCodeGenerator<BaseEditorScriptTemplate>))]
        protected string _codeGeneratorTypeName;

        public static EditorScriptGeneratorSettings CreateSettingsObject()
        {
            var settings = CreateInstance<EditorScriptGeneratorSettings>();

            if (!Directory.Exists(DIRECTORY_PATH))
            {
                Directory.CreateDirectory(DIRECTORY_PATH);
            }

            settings._defaultFileFolder = DEFAULT_GENERATED_FILE_FOLDER;
            settings._defaultScriptTemplate = DefaultEditorScriptTemplate.CreateTemplateObject();
            settings._codeGeneratorTypeName = typeof(EditorScriptGenerator).AssemblyQualifiedName;

            string path = $"{DIRECTORY_PATH}/{DEFAULT_OBJECT_NAME}.asset";
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();

            return settings;
        }
    }
}
