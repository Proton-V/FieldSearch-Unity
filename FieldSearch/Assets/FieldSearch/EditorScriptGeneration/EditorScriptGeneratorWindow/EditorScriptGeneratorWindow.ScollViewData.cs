using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration.Editor
{
    public partial class EditorScriptGeneratorWindow
    {
        /// <summary>
        /// ScrollViewData class for <see cref="EditorScriptGeneratorWindow"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ScrollViewData<T>
        {
            public ScrollViewData(Vector2 size) : this(size.x, size.y) { }

            public ScrollViewData(float width, float height)
            {
                size = new Vector2(width, height);
                objects = new List<T>();
            }

            public List<T> Objects => objects;

            public Vector2 size;
            public Vector2 scrollPosition;
            private List<T> objects;

            public void AddObjects(params T[] objs)
            {
                objects.AddRange(objs);
            }

            public void RemoveObjects(params T[] objs)
            {
                foreach (var obj in objs)
                {
                    objects.Remove(obj);
                }
            }
        }

        void ShowScrollViewSingleLayout(string label, ScrollViewData<Type> scrollViewData, Action<Type> onClickData)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(label, labelHeaderStyle, GUILayout.MinHeight(TB_OFFSET));

            scrollViewData.scrollPosition = GUILayout.BeginScrollView(scrollViewData.scrollPosition,
                false, false,
                GUIStyle.none, GUI.skin.verticalScrollbar,
                GUILayout.Width(scrollViewData.size.x), GUILayout.Height(scrollViewData.size.y));

            foreach (var data in scrollViewData.Objects.ToArray())
            {
                if (GUILayout.Button(data.Name, 
                    GUILayout.MinHeight(MIN_LABEL_AREA_HEIGHT),
                    GUILayout.MaxWidth(scrollViewData.size.x - BUTTON_SCROLLVIEW_RL_OFFSET)))
                {
                    onClickData(data);
                }
            }

            GUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }
    }
}
