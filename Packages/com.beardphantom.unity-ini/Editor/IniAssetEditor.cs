using System.IO;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(IniAsset))]
    public class IniAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private static readonly GUILayoutOption[] _guiLayoutOption =
        {
            GUILayout.ExpandHeight(true)
        };

        private static bool _contentsFoldout;

        private string _contents;

        #endregion

        #region Properties

        [field: SerializeField]
        private FoldoutStates FoldoutStates { get; set; } = new();

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            using (new EditorGUI.DisabledScope(false))
            {
                _contentsFoldout = EditorGUILayout.Foldout(_contentsFoldout, "File Contents", true);
                if (_contentsFoldout)
                {
                    EditorGUILayout.TextArea(_contents, EditorStyles.textArea, _guiLayoutOption);
                }
            }

            var asset = (IniAsset)target;
            IniAssetEditorUtility.DrawIniData(asset);
        }

        private void OnEnable()
        {
            var path = AssetDatabase.GetAssetPath(target);
            _contents = File.ReadAllText(path);
        }

        #endregion
    }
}