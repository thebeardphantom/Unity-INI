using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomPropertyDrawer(typeof(IniDataConfig))]
    public class IniDataConfigPropertyDrawer : PropertyDrawer
    {
        #region Fields

        private const string COMMAND_STRING_PROPERTY_NAME = "<" + nameof(IniDataConfig.CommentString) + ">k__BackingField";

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            var commentStringProperty = property.FindPropertyRelative(COMMAND_STRING_PROPERTY_NAME);
            if (string.IsNullOrWhiteSpace(commentStringProperty.stringValue))
            {
                commentStringProperty.stringValue = IniDataConfig.COMMENT_STRING_DEFAULT;
            }
        }

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        #endregion
    }
}