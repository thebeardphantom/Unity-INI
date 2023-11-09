using IniParser.Model;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    public static class IniAssetEditorUtility
    {
        #region Fields

        private static GUIStyle _styleLeft;
        private static GUIStyle _styleRight;

        #endregion

        #region Methods

        public static void DrawIniData(IniAssetBase asset)
        {
            var foldoutStates = asset.FoldoutStates;

            foldoutStates.RootFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutStates.RootFoldout, "INI Data");
            if (foldoutStates.RootFoldout)
            {
                using var _ = new EditorGUI.IndentLevelScope();
                DrawKeyDataCollection(asset.Data.Global, null, foldoutStates);

                foreach (var section in asset.Data.Sections.OrderBy(s => s.SectionName))
                {
                    DrawKeyDataCollection(section.Keys, section.SectionName, foldoutStates);
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private static void DrawKeyDataCollection(KeyDataCollection collection, string sectionName, FoldoutStates foldoutStates)
        {
            var hasSectionName = !string.IsNullOrWhiteSpace(sectionName);
            if (!foldoutStates.DrawFoldout(hasSectionName ? sectionName : "\u2605Global", collection.Count > 0, true))
            {
                return;
            }

            if (_styleLeft == null)
            {
                _styleLeft = new GUIStyle(EditorStyles.label)
                {
                    fontStyle = FontStyle.Italic
                };

                _styleRight = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleRight,
                    fontStyle = FontStyle.Bold
                };
            }

            using var _ = new EditorGUI.IndentLevelScope();
            foreach (var keyData in collection.OrderBy(k => k.KeyName))
            {
                using var horizontalScope = new EditorGUILayout.HorizontalScope();
                var left = GUILayoutUtility.GetRect(
                    new GUIContent(keyData.KeyName),
                    _styleLeft,
                    GUILayout.ExpandWidth(false));

                var leftIndented = EditorGUI.IndentedRect(left);
                leftIndented.width = left.width;
                GUI.Label(leftIndented, keyData.KeyName, _styleLeft);

                var right = new Rect(horizontalScope.rect);
                right.xMin += leftIndented.width;
                GUI.Label(right, keyData.Value, _styleRight);

                if (HasContextClick(horizontalScope.rect))
                {
                    var menu = new GenericMenu();
                    menu.AddItem(
                        new GUIContent("Copy Full Key Name"),
                        false,
                        () =>
                        {
                            var fullKeyName = hasSectionName ? $"{sectionName}.{keyData.KeyName}" : keyData.KeyName;
                            GUIUtility.systemCopyBuffer = fullKeyName;
                        });
                    menu.AddItem(
                        new GUIContent("Copy Key Name"),
                        false,
                        () =>
                        {
                            GUIUtility.systemCopyBuffer = keyData.KeyName;
                        });
                    menu.AddItem(
                        new GUIContent("Copy Value"),
                        false,
                        () =>
                        {
                            GUIUtility.systemCopyBuffer = keyData.Value;
                        });
                    menu.ShowAsContext();
                }
            }
        }

        private static bool HasContextClick(Rect rect)
        {
            var evt = Event.current;
            return evt.type == EventType.ContextClick && rect.Contains(evt.mousePosition);
        }

        #endregion
    }
}