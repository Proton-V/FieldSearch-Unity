using CodeGeneration.Base;
using FieldSearch.EditorScriptGeneration.Templates;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration
{
    [CreateAssetMenu(fileName = "EditorScriptGenerator Settings", menuName = "ScriptableObjects/FieldSearch/EditorScriptGenerator/Settings")]
    public class EditorScriptGeneratorSettings : BaseCodeGeneratorSettings<BaseEditorScriptTemplate>
    {

    }
}
