#if UNITY_EDITOR
using BeardPhantom.UnityINI.Editor;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    public abstract partial class IniAssetBase
    {
        #region Properties

        [field: SerializeField]
        [field: HideInInspector]
        internal FoldoutStates FoldoutStates { get; private set; } = new();

        #endregion

        #region Methods

        private void OnValidate()
        {
            RebuildData();
        }

        #endregion
    }
}
#endif