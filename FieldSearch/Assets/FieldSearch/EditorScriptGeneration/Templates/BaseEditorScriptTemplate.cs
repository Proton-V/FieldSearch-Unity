using CodeGeneration.Base;
using FieldSearch.Attributes;
using FieldSearch.Core.GlobalEditor;
using System;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration.Templates
{
    public abstract class BaseEditorScriptTemplate : BaseScriptTemplate
    {
        protected Type DefaultBaseEditorType => Type.GetType(_defaultBaseEditorTypeName);

        [SerializeField]
        [TypeRefDropdown(typeof(BaseFieldSearchGlobalEditor))]
        protected string _defaultBaseEditorTypeName;
    }
}
