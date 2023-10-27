using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [CreateAssetMenu(menuName = "INI/Composite INI")]
    public partial class CompositeIniAsset : IniAssetBase
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
            compositeIniAsset.RegenerateDataInEditor();
            return compositeIniAsset;
        }

        private void OnValidate()
        {
            RegenerateDataInEditor();
        }

        #endregion
    }
}