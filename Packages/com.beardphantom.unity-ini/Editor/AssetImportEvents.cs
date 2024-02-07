using System;
using UnityEditor;

namespace BeardPhantom.UnityINI.Editor
{
    public class AssetImportEvents : AssetPostprocessor
    {
        #region Events

        public static event Action AssetsImported;

        #endregion

        #region Methods

        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (importedAssets?.Length > 0)
            {
                AssetsImported?.Invoke();
            }
        }

        #endregion
    }
}