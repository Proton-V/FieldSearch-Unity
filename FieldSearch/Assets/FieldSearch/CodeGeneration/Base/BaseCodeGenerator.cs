using System;
using UnityEngine;

namespace CodeGeneration.Base
{
    /// <summary>
    /// Base class for custom CodeGenerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCodeGenerator<T> where T : BaseScriptTemplate
    {
        protected BaseCodeGenerator(BaseCodeGeneratorSettings<T> settings)
        {
            _settings = settings;
        }

        [SerializeField]
        protected BaseCodeGeneratorSettings<T> _settings;

        /// <summary>
        /// Create scripts using <paramref name="scriptTemplate"/>
        /// </summary>
        /// <param name="scriptTemplate"></param>
        /// <param name="refresh"></param>
        /// <param name="inputTypes"></param>
        public abstract void CreateScripts(T scriptTemplate, bool refresh = true, params Type[] inputTypes);
    }
}
