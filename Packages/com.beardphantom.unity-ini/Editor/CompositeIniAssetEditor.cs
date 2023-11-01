using UnityEditor;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(CompositeIniAsset))]
    public class CompositeIniAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private static readonly string[] _props =
        {
            "<Layers>k__BackingField",
            "<Data>k__BackingField"
        };

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, _props);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_props[0]), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_props[1]), true);
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}