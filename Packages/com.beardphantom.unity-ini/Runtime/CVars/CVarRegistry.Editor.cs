#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.CVars
{
    [InitializeOnLoad]
    public partial class CVarRegistry
    {
        #region Fields

        private static bool _isPlayingInEditor;

        #endregion

        #region Constructors

        static CVarRegistry()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeStateChanged;
            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
        }

        private CVarRegistry()
        {
            EditorApplication.delayCall += DiscoverCVarsEditorOnly;
        }

        #endregion

        #region Methods

        private static void OnPlaymodeStateChanged(PlayModeStateChange stateChange)
        {
            _isPlayingInEditor = stateChange is PlayModeStateChange.EnteredPlayMode or PlayModeStateChange.ExitingEditMode;
            if (stateChange != PlayModeStateChange.EnteredEditMode)
            {
                return;
            }

            Debug.LogWarning("RESETTING REGISTRY FOR EDIT MODE");
            foreach (var cvar in Instance)
            {
                cvar.ClearEventListeners();
                cvar.ResetToDefault();
            }

            Instance.LogRegistrations();
        }

        private void DiscoverCVarsEditorOnly()
        {
            var cvarFields = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(f => f.FieldType == typeof(CVar))
                .Distinct();
            // Should force convars to register
            foreach (var cvarField in cvarFields)
            {
                cvarField.GetValue(null);
            }

            LogRegistrations();
        }

        #endregion
    }
}
#endif