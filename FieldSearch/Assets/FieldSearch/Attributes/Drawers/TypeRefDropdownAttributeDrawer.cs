using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Attributes.Drawers
{
    /// <summary>
    /// Drawer for <see cref="TypeRefDropdownAttribute"/>
    /// </summary>
    [CustomPropertyDrawer(typeof(TypeRefDropdownAttribute))]
    public class TypeRefDropdownAttributeDrawer : PropertyDrawer
    {
        TypeRefDropdownAttribute target;

        int currentTypeIndex;
        GUIContent[] displayedOptions;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (target == null)
            {
                Init(property);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            ShowDropDownTypeList(position, property, label);
            EditorGUI.EndProperty();
        }

        private void ShowDropDownTypeList(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            currentTypeIndex = EditorGUI.Popup(position, label, currentTypeIndex, displayedOptions);
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = target.InheritedTypeNameArray[currentTypeIndex];
                property.serializedObject.ApplyModifiedProperties();
                property.serializedObject.Update();
            }
        }

        /// <summary>
        /// First Init call
        /// </summary>
        /// <param name="property"></param>
        private void Init(SerializedProperty property)
        {
            target = attribute as TypeRefDropdownAttribute;
            currentTypeIndex = Array.IndexOf(target.InheritedTypeNameArray, property.stringValue);

            if (currentTypeIndex < 0)
            {
                Debug.LogWarning($"Type Index can't found for {property.displayName}");
                currentTypeIndex = 0;
            }

            displayedOptions = target.ShortInheritedTypeNameArray.Select(x => new GUIContent(x)).ToArray();
        }
    }
}
