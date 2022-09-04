using FieldSearch.Core.Inspectors;
using FieldSearch.Core.Inspectors.Base;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Settings
{
    [CreateAssetMenu(fileName = "FieldSearch Settings", menuName = "ScriptableObjects/FieldSearch/Settings")]
    public class FieldSearchSettings : ScriptableObject
    {
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
        private bool applyToAll;

        [SerializeField]
        private DefaultSearchableEditorConfigObject searchableEditor;

        [Header("Cache settings")]
        [SerializeField]
        private bool saveToDisk;

        [SerializeField]
        [Range(100, 10000)]
        private int memoryLimitInMb = 100;

        public bool ApplyToAll => applyToAll;

        public BaseSearchableEditorConfigObject SearchableEditor => searchableEditor;

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

                Debug.LogError($"Delete previous setting (name: {Instance.name},path: {path})" +
                    $"\n&& set new instance ({this.name}) to {typeof(FieldSearchSettings)}");

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
    }
}
