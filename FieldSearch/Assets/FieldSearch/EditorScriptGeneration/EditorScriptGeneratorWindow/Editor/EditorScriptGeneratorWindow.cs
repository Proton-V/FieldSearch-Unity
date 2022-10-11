using CodeGeneration;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.EditorScriptGeneration.Editor
{
    public partial class EditorScriptGeneratorWindow : EditorWindow
    {
        EditorScriptGenerator editorScriptGenerator;

        ScrollViewData<Type> typeDatas;
        ScrollViewData<Type> selectedTypeDatas;

        GUIStyle labelHeaderStyle;

        public static void Init(EditorScriptGeneratorSettings editorScriptGeneratorSettings)
        {
            EditorScriptGeneratorWindow window = (EditorScriptGeneratorWindow)
                GetWindow(typeof(EditorScriptGeneratorWindow));

            window.editorScriptGenerator = editorScriptGeneratorSettings.GeneratorInstance;

            var size = new Vector2(WIDTH, HEIGHT);
            window.minSize = size;
            window.maxSize = size;

            size.x -= RL_OFFSET * 2;
            size.x /= 2;
            size.y -= TB_OFFSET * 2;

            window.InitScrollView(size);
            window.InitStyles();
            window.Show();
        }

        void InitStyles()
        {
            labelHeaderStyle = new GUIStyle(EditorStyles.boldLabel);
            labelHeaderStyle.alignment = TextAnchor.MiddleCenter;
        }

        void InitScrollView(Vector2 size)
        {
            var allEditorClasses = CodeGenerationUtils.GetAllAvailableEditorTypes();

            typeDatas = new ScrollViewData<Type>(size);
            typeDatas.AddObjects(allEditorClasses);

            selectedTypeDatas = new ScrollViewData<Type>(size);
        }

        void OnGUI()
        {
            if(editorScriptGenerator == null)
            {
                Close();
                return;
            }

            ShowScrollViewLayout();
            ShowButtonsLayout();
        }

        void ShowButtonsLayout()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Try update all Editors", GUILayout.MinHeight(TB_OFFSET)))
            {
                var allTypes = typeDatas.Objects.ToList();
                allTypes.AddRange(selectedTypeDatas.Objects);

                TryCreateEditors(allTypes.ToArray());
                Close();
            }
            if (GUILayout.Button("Try update selected Editors", GUILayout.MinHeight(TB_OFFSET)))
            {
                TryCreateEditors(selectedTypeDatas.Objects.ToArray());
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }

        void TryCreateEditors(params Type[] types)
        {
            editorScriptGenerator.CreateScripts(inputTypes: types);
        }

        void ShowScrollViewLayout()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(RL_OFFSET);

            ShowScrollViewSingleLayout("Editor Types", typeDatas, OnClickAllTypesData);
            ShowScrollViewSingleLayout("Selected Types", selectedTypeDatas, OnClickSelectedTypeData);

            GUILayout.Space(RL_OFFSET);
            EditorGUILayout.EndHorizontal();
        }

        void OnClickAllTypesData(Type type)
        {
            typeDatas.RemoveObjects(type);
            selectedTypeDatas.AddObjects(type);
        }

        void OnClickSelectedTypeData(Type type)
        {
            selectedTypeDatas.RemoveObjects(type);
            typeDatas.AddObjects(type);
        }
    }
}
