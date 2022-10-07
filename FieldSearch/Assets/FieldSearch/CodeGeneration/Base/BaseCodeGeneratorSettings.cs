using System;
using System.IO;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseCodeGeneratorSettings<T> : ScriptableObject where T : BaseScriptTemplate
    {
        public T DefaultScriptTemplate => _defaultScriptTemplate;

        public string DefaultFileFolder =>
            Path.Combine(Environment.CurrentDirectory, _defaultFileFolder);

        protected abstract string CodeGeneratorTypeName { get; }

        protected Type CodeGeneratorType => Type.GetType(CodeGeneratorTypeName);

        [Tooltip("Default relative folder path")]
        [SerializeField]
        protected string _defaultFileFolder;
        [SerializeField]
        protected T _defaultScriptTemplate;

        public string FullFileFolder(string relativePath) =>
            Path.Combine(Environment.CurrentDirectory, relativePath);

        public BaseCodeGenerator<T> CreateGeneratorInstance()
        {
            return (BaseCodeGenerator<T>)Activator
                .CreateInstance(CodeGeneratorType, args: this);
        }
    }
}
