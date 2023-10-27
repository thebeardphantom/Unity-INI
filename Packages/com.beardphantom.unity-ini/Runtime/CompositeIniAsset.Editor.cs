#if UNITY_EDITOR
using IniParser.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityINI
{
    public partial class CompositeIniAsset
    {
        #region Methods

        /// <inheritdoc />
        [ContextMenu("Regenerate")]
        public override void RegenerateDataInEditor()
        {
            using var _ = ListPool<Layer>.Get(out var valid);
            valid.AddRange(Layers.Where(Layer.IsValidAndEnabled));
            Data = IniSerializedData.Default;
            if (valid.Count == 0)
            {
                return;
            }

            valid[0].Asset.RegenerateDataInEditor();
            var parsedData = new IniData(valid[0].Asset.IniParsedData);
            for (var i = 1; i < valid.Count; i++)
            {
                var iniAsset = valid[i].Asset;
                iniAsset.RegenerateDataInEditor();
                parsedData.Merge(iniAsset.IniParsedData);
            }

            Data = new IniSerializedData(parsedData);
        }

        #endregion
    }
}
#endif