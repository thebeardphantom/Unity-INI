#if UNITY_EDITOR
using BeardPhantom.UnityINI.Editor;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    public abstract partial class IniAssetBase
    {
        #region Properties

        [field: SerializeField]
        internal FoldoutStates FoldoutStates { get; private set; } = new();

        #endregion
    }
}
#endif