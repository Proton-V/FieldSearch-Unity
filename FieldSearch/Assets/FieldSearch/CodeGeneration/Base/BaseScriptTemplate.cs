using CodeGeneration.Data;
using System;
using UnityEngine;

namespace CodeGeneration.Base
{
    public abstract class BaseScriptTemplate : ScriptableObject
    {
        [SerializeField]
        protected string _scriptNameFormatString;

        [TextArea(10, 70)]
        [SerializeField]
        protected string _scriptFormatString;

        public abstract GeneratedScript CreateScript(Type type, params object[] args);
    }
}
