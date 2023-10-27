using System.IO;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(IniAsset))]
    public class IniAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private static bool _contentsFoldout;

        private string _contents;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            using (new EditorGUI.DisabledScope(false))
            {
                _contentsFoldout = EditorGUILayout.Foldout(_contentsFoldout, "File Contents", true);
                if (_contentsFoldout)
                {
                    EditorGUILayout.TextArea(_contents, EditorStyles.textArea, GUILayout.ExpandHeight(true));
                }
            }

            DrawPropertiesExcluding(serializedObject, "m_Script");
        }

        private void OnEnable()
        {
            var path = AssetDatabase.GetAssetPath(target);
            _contents = File.ReadAllText(path);
        }

        #endregion
    }
}