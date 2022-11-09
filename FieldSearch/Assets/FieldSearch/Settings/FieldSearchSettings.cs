using FieldSearch.Core.Inspectors;
using FieldSearch.EditorScriptGeneration;
using FieldSearch.Settings.Base;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Settings
{
    /// <summary>
    /// FieldSearchSettings implementation
    /// based on <see cref="BaseFieldSearchSettings"/>
    /// </summary>
    [CreateAssetMenu(fileName = "FieldSearch Settings", menuName = "ScriptableObjects/FieldSearch/Settings", order = -1)]
    public class FieldSearchSettings : BaseFieldSearchSettings
    {
        /// <summary>
        /// EditorScriptGenerator settings ref
        /// </summary>
        public EditorScriptGeneratorSettings EditorScriptGeneratorSettings =>
            _editorScriptGeneratorSettings;

        [Header("EditorScriptGenerator Settings")]
        [SerializeField]
        private EditorScriptGeneratorSettings _editorScriptGeneratorSettings;

        /// <summary>
        /// Show settings object,
        /// method for editor menu
        /// </summary>
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

        /// <summary>
        /// Create <see cref="FieldSearchSettings"/> with default fields
        /// </summary>
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

        /// <summary>
        /// Add custom global gitignore to project,
        /// method for editor menu
        /// </summary>
        [MenuItem("Field Search/Add package folders to .gitignore (global)")]
        public static void AddToGlobalGitignore()
        {
            var gitignorePath = Path.Combine(Application.dataPath, GlobalGitignorePath);
            var str = $"git config --global core.excludesfile {gitignorePath}";
            StartCmdProcess(GetDirectoryName(), str);
        }

        /// <summary>
        /// Remove custom global gitignore from project,
        /// method for editor menu
        /// </summary>
        [MenuItem("Field Search/Remove package folders from .gitignore (global)")]
        public static void RemoveFromGlobalGitignore()
        {
            var str = $"git config --global --unset core.excludesfile";
            StartCmdProcess(GetDirectoryName(), str);
        }
    }
}
