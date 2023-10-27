using BeardPhantom.UnityINI;
using IniParser.Model;
using IniParser.Model.Configuration;
using IniParser.Parser;
using System.IO;
using UnityEngine;

public class IniAsset : IniAssetBase, ISerializationCallbackReceiver
{
    #region Properties

    [field: SerializeField]
    public override IniSerializedData Data { get; protected set; }

    internal IniData IniParsedData { get; private set; }

    #endregion

    #region Methods

    public static IniAsset CreateFromPath(string path, IniParserConfiguration parserConfig)
    {
        var fileContents = File.ReadAllText(path);
        return CreateFromString(fileContents, parserConfig);
    }

    public static IniAsset CreateFromString(string iniDataString, IniParserConfiguration parserConfig)
    {
        var iniAsset = CreateInstance<IniAsset>();
        iniAsset.Populate(iniDataString, parserConfig);
        return iniAsset;
    }

    public void Merge(IniAsset other)
    {
        IniParsedData.Merge(other.IniParsedData);
        Data = new IniSerializedData(IniParsedData);
    }

    private void Populate(string iniDataString, IniParserConfiguration parserConfig)
    {
        var parser = new IniDataParser(parserConfig);
        IniParsedData = parser.Parse(iniDataString);
        Data = new IniSerializedData(IniParsedData);
    }

    /// <inheritdoc />
    void ISerializationCallbackReceiver.OnBeforeSerialize() { }

    /// <inheritdoc />
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        if (IniParsedData != null)
        {
            // Only have to do this once
            return;
        }

        IniParsedData = new IniData();
        foreach (var qualifiedKeyValue in Data.Global)
        {
            IniParsedData.Global.AddKey(qualifiedKeyValue.QualifiedKey.Key, qualifiedKeyValue.Value);
        }

        foreach (var section in Data.Sections)
        {
            IniParsedData.Sections.AddSection(section.Name);
            var keyDataCollection = IniParsedData.Sections[section.Name];
            foreach (var qualifiedKeyValue in section)
            {
                keyDataCollection.AddKey(qualifiedKeyValue.QualifiedKey.Key, qualifiedKeyValue.Value);
            }
        }
    }

    #endregion
}