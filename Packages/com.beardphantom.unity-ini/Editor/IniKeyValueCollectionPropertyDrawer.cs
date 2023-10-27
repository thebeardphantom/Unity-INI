using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomPropertyDrawer(typeof(IniKeyValueCollection))]
    public class IniKeyValueCollectionPropertyDrawer : PropertyDrawer
    {
        #region Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) { }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // var serializedData = GetIniData(property);
            // var singleLineHeight = EditorGUIUtility.singleLineHeight;
            // if (property.isExpanded)
            // {
            //     return singleLineHeight;
            // }
            var size = property.FindPropertyRelative("<KeysToValues>k__BackingField.<SerializedKeyValuePairs>k__BackingField")
                .arraySize;
            return EditorGUIUtility.singleLineHeight * size;
        }

        #endregion
    }
}