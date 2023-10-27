using IniParser.Model.Configuration;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [ScriptedImporter(1, "ini")]
    public class IniAssetImporter : ScriptedImporter
    {
        #region Properties

        [field: SerializeField]
        private bool AllowDuplicateKeys { get; set; } = true;

        [field: SerializeField]
        private bool DuplicateKeysUseLastValue { get; set; } = true;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var text = File.ReadAllText(ctx.assetPath);
            var parserConfig = new IniParserConfiguration
            {
                AllowDuplicateKeys = AllowDuplicateKeys,
                OverrideDuplicateKeys = DuplicateKeysUseLastValue
            };
            var iniAsset = IniAsset.CreateFromString(text, parserConfig);
            ctx.AddObjectToAsset("MainAsset", iniAsset);
            ctx.SetMainObject(iniAsset);

            foreach (var keyValue in iniAsset.Data)
            {
                var keyValueAsset = ScriptableObject.CreateInstance<IniKeyValueAsset>();
                keyValueAsset.QualifiedKey = new IniQualifiedKey
                {
                    Key = keyValue.Key,
                    Section = keyValue.Section
                };
                keyValueAsset.IniAsset = iniAsset;
                var subassetName = keyValueAsset.QualifiedKey.ToString();
                keyValueAsset.name = subassetName;
                ctx.AddObjectToAsset(subassetName, keyValueAsset);
            }
        }

        #endregion
    }
}