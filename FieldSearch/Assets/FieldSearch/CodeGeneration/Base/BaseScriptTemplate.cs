using CodeGeneration.Data;
using System;
using UnityEngine;

namespace CodeGeneration.Base
{
    /// <summary>
    /// Base script template object
    /// </summary>
    public abstract class BaseScriptTemplate : ScriptableObject
    {
        /// <summary>
        /// File name format string
        /// </summary>
        [SerializeField]
        protected string _scriptNameFormatString;

        /// <summary>
        /// Script format string
        /// </summary>
        [TextArea(10, 70)]
        [SerializeField]
        protected string _scriptFormatString;

        /// <summary>
        /// Create script with args using <see cref="_scriptFormatString"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract GeneratedScript CreateScript(Type type, params object[] args);
    }
}
