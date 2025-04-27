#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace VodVas.UIOptimizer
{
    public class UIOptimizerView
    {
        private readonly UIOptimizerPresenter _presenter;

        private GUIStyle _headerStyle;
        private GUIStyle _sectionStyle;
        private Vector2 _mainScroll;
        private Vector2 _exclusionScroll;
        private string _searchFilter = "";
        private bool _stylesInitialized;

        public UIOptimizerView(UIOptimizerPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Draw()
        {
            InitializeStyles();

            _mainScroll = EditorGUILayout.BeginScrollView(_mainScroll);

            DrawHeader();
            EditorGUILayout.Space(30);
            DrawProfileSelector();
            DrawConfiguration();
            DrawExclusionRules();
            DrawDragDropSection();
            DrawExcludedList();
            DrawActionButtons();

            var results = _presenter.GetLastResults();
            if (results.ProcessedTextCount > 0 ||
                results.ProcessedImageCount > 0 ||
                results.ProcessedPanelCount > 0)
            {
                DrawResults(results);
            }

            EditorGUILayout.EndScrollView();
        }

        private void InitializeStyles()
        {
            if (_stylesInitialized) return;

            _headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                normal = {
                    textColor = new Color(1f, 0.5f, 0.2f),
                    background = CreateColoredTex(new Color(0f, 0f, 0f))
                },
                padding = new RectOffset(15, 15, 12, 12),
                margin = new RectOffset(0, 0, 15, 15),
                fixedHeight = 60,
                stretchWidth = true
            };

            _sectionStyle = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(10, 10, 10, 10),
                margin = new RectOffset(5, 5, 5, 5)
            };

            _stylesInitialized = true;
        }

        private Texture2D CreateColoredTex(Color col)
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, col);
            tex.Apply();
            return tex;
        }

        private void DrawHeader()
        {
            EditorGUILayout.LabelField("UI OPTIMIZER PRO", _headerStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.Space(10);
        }

        private void DrawProfileSelector()
        {
            EditorGUILayout.BeginVertical(_sectionStyle);

            UIOptimizerProfile profile = _presenter.Profile;
            EditorGUILayout.ObjectField("Profile", profile, typeof(UIOptimizerProfile), false);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Profile", GUILayout.Width(120)))
            {
                _presenter.SaveProfile();
            }

            if (GUILayout.Button("Create New", GUILayout.Width(120)))
            {
                _presenter.CreateNewProfile();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);
        }

        private void DrawConfiguration()
        {
            UIOptimizerProfile profile = _presenter.Profile;
            SerializedObject profileObj = new SerializedObject(profile);
            profileObj.Update();

            EditorGUILayout.BeginVertical(_sectionStyle);

            SerializedProperty includeInactiveProp = profileObj.FindProperty("_includeInactive");
            EditorGUILayout.PropertyField(includeInactiveProp, new GUIContent("Include Inactive"));

            EditorGUILayout.Space(10);

            DrawComponentSettings(profileObj, "Text Components", "_textSettings");
            DrawComponentSettings(profileObj, "Image Components", "_imageSettings");
            DrawComponentSettings(profileObj, "Raw Image Components", "_rawImageSettings");
            DrawComponentSettings(profileObj, "Panel Components", "_panelSettings");

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            profileObj.ApplyModifiedProperties();
        }

        private void DrawComponentSettings(SerializedObject profileObj, string label, string propertyPath)
        {
            SerializedProperty settingsProp = profileObj.FindProperty(propertyPath);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            SerializedProperty processProp = settingsProp.FindPropertyRelative("_process");
            SerializedProperty disableMaskableProp = settingsProp.FindPropertyRelative("_disableMaskable");
            SerializedProperty disableRaycastProp = settingsProp.FindPropertyRelative("_disableRaycast");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel, GUILayout.Width(120));
            EditorGUILayout.PropertyField(processProp, GUIContent.none, GUILayout.Width(40));
            EditorGUILayout.EndHorizontal();

            if (processProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(disableMaskableProp, new GUIContent("Disable Maskable"));
                EditorGUILayout.PropertyField(disableRaycastProp, new GUIContent("Disable Raycast"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        private void DrawExclusionRules()
        {
            UIOptimizerProfile profile = _presenter.Profile;
            SerializedObject profileObj = new SerializedObject(profile);
            profileObj.Update();

            EditorGUILayout.BeginVertical(_sectionStyle);
            EditorGUILayout.LabelField("Exclusion Rules", EditorStyles.boldLabel);

            SerializedProperty excludeButtonsProp = profileObj.FindProperty("_excludeButtons");
            SerializedProperty excludeTextProp = profileObj.FindProperty("_excludeText");
            SerializedProperty excludeTMPTextProp = profileObj.FindProperty("_excludeTMPText");
            SerializedProperty excludeSlidersProp = profileObj.FindProperty("_excludeSliders");
            SerializedProperty excludeTogglesProp = profileObj.FindProperty("_excludeToggles");
            SerializedProperty excludeScrollbarsProp = profileObj.FindProperty("_excludeScrollbars");
            SerializedProperty excludeDropdownsProp = profileObj.FindProperty("_excludeDropdowns");
            SerializedProperty excludeInputFieldsProp = profileObj.FindProperty("_excludeInputFields");
            SerializedProperty excludeEventTriggersProp = profileObj.FindProperty("_excludeEventTriggers");

            EditorGUILayout.PropertyField(excludeButtonsProp, new GUIContent("Exclude Buttons"));
            EditorGUILayout.PropertyField(excludeTextProp, new GUIContent("Exclude Legacy Text"));
            EditorGUILayout.PropertyField(excludeTMPTextProp, new GUIContent("Exclude TMP Text"));
            EditorGUILayout.PropertyField(excludeSlidersProp, new GUIContent("Exclude Sliders"));
            EditorGUILayout.PropertyField(excludeTogglesProp, new GUIContent("Exclude Toggles"));
            EditorGUILayout.PropertyField(excludeScrollbarsProp, new GUIContent("Exclude Scrollbars"));
            EditorGUILayout.PropertyField(excludeDropdownsProp, new GUIContent("Exclude Dropdowns"));
            EditorGUILayout.PropertyField(excludeInputFieldsProp, new GUIContent("Exclude Input Fields"));
            EditorGUILayout.PropertyField(excludeEventTriggersProp, new GUIContent("Exclude Event Triggers"));

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            profileObj.ApplyModifiedProperties();
        }


        private void DrawDragDropSection()
        {
            EditorGUILayout.BeginVertical(_sectionStyle);

            var rect = GUILayoutUtility.GetRect(0, 50, GUILayout.ExpandWidth(true));
            GUI.Box(rect, "Drag Objects To Exclude", EditorStyles.centeredGreyMiniLabel);

            if (rect.Contains(Event.current.mousePosition))
                HandleDragAndDrop(rect);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);
        }

        private void HandleDragAndDrop(Rect dropArea)
        {
            Event currentEvent = Event.current;

            if (currentEvent.type == EventType.DragUpdated || currentEvent.type == EventType.DragPerform)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (currentEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (UnityEngine.Object obj in DragAndDrop.objectReferences)
                    {
                        if (obj is GameObject go)
                        {
                            _presenter.AddExclusion(go);
                        }
                    }

                    currentEvent.Use();
                }
            }
        }

        private void DrawExcludedList()
        {
            UIOptimizerProfile profile = _presenter.Profile;
            SerializedObject profileObj = new SerializedObject(profile);
            profileObj.Update();

            EditorGUILayout.BeginVertical(_sectionStyle);

            EditorGUILayout.BeginHorizontal();
            _searchFilter = EditorGUILayout.TextField("Search:", _searchFilter, GUILayout.Width(200));

            if (GUILayout.Button("Clear", GUILayout.Width(100)))
            {
                _presenter.ClearExclusions();
            }
            EditorGUILayout.EndHorizontal();

            _exclusionScroll = EditorGUILayout.BeginScrollView(_exclusionScroll, GUILayout.Height(100));

            SerializedProperty exclusionsProp = profileObj.FindProperty("_manualExclusions");

            for (int i = 0; i < exclusionsProp.arraySize; i++)
            {
                SerializedProperty elementProp = exclusionsProp.GetArrayElementAtIndex(i);
                GameObject obj = elementProp.objectReferenceValue as GameObject;

                if (obj == null)
                {
                    exclusionsProp.DeleteArrayElementAtIndex(i);
                    i--;
                    continue;
                }

                if (!string.IsNullOrEmpty(_searchFilter) &&
                   !obj.name.Contains(_searchFilter, StringComparison.OrdinalIgnoreCase)) continue;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(obj, typeof(GameObject), true);

                if (GUILayout.Button("×", GUILayout.Width(20)))
                {
                    _presenter.RemoveExclusion(i);
                    GUIUtility.ExitGUI();
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            profileObj.ApplyModifiedProperties();
        }

        private void DrawActionButtons()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Process Scene", GUILayout.Height(40)))
            {
                _presenter.ProcessScene();
            }

            if (GUILayout.Button("Validate Exclusions", GUILayout.Height(40)))
            {
                _presenter.ValidateExclusions();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawResults(OptimizationResults results)
        {
            EditorGUILayout.BeginVertical(_sectionStyle);
            EditorGUILayout.LabelField("Optimization Results", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Text components: {results.ProcessedTextCount}/{results.ScannedTextCount}");
            EditorGUILayout.LabelField($"TMP Text components: {results.ProcessedTMPTextCount}/{results.ScannedTMPTextCount}");
            EditorGUILayout.LabelField($"Image components: {results.ProcessedImageCount}/{results.ScannedImageCount}");
            EditorGUILayout.LabelField($"Raw Image components: {results.ProcessedRawImageCount}/{results.ScannedRawImageCount}");
            EditorGUILayout.LabelField($"Panel components: {results.ProcessedPanelCount}/{results.ScannedPanelCount}");
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField($"Total processed: {results.TotalProcessed}/{results.TotalScanned}");
            EditorGUILayout.LabelField($"Processing time: {results.ProcessingTime.TotalMilliseconds:F2}ms");
            EditorGUILayout.EndVertical();
        }
    }
}
#endif