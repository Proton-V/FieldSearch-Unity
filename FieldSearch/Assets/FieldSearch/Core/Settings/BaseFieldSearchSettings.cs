using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System;
using FieldSearch.Core.Inspectors.Base;
using FieldSearch.Attributes;
using Debug = UnityEngine.Debug;

namespace FieldSearch.Settings.Base
{
    public abstract class BaseFieldSearchSettings : ScriptableObject
    {
        protected const string GlobalGitignorePath = "FieldSearch/gitignore.global";

        public static BaseFieldSearchSettings Instance
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
        protected static BaseFieldSearchSettings _instance;

        [Header("Inspector settings")]
        [SerializeField]
        protected bool applyToAll = true;

        [SerializeField, TypeRefDropdown(typeof(BaseSearchLayerInspector))]
        protected string searchLayerTypeName;

        [Header("Cache settings")]
        [SerializeField]
        protected bool saveToDisk = true;

        [SerializeField]
        [Range(100, 10000)]
        protected int memoryLimitInMb = 100;

        public bool ApplyToAll => applyToAll;

        public Type SearchLayerInspectorType => Type.GetType(searchLayerTypeName);

        public bool SaveToDisk => saveToDisk;

        public int MemoryLimitInMb => memoryLimitInMb;

        private void OnEnable()
        {
            if (GetInstance() != null)
            {
                TryUpdateInstance();
            }
        }

        protected virtual void TryUpdateInstance()
        {
            if (Instance != this)
            {
                var path = AssetDatabase.GetAssetPath(Instance);

                Debug.LogWarning($"Delete previous setting \n(name: {Instance.name},path: {path})" +
                    $" && set new instance ({this.name}) to {typeof(BaseFieldSearchSettings)}");

                AssetDatabase.DeleteAsset(path);
                Instance = this;
            }
        }

        protected static BaseFieldSearchSettings GetInstance()
        {
            var guid = AssetDatabase.FindAssets($"t:{nameof(BaseFieldSearchSettings)}")
                .FirstOrDefault();
            var path = AssetDatabase.GUIDToAssetPath(guid);

            return AssetDatabase.LoadAssetAtPath<BaseFieldSearchSettings>(path);
        }

        protected static string GetDirectoryName()
        {
            var path = Application.dataPath;

            // Debug line for repo dev
            //path = Path.GetDirectoryName(path);
            return Path.GetDirectoryName(path);
        }

        protected static void StartCmdProcess(string path, string cmdArgs)
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
