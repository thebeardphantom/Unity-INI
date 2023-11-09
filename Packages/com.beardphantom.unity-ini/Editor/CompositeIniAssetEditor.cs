using System;
using UnityEditor;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(CompositeIniAsset))]
    public class CompositeIniAssetEditor : UnityEditor.Editor
    {
        #region Methods

        private static DateTime TimeStampToDateTime(ulong assetTimeStamp)
        {
            var time = new DateTime((long)assetTimeStamp);
            time = time.ToLocalTime();
            return time;
        }

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();

            // Detect data changes and rebuild
            var asset = (CompositeIniAsset)target;
            foreach (var layer in asset.Layers)
            {
                if (layer.Enabled && layer.Asset != null && layer.Asset.TextDataHash != layer.CachedTextDataHash)
                {
                    asset.RebuildData();
                    break;
                }
            }

            IniAssetEditorUtility.DrawIniData(asset);
            serializedObject.ApplyModifiedProperties();
        }

        /// <inheritdoc />
        protected override bool ShouldHideOpenButton()
        {
            return true;
        }

        #endregion
    }
}