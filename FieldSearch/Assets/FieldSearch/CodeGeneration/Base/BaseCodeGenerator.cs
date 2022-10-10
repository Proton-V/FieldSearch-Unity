using System;
using System.IO;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseCodeGenerator<T> where T : BaseScriptTemplate
    {
        protected BaseCodeGenerator(BaseCodeGeneratorSettings<T> settings)
        {
            _settings = settings;
        }

        [SerializeField]
        protected BaseCodeGeneratorSettings<T> _settings;

        public abstract void CreateScripts(T scriptTemplate, bool refresh = true, params Type[] inputTypes);
    }

}
