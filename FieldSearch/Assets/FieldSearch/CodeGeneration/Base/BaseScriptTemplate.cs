using CodeGeneration.Data;
using System;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseScriptTemplate : ScriptableObject
    {
        [TextArea]
        [SerializeField]
        protected string _scriptFormatString;
        [SerializeField]
        protected string _scriptNameFormatString;

        public abstract GeneratedScript CreateScript(Type type, params object[] args);
    }
}
