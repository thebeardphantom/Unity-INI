using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityINI
{
    [CreateAssetMenu(menuName = "INI/Composite INI")]
    public class CompositeIniAsset : IniAssetBase
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

        [field: SerializeField]
        public override IniSerializedData Data { get; protected set; }

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

        [ContextMenu("Regenerate INI Data")]
        public void RegenerateData()
        {
            using var _ = ListPool<Layer>.Get(out var valid);
            valid.AddRange(Layers.Where(Layer.IsValidAndEnabled));
            if (valid.Count == 0)
            {
                return;
            }

            var parsedData = new IniData(valid[0].Asset.IniParsedData);
            for (var i = 1; i < valid.Count; i++)
            {
                parsedData.Merge(valid[i].Asset.IniParsedData);
            }

            Data = new IniSerializedData(parsedData);
        }

        private void OnValidate()
        {
            RegenerateData();
        }

        #endregion
    }
}