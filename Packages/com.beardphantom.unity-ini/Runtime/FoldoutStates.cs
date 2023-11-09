#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [Serializable]
    internal class FoldoutStates
    {
        #region Properties

        [field: SerializeField]
        public bool RootFoldout { get; set; } = true;

        [field: SerializeField]
        private SerializedDictionary<string, bool> States { get; set; } = new();

        #endregion

        #region Methods

        public bool DrawFoldout(string label, bool hasContent, bool defaultState = false)
        {
            using var _ = new EditorGUI.DisabledScope(!hasContent);
            if (!States.TryGetValue(label, out var state))
            {
                state = defaultState;
            }

            state &= hasContent;
            state = EditorGUILayout.Foldout(state, label, true);
            States[label] = state;
            return state;
        }

        #endregion
    }
}
#endif