using IniParser.Model;
using IniParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityINI
{
    [CreateAssetMenu(menuName = "INI/Composite INI")]
    public partial class CompositeIniAsset : ScriptableObject
    {
        #region Types

        [Serializable]
        public class Layer
        {
            #region Properties

            [field: SerializeField]
            public bool Enabled { get; set; } = true;

            [field: SerializeField]
            public TextAsset Asset { get; set; }

            #endregion

            #region Methods

            public static bool IsValidAndEnabled(Layer layer)
            {
                return layer is { Enabled: true } && layer.Asset != null;
            }

            #endregion
        }

        private enum DataLoadState
        {
            Unloaded = 0,
            LoadedInPlayMode = 1,
            LoadedInEditMode = 2
        }

        #endregion

        #region Fields

        private IniData _data;

        private DataLoadState _dataLoadState;

        #endregion

        #region Properties

        [field: SerializeField]
        public IniDataConfig DataConfig { get; private set; } = IniDataConfig.Default;

        [field: SerializeField]
        public List<Layer> Layers { get; private set; } = new();

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
            private set => _data = value;
        }

        #endregion

        #region Methods

        public static CompositeIniAsset Create(IEnumerable<string> iniDatas)
        {
            return Create(iniDatas.Select(iniData => new TextAsset(iniData)));
        }

        public static CompositeIniAsset Create(IEnumerable<TextAsset> iniAssets)
        {
            var compositeIniAsset = CreateInstance<CompositeIniAsset>();
            compositeIniAsset.Layers.AddRange(
                iniAssets.Select(
                    textAsset => new Layer
                    {
                        Asset = textAsset,
                        Enabled = true
                    }));
            compositeIniAsset.RebuildData();
            return compositeIniAsset;
        }

        public void RebuildData()
        {
            _dataLoadState = Application.isPlaying ? DataLoadState.LoadedInPlayMode : DataLoadState.LoadedInEditMode;
            var sb = new StringBuilder();
            foreach (var layer in Layers)
            {
                if (!layer.Enabled || layer.Asset == null)
                {
                    continue;
                }

                sb.AppendLine(layer.Asset.text);
            }

            Data = new IniData();
            if (sb.Length == 0)
            {
                return;
            }

            var parser = new IniDataParser(DataConfig);
            Data = parser.Parse(sb.ToString());
        }

        #endregion
    }
}