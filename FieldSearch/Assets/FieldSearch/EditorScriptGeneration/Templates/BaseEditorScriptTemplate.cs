using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.Core.GlobalEditor;
using System;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration.Templates
{
    /// <summary>
    /// Base editor script template object.
    /// Base editor implementation <see cref="BaseScriptTemplate"/>
    /// </summary>
    public abstract class BaseEditorScriptTemplate : BaseScriptTemplate
    {
        protected Type DefaultBaseEditorType => Type.GetType(_defaultBaseEditorTypeName);

        [SerializeField]
        [TypeRefDropdown(typeof(BaseFieldSearchGlobalEditor),
            ignoredNamespaces: new string[] { null })] // null to ignore generated classes w/o namespace
        protected string _defaultBaseEditorTypeName;
    }
}
