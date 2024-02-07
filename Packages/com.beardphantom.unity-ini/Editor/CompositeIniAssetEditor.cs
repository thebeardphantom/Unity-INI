using UnityEditor;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(CompositeIniAsset))]
    public class CompositeIniAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private CompositeIniAsset _asset;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            if (DrawDefaultInspector())
            {
                RebuildData();
            }

            IniAssetEditorUtility.DrawIniData(_asset);
        }

        /// <inheritdoc />
        protected override bool ShouldHideOpenButton()
        {
            return true;
        }

        private void OnEnable()
        {
            AssetImportEvents.AssetsImported -= OnAssetsImported;
            AssetImportEvents.AssetsImported += OnAssetsImported;
            _asset = (CompositeIniAsset)target;
            RebuildData();
        }

        private void OnDisable()
        {
            AssetImportEvents.AssetsImported -= OnAssetsImported;
        }

        private void RebuildData()
        {
            _asset.RebuildData();
        }

        private void OnAssetsImported()
        {
            RebuildData();
        }

        #endregion
    }
}