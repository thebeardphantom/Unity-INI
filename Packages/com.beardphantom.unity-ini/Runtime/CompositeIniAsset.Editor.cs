using BeardPhantom.UnityINI.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    public partial class CompositeIniAsset
    {
        #region Properties

        [field: HideInInspector]
        [field: SerializeField]
        internal FoldoutStates FoldoutStates { get; private set; } = new();

        #endregion

        #region Methods

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeStateChanged;
            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeStateChanged;
        }

        private void OnPlaymodeStateChanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                case PlayModeStateChange.EnteredPlayMode:
                {
                    break;
                }
                case PlayModeStateChange.ExitingEditMode:
                case PlayModeStateChange.ExitingPlayMode:
                {
                    Data = null;
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
                }
            }
        }

        #endregion
    }
}