using IniParser.Model;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    public abstract partial class IniAssetBase : ScriptableObject, IIniAsset
    {
        #region Properties

        /// <inheritdoc />
        public IniData Data { get; protected set; }
        
        

        #endregion
    }
}