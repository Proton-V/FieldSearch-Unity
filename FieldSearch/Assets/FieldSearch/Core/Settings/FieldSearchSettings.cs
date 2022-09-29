using FieldSearch.Core.Inspectors;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;

namespace FieldSearch.Settings
{
    [CreateAssetMenu(fileName = "FieldSearch Settings", menuName = "ScriptableObjects/FieldSearch/Settings")]
    public class FieldSearchSettings : ScriptableObject
    {
        private const string GlobalGitignorePath = "FieldSearch/gitignore.global";

        public static FieldSearchSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetInstance();
                }
                return _instance;
            }
            set => _instance = value;
        }
        private static FieldSearchSettings _instance;

        [Header("Inspector settings")]
        [SerializeField]
        private bool applyToAll = true;

        [SerializeField]
        private string searchEditorTypeQualifiedName;

        [Header("Cache settings")]
        [SerializeField]
        private bool saveToDisk = true;

        [SerializeField]
        [Range(100, 10000)]
        private int memoryLimitInMb = 100;

        public bool ApplyToAll => applyToAll;

        public Type SearchEditorType => Type.GetType(searchEditorTypeQualifiedName);

        public bool SaveToDisk => saveToDisk;

        public int MemoryLimitInMb => memoryLimitInMb;

        private void OnEnable()
        {
            if (GetInstance() != null)
            {
                TryUpdateInstance();
            }
        }

        private void TryUpdateInstance()
        {
            if (Instance != this)
            {
                var path = AssetDatabase.GetAssetPath(Instance);

                Debug.LogWarning($"Delete previous setting \n(name: {Instance.name},path: {path})" +
                    $" && set new instance ({this.name}) to {typeof(FieldSearchSettings)}");

                AssetDatabase.DeleteAsset(path);
                Instance = this;
            }
        }

        private static FieldSearchSettings GetInstance()
        {
            var guid = AssetDatabase.FindAssets($"t:{nameof(FieldSearchSettings)}")
                .FirstOrDefault();
            var path = AssetDatabase.GUIDToAssetPath(guid);

            return AssetDatabase.LoadAssetAtPath<FieldSearchSettings>(path);
        }

        [MenuItem("Field Search/ShowSettings")]
        public static void ShowSettings()
        {
            var settings = CreateInstance<FieldSearchSettings>();

            var directoryPath = "Assets/FieldSearchConfigs";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            settings.searchEditorTypeQualifiedName = typeof(DefaultSearchLayerInspector).AssemblyQualifiedName;

            string path = $"{directoryPath}/FieldSearch Settings.asset";
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();

            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
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
            
            settings.searchEditorTypeQualifiedName = typeof(DefaultSearchLayerInspector).AssemblyQualifiedName;

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
            var str = $"git config core.excludesfile {gitignorePath}";
            StartCmdProcess(GetDirectoryName(), str);
        }

        [MenuItem("Field Search/Remove package folders from .gitignore (global)")]
        public static void RemoveFromGlobalGitignore()
        {
            var str = $"git config --unset core.excludesfile";
            StartCmdProcess(GetDirectoryName(), str);
        }

        private static string GetDirectoryName()
        {
            var path = Application.dataPath;

            // Debug line for repo dev
            //path = Path.GetDirectoryName(path);
            return Path.GetDirectoryName(path);
        }

        private static void StartCmdProcess(string path, string cmdArgs)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = path;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C {cmdArgs}";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
