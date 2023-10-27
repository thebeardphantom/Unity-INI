using IniParser.Model.Configuration;
using System.IO;
using UnityEditor;
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

        [MenuItem("Assets/Create/INI/INI File", priority = -100)]
        public static void CreateIniAsset()
        {
            // IniTemplate
            var iniTemplatePath = AssetDatabase.GUIDToAssetPath("af6e9c5cc66f40598a00fa85105c403e");
            var iniTemplateAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(iniTemplatePath);
            var newAssetPath = "Assets/NewIni.ini";
            newAssetPath = AssetDatabase.GenerateUniqueAssetPath(newAssetPath);
            ProjectWindowUtil.CreateAssetWithContent(newAssetPath, iniTemplateAsset.text);
        }

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
                keyValueAsset.QualifiedKey = new IniQualifiedKey(
                    keyValue.QualifiedKey.Section,
                    keyValue.QualifiedKey.Key);
                keyValueAsset.IniAsset = iniAsset;
                var subassetName = keyValueAsset.QualifiedKey.ToString();
                keyValueAsset.name = subassetName;
                ctx.AddObjectToAsset(subassetName, keyValueAsset);
            }
        }

        #endregion
    }
}