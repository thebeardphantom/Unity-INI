using IniParser.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityINI
{
    [CreateAssetMenu(menuName = nameof(MultiIniAsset))]
    public class MultiIniAsset : ScriptableObject, IIniAsset
    {
        #region Properties

        [field: SerializeField]
        private List<IniAsset> IniAssets { get; set; } = new();

        [field: SerializeField]
        public IniSerializedData Data { get; private set; }

        #endregion

        #region Methods

        public static MultiIniAsset Create(IEnumerable<IniAsset> iniAssets)
        {
            var multiIniAsset = CreateInstance<MultiIniAsset>();
            multiIniAsset.IniAssets.AddRange(iniAssets);
            multiIniAsset.RegenerateData();
            return multiIniAsset;
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