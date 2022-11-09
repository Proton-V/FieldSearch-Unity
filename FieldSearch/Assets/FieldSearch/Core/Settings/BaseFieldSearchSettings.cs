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
    /// <summary>
    /// Base settings singleton class for <see cref="FieldSearch"/>
    /// </summary>
    public abstract class BaseFieldSearchSettings : ScriptableObject
    {
        /// <summary>
        /// Relative path to custom global gitignore file
        /// </summary>
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
        [Tooltip("Apply SearchableEditor to all MonoBehaviour (who don't use custom inspector)")]
        protected bool applyToAll = true;

        [SerializeField, TypeRefDropdown(typeof(BaseSearchLayerInspector))]
        protected string searchLayerTypeName;

        [Header("Cache settings")]
        [SerializeField]
        [Tooltip("Save cache to disk to use previous cache between sessions")]
        protected bool saveToDisk = true;

        [SerializeField]
        [Range(100, 10000)]
        [Tooltip("Memory limit in MB - memory and disk cache limit")]
        protected int memoryLimitInMb = 100;

        /// <summary>
        /// Apply SearchableEditor to all MonoBehaviour flag
        /// </summary>
        public bool ApplyToAll => applyToAll;

        /// <summary>
        /// Custom SearchableLayerInspector type
        /// </summary>
        public Type SearchLayerInspectorType => Type.GetType(searchLayerTypeName);

        /// <summary>
        /// Save cache to disk flag
        /// </summary>
        public bool SaveToDisk => saveToDisk;

        /// <summary>
        /// Disk/Memory cache limit in MB
        /// </summary>
        public int MemoryLimitInMb => memoryLimitInMb;

        private void OnEnable()
        {
            if (GetInstance() != null)
            {
                TryUpdateInstance();
            }
        }

        /// <summary>
        /// Delete previous instance when creating a new one
        /// </summary>
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

        /// <summary>
        /// Start hidden cmd process
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cmdArgs"></param>
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
