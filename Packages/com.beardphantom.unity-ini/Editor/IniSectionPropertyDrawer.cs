using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomPropertyDrawer(typeof(IniSection))]
    public class IniSectionPropertyDrawer : PropertyDrawer
    {
        #region Fields

        private static readonly string _keyValuesPropertyPath = PropertyPathHelper.BuildPath(
            "KeyValueCollection",
            "KeysToValues",
            "SerializedKeyValuePairs");

        #endregion

        #region Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            position = new Rect(position)
            {
                height = singleLineHeight
            };
            var foldoutStyle = EditorStyles.foldout;
            var useSpecialDrawing = label.text == IniSerializedDataPropertyDrawer.GLOBAL_SECTION_SPECIAL_DISPLAY_NAME;
            if (useSpecialDrawing)
            {
                label.text = "Global";
                foldoutStyle = new GUIStyle(foldoutStyle)
                {
                    fontStyle = FontStyle.Bold
                };
                GUI.contentColor = Color.yellow;
            }

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true, foldoutStyle);
            GUI.contentColor = Color.white;
            if (!property.isExpanded)
            {
                return;
            }

            using (new EditorGUI.IndentLevelScope())
            {
                var kvpStart = property.FindPropertyRelative(_keyValuesPropertyPath);
                for (var i = 0; i < kvpStart.arraySize; i++)
                {
                    position.y += singleLineHeight;
                    var element = kvpStart.GetArrayElementAtIndex(i);

                    var key = element.FindPropertyRelative("<Key>k__BackingField");
                    var left = new Rect(position);
                    left = EditorGUI.IndentedRect(left);
                    left.width /= 2f;
                    GUI.Label(left, key.stringValue);

                    var value = element.FindPropertyRelative("<Value>k__BackingField");
                    var right = new Rect(left);
                    right.x += right.width;
                    GUI.Label(right, value.stringValue);

                    var evt = Event.current;
                    if (evt.type == EventType.ContextClick && position.Contains(evt.mousePosition))
                    {
                        var sectionName = property.FindPropertyRelative("<Name>k__BackingField").stringValue;
                        var keyStringValue = key.stringValue;
                        var valueStringValue = value.stringValue;
                        var menu = new GenericMenu();
                        menu.AddItem(
                            new GUIContent("Copy Qualified Key"),
                            false,
                            () =>
                            {
                                GUIUtility.systemCopyBuffer = $"{sectionName}.{keyStringValue}";
                            });
                        menu.AddItem(
                            new GUIContent("Copy Key"),
                            false,
                            () =>
                            {
                                GUIUtility.systemCopyBuffer = keyStringValue;
                            });
                        menu.AddItem(
                            new GUIContent("Copy Value"),
                            false,
                            () =>
                            {
                                GUIUtility.systemCopyBuffer = valueStringValue;
                            });
                        menu.ShowAsContext();
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            if (!property.isExpanded)
            {
                return singleLineHeight;
            }

            var size = property.FindPropertyRelative(_keyValuesPropertyPath).arraySize;
            return singleLineHeight + singleLineHeight * size;
        }

        #endregion
    }
}