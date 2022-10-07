using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.EditorScriptGeneration.Templates;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration
{
    [CreateAssetMenu(fileName = "EditorScriptGenerator Settings", menuName = "ScriptableObjects/FieldSearch/EditorScriptGenerator/Settings")]
    public class EditorScriptGeneratorSettings : BaseCodeGeneratorSettings<BaseEditorScriptTemplate>
    {
        protected override string CodeGeneratorTypeName => _codeGeneratorTypeName;

        [SerializeField]
        [TypeRefDropdown(typeof(BaseCodeGenerator<BaseEditorScriptTemplate>))]
        protected string _codeGeneratorTypeName;
    }
}
