using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace BeardPhantom.UnityINI.Editor
{
    [ScriptedImporter(1, "ini")]
    public class IniFileImporter : ScriptedImporter
    {
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
            var textAsset = new TextAsset(text);
            ctx.AddObjectToAsset("MainAsset", textAsset);
            ctx.SetMainObject(textAsset);
        }

        #endregion
    }
}