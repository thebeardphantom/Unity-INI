using IniParser.Model;
using UnityEngine;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityINI
{
    public abstract partial class IniAssetBase : ScriptableObject, IIniAsset, ISerializationCallbackReceiver
    {
        #region Fields

        private IniData _data;

        #endregion

        #region Properties

        /// <inheritdoc />
        public IniData Data
        {
            get
            {
                if (_data == null)
                {
                    RebuildData();
                }

                Assert.IsNotNull(_data, "_data != null");
                return _data;
            }
            protected set => _data = value;
        }

        #endregion

        #region Methods

        internal abstract void RebuildData();

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            RebuildData();
        }

        #endregion
    }
}