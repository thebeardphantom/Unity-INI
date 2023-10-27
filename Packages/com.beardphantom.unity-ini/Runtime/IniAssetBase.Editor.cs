#if UNITY_EDITOR
public abstract partial class IniAssetBase : IIniEditorAsset
{
    #region Methods

    /// <inheritdoc />
    public abstract void RegenerateDataInEditor();

    #endregion
}
#endif