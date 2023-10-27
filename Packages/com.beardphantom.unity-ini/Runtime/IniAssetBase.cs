using BeardPhantom.UnityINI;
using UnityEngine;

public abstract partial class IniAssetBase : ScriptableObject, IIniAsset
{
    #region Properties

    [field: SerializeField]
    public IniSerializedData Data { get; protected set; } = IniSerializedData.Default;

    #endregion
}