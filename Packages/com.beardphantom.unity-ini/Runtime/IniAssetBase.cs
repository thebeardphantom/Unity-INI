using BeardPhantom.UnityINI;
using UnityEngine;

public abstract class IniAssetBase : ScriptableObject, IIniAsset
{
    #region Properties

    /// <inheritdoc />
    public abstract IniSerializedData Data { get; protected set; }

    #endregion
}