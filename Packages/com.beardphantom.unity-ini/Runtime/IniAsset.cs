using IniParser.Model;
using IniParser.Parser;
using System.IO;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    public class IniAsset : IniAssetBase, ISerializationCallbackReceiver
    {
        #region Properties

        [field: SerializeField]
        private string TextData { get; set; }

        [field: SerializeField]
        private SerializedIniParserConfig ParserConfig { get; set; } = SerializedIniParserConfig.Default;

        #endregion

        #region Methods

        public static IniAsset CreateFromPath(string path)
        {
            return CreateFromPath(path, SerializedIniParserConfig.Default);
        }

        public static IniAsset CreateFromString(string iniDataString)
        {
            return CreateFromString(iniDataString, SerializedIniParserConfig.Default);
        }

        public static IniAsset CreateFromPath(string path, SerializedIniParserConfig parserConfig)
        {
            var fileContents = File.ReadAllText(path);
            return CreateFromString(fileContents, parserConfig);
        }

        public static IniAsset CreateFromString(string iniDataString, SerializedIniParserConfig parserConfig)
        {
            var iniAsset = CreateInstance<IniAsset>();
            iniAsset.Populate(iniDataString, parserConfig);
            return iniAsset;
        }

        private void Populate(string iniDataString, SerializedIniParserConfig parserConfig)
        {
            var parser = new IniDataParser(parserConfig);
            ParserConfig = parserConfig;
            Data = string.IsNullOrWhiteSpace(TextData) ? new IniData() : parser.Parse(iniDataString);
            TextData = iniDataString;
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Populate(TextData, ParserConfig);
        }

        #endregion
    }
}