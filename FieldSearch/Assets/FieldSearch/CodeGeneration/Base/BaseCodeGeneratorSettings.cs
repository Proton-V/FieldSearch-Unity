using CodeGeneration.Data;
using System;
using System.IO;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseCodeGeneratorSettings<T> : ScriptableObject where T : BaseScriptTemplate
    {
        public string DefaultFileFolder =>
            Path.Combine(Environment.CurrentDirectory, _defaultFileFolder);

        [Tooltip("Default relative folder path")]
        [SerializeField]
        protected string _defaultFileFolder;
        [SerializeField]
        protected T _defaultScriptTemplate;

        public string FullFileFolder(string relativePath) =>
            Path.Combine(Environment.CurrentDirectory, relativePath);
    }
}
