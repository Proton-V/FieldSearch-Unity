using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseCodeGenerator<T> where T : BaseScriptTemplate
    {
        [SerializeField]
        private BaseCodeGeneratorSettings _settings;

        public abstract void CreateScripts(T scriptTemplate, params Type[] inputTypes);
    }

}
