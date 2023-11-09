using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(CompositeIniAsset))]
    public class CompositeIniAssetEditor : UnityEditor.Editor
    {
        #region Properties

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();
            var asset = (CompositeIniAsset)target;
            IniAssetEditorUtility.DrawIniData(asset);
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}