namespace FieldSearch.EditorScriptGeneration.Templates
{
    public partial class DefaultEditorScriptTemplate
    {
        const string DIRECTORY_PATH = "Assets/FieldSearchConfigs/EditorScriptGenerator";
        const string DEFAULT_OBJECT_NAME = "DefaultEditorScriptTemplate";

        public const string DEFAULT_SCRIPT_NAME_FORMAT = "{0}_Generated.cs";
        public const string DEFAULT_SCRIPT_FORMAT =
@"using UnityEditor;
{0}
{1}
{2}
[CustomEditor(typeof({3}))]
public class {4} : {5}<{6}>
{{

}}
";
    }
}
