using FieldSearch.Core.Inspectors;
using FieldSearch.EditorScriptGeneration;
using FieldSearch.Settings.Base;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Settings
{
    [CreateAssetMenu(fileName = "FieldSearch Settings", menuName = "ScriptableObjects/FieldSearch/Settings", order = -1)]
    public class FieldSearchSettings : BaseFieldSearchSettings
    {
        public EditorScriptGeneratorSettings EditorScriptGeneratorSettings =>
            _editorScriptGeneratorSettings;

        [Header("EditorScriptGenerator Settings")]
        [SerializeField]
        private EditorScriptGeneratorSettings _editorScriptGeneratorSettings;

        [MenuItem("Field Search/ShowSettings")]
        public static void ShowSettings()
        {
            Selection.activeObject = Instance;
            EditorGUIUtility.PingObject(Instance);
        }

        [MenuItem("Field Search/ShowSettings", true)]
        static bool ValidateShowSettings()
        {
            return Instance != null;
        }

        [MenuItem("Field Search/Add default settings (override if exists)")]
        public static void CreateSettingsObject()
        {
            var settings = CreateInstance<FieldSearchSettings>();

            var directoryPath = "Assets/FieldSearchConfigs";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            settings.searchLayerTypeName = typeof(DefaultSearchLayerInspector).AssemblyQualifiedName;
            settings._editorScriptGeneratorSettings = EditorScriptGeneratorSettings.CreateSettingsObject();

            string path = $"{directoryPath}/FieldSearch Settings.asset";
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();

            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
        }

        [MenuItem("Field Search/Add default settings (override if exists)", true)]
        static bool ValidateCreateSettingsObject()
        {
            return Instance == null;
        }

        [MenuItem("Field Search/Add package folders to .gitignore (global)")]
        public static void AddToGlobalGitignore()
        {
            var gitignorePath = Path.Combine(Application.dataPath, GlobalGitignorePath);
            var str = $"git config --global core.excludesfile {gitignorePath}";
            StartCmdProcess(GetDirectoryName(), str);
        }

        [MenuItem("Field Search/Remove package folders from .gitignore (global)")]
        public static void RemoveFromGlobalGitignore()
        {
            var str = $"git config --global --unset core.excludesfile";
            StartCmdProcess(GetDirectoryName(), str);
        }
    }
}
