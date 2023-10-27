using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomPropertyDrawer(typeof(IniSerializedData))]
    public class IniSerializedDataPropertyDrawer : PropertyDrawer
    {
        #region Fields

        internal const string GLOBAL_SECTION_SPECIAL_DISPLAY_NAME = "\0";

        private static readonly string _sectionsPath = PropertyPathHelper.BuildPath(
            "Sections",
            "Sections",
            "SerializedKeyValuePairs");

        #endregion

        #region Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            var foldoutRect = new Rect(position)
            {
                height = singleLineHeight
            };

            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, label);
            if (property.isExpanded)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    position.yMin += singleLineHeight;
                    var globalSection = property.FindPropertyRelative("<Global>k__BackingField");
                    EditorGUI.PropertyField(position, globalSection, new GUIContent(GLOBAL_SECTION_SPECIAL_DISPLAY_NAME), true);

                    position.yMin += EditorGUI.GetPropertyHeight(globalSection, true);

                    var sections = property.FindPropertyRelative(_sectionsPath);
                    for (var i = 0; i < sections.arraySize; i++)
                    {
                        var element = sections.GetArrayElementAtIndex(i);
                        var key = element.FindPropertyRelative("<Key>k__BackingField");
                        var value = element.FindPropertyRelative("<Value>k__BackingField");

                        EditorGUI.PropertyField(position, value, new GUIContent(key.stringValue), true);
                        position.yMin += EditorGUI.GetPropertyHeight(value, true);
                    }
                }
            }

            EditorGUI.EndFoldoutHeaderGroup();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = 0f;
            height += EditorGUI.GetPropertyHeight(property, label, false);
            if (property.isExpanded)
            {
                var globalSection = property.FindPropertyRelative("<Global>k__BackingField");
                height += EditorGUI.GetPropertyHeight(globalSection, true);

                var sections = property.FindPropertyRelative(_sectionsPath);
                for (var i = 0; i < sections.arraySize; i++)
                {
                    var element = sections.GetArrayElementAtIndex(i);
                    var value = element.FindPropertyRelative("<Value>k__BackingField");
                    height += EditorGUI.GetPropertyHeight(value, true);
                }
            }

            return height;
        }

        #endregion
    }
}