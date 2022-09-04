using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FieldSearch.Core.Inspectors.Base
{
	public abstract class BaseSearchableEditorConfigObject : ScriptableObject
	{
		public abstract void OnEnableInspector(Object target, SerializedObject serializedObject);
		public abstract void OnDisableInspector(Object target);
		public abstract void OnInspectorGUI(SerializedObject serializedObject, Action defaultOnInspectorGUI);
	}
}
