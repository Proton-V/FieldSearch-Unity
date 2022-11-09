using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.EditorScriptGeneration.Templates;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration
{
    /// <summary>
    /// Settings singleton object for <see cref="EditorScriptGenerator"/>
    /// </summary>
    [CreateAssetMenu(fileName = DEFAULT_OBJECT_NAME, menuName = "ScriptableObjects/FieldSearch/EditorScriptGenerator/Settings")]
    public partial class EditorScriptGeneratorSettings : BaseCodeGeneratorSettings<BaseEditorScriptTemplate>
    {
        public EditorScriptGenerator GeneratorInstance
        {
            get
            {
                if(_generatorInstance == null)
                {
                    _generatorInstance = (EditorScriptGenerator)CreateGeneratorInstance();
                }
                return _generatorInstance;
            }
        }
        private EditorScriptGenerator _generatorInstance;

        /// <summary>
        /// CodeGenerator type name
        /// </summary>
        protected override string CodeGeneratorTypeName => _codeGeneratorTypeName;

        [SerializeField]
        [TypeRefDropdown(typeof(BaseCodeGenerator<BaseEditorScriptTemplate>))]
        protected string _codeGeneratorTypeName;

        /// <summary>
        /// Create <see cref="EditorScriptGeneratorSettings"/> with default fields
        /// </summary>
        /// <returns></returns>
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
