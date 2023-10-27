#if UNITY_EDITOR
using BeardPhantom.UnityINI.Editor;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class IniAsset
{
    #region Methods

    /// <inheritdoc />
    [ContextMenu("Regenerate")]
    public override void RegenerateDataInEditor()
    {
        var path = AssetDatabase.GetAssetPath(this);
        var assetImporter = (IniAssetImporter)AssetImporter.GetAtPath(path);
        var parserConfig = assetImporter.GetParserConfig();
        var text = File.ReadAllText(path);
        Populate(text, parserConfig);
    }

    #endregion
}
#endif