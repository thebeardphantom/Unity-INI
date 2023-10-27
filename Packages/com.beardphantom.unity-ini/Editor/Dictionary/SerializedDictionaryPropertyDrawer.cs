using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryPropertyDrawer : PropertyDrawer
    {
        #region Fields

        private static readonly string _propertyPath =
            $"<{SerializedDictionary<object, object>.SERIALIZED_KEY_VALUE_PAIRS_PROPERTY_NAME}>k__BackingField";

        #endregion

        #region Methods

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var serializedValuesProperty = property.FindPropertyRelative(_propertyPath);
            return new PropertyField(serializedValuesProperty, property.displayName);
        }

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        #endregion
    }
}