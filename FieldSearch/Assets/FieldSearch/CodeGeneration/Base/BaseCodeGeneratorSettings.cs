using CodeGeneration.Data;
using System;
using System.IO;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseCodeGeneratorSettings : ScriptableObject
    {
        public string DefaultFileFolder =>
            Path.Combine(Environment.CurrentDirectory, _defaultFileFolder);

        [SerializeField]
        protected string _defaultFileFolder;
        [SerializeField]
        protected BaseScriptTemplate _defaultScriptTemplate;

        public string FullFileFolder(string relativePath) =>
            Path.Combine(Environment.CurrentDirectory, relativePath);
    }
}
