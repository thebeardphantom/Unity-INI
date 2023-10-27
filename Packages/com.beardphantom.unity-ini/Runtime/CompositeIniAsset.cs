using IniParser.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityINI
{
    [CreateAssetMenu(menuName = "INI/Composite INI")]
    public class CompositeIniAsset : ScriptableObject, IIniAsset
    {
        #region Properties

        [field: SerializeField]
        public List<IniAsset> IniAssets { get; private set; } = new();

        [field: SerializeField]
        public IniSerializedData Data { get; private set; }

        #endregion

        #region Methods

        public static CompositeIniAsset Create(IEnumerable<IniAsset> iniAssets)
        {
            var compositeIniAsset = CreateInstance<CompositeIniAsset>();
            compositeIniAsset.IniAssets.AddRange(iniAssets);
            compositeIniAsset.RegenerateData();
            return compositeIniAsset;
        }

        [ContextMenu("Regenerate INI Data")]
        public void RegenerateData()
        {
            using var _ = ListPool<IniAsset>.Get(out var valid);
            valid.AddRange(IniAssets.Where(i => i != null));
            if (valid.Count == 0)
            {
                return;
            }

            var parsedData = new IniData(valid[0].IniParsedData);
            for (var i = 1; i < valid.Count; i++)
            {
                parsedData.Merge(valid[i].IniParsedData);
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