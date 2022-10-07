using System;
using System.IO;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseCodeGeneratorSettings<T> : ScriptableObject where T : BaseScriptTemplate
    {
        protected abstract string CodeGeneratorTypeName { get; }

        protected Type CodeGeneratorType => Type.GetType(CodeGeneratorTypeName);
        public string DefaultFileFolder =>
            Path.Combine(Environment.CurrentDirectory, _defaultFileFolder);

        [Tooltip("Default relative folder path")]
        [SerializeField]
        protected string _defaultFileFolder;
        [SerializeField]
        protected T _defaultScriptTemplate;

        public string FullFileFolder(string relativePath) =>
            Path.Combine(Environment.CurrentDirectory, relativePath);

        public BaseCodeGenerator<T> CreateNewGeneratorInstance()
        {
            return (BaseCodeGenerator<T>)Activator
                .CreateInstance(CodeGeneratorType, args: this);
        }
    }
}
