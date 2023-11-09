using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [CreateAssetMenu(menuName = "INI/Composite INI")]
    public class CompositeIniAsset : IniAssetBase, ISerializationCallbackReceiver
    {
        #region Types

        [Serializable]
        public class Layer
        {
            #region Properties

            [field: SerializeField]
            public bool Enabled { get; set; } = true;

            [field: SerializeField]
            public IniAsset Asset { get; set; }

            #endregion

            #region Methods

            public static bool IsValidAndEnabled(Layer layer)
            {
                return layer is { Enabled: true } && layer.Asset != null;
            }

            #endregion
        }

        #endregion

        #region Properties

        [field: SerializeField]
        public List<Layer> Layers { get; private set; } = new();

        #endregion

        #region Methods

        public static CompositeIniAsset Create(IEnumerable<IniAsset> iniAssets)
        {
            var compositeIniAsset = CreateInstance<CompositeIniAsset>();
            compositeIniAsset.Layers.AddRange(
                iniAssets.Select(
                    i => new Layer
                    {
                        Asset = i,
                        Enabled = true
                    }));
            compositeIniAsset.RegenerateData();
            return compositeIniAsset;
        }

        /// <inheritdoc />
        public void OnBeforeSerialize() { }

        /// <inheritdoc />
        public void OnAfterDeserialize()
        {
            RegenerateData();
        }

        private void RegenerateData()
        {
            Data = new IniData();
            foreach (var layer in Layers)
            {
                if (!layer.Enabled || layer.Asset == null)
                {
                    continue;
                }

                Data.Merge(layer.Asset.Data);
            }
        }

        #endregion
    }
}