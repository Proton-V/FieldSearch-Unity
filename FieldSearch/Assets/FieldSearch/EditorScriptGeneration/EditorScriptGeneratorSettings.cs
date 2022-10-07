using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.EditorScriptGeneration.Templates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration
{
    [CreateAssetMenu(fileName = "EditorScriptGenerator Settings", menuName = "ScriptableObjects/FieldSearch/EditorScriptGenerator/Settings")]
    public class EditorScriptGeneratorSettings : BaseCodeGeneratorSettings<BaseEditorScriptTemplate>
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
    }
}
