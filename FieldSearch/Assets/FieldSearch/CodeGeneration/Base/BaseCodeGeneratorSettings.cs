using System;
using System.IO;
using UnityEngine;

namespace CodeGeneration.Base
{
    /// <summary>
    /// Base settings object for <see cref="BaseCodeGenerator{T}"/>
    /// </summary>
    public abstract class BaseCodeGeneratorSettings<T> : ScriptableObject where T : BaseScriptTemplate
    {
        /// <summary>
        /// Default script template, used in <see cref="BaseCodeGenerator{T}"/>
        /// </summary>
        public T DefaultScriptTemplate => _defaultScriptTemplate;

        /// <summary>
        /// Folder for generated files
        /// </summary>
        public string DefaultFileFolder =>
            Path.Combine(Environment.CurrentDirectory, _defaultFileFolder);

        /// <summary>
        /// CodeGenerator type name
        /// </summary>
        protected abstract string CodeGeneratorTypeName { get; }

        /// <summary>
        /// Get CodeGeneratorType using <see cref="CodeGeneratorTypeName"/>
        /// </summary>
        protected Type CodeGeneratorType => Type.GetType(CodeGeneratorTypeName);

        [Tooltip("Default relative folder path")]
        [SerializeField]
        protected string _defaultFileFolder;
        [SerializeField]
        protected T _defaultScriptTemplate;

        /// <summary>
        /// Full folder path with <paramref name="relativePath"/>
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public string FullFileFolder(string relativePath) =>
            Path.Combine(Environment.CurrentDirectory, relativePath);

        /// <summary>
        /// Create new instance <see cref="BaseCodeGenerator{T}"/>
        /// using <see cref="CodeGeneratorType"/>
        /// </summary>
        /// <returns></returns>
        public BaseCodeGenerator<T> CreateGeneratorInstance()
        {
            return (BaseCodeGenerator<T>)Activator
                .CreateInstance(CodeGeneratorType, args: this);
        }
    }
}
