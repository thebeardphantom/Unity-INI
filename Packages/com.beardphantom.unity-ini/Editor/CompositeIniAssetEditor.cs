using System.Linq;
using UnityEditor;
using UnityEngine.Pool;

namespace BeardPhantom.UnityINI.Editor
{
    [CustomEditor(typeof(CompositeIniAsset))]
    public class CompositeIniAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private static readonly string[] _props =
        {
            "<Layers>k__BackingField",
            "<Data>k__BackingField"
        };

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, _props);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_props[0]), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_props[1]), true);
            if (!serializedObject.ApplyModifiedProperties())
            {
                return;
            }

            var needsSaving = false;
            var obj = (CompositeIniAsset)target;
            var assetPath = AssetDatabase.GetAssetPath(obj);
            using var _ = ListPool<IniKeyValueAsset>.Get(out var subAssets);
            var allValidSubassets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath)
                .Where(a => a != null)
                .Cast<IniKeyValueAsset>();
            subAssets.AddRange(allValidSubassets);

            // Remove all subassets that aren't in the INI data
            foreach (var subAsset in subAssets)
            {
                var found = false;
                foreach (var keyValue in obj.Data)
                {
                    if (subAsset.QualifiedKey.Equals(keyValue.QualifiedKey))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    needsSaving = true;
                    AssetDatabase.RemoveObjectFromAsset(subAsset);
                }
            }

            // Add all keyvalues that aren't subassets
            foreach (var keyValue in obj.Data)
            {
                var found = false;
                foreach (var subAsset in subAssets)
                {
                    if (subAsset.QualifiedKey.Equals(keyValue.QualifiedKey))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    continue;
                }

                needsSaving = true;
                var keyValueAsset = CreateInstance<IniKeyValueAsset>();
                keyValueAsset.QualifiedKey = new IniQualifiedKey(keyValue.QualifiedKey.Section, keyValue.QualifiedKey.Key);
                keyValueAsset.IniAsset = obj;
                var subassetName = keyValueAsset.QualifiedKey.ToString();
                keyValueAsset.name = subassetName;
                AssetDatabase.AddObjectToAsset(keyValueAsset, obj);
            }

            if (!needsSaving)
            {
                return;
            }

            EditorUtility.SetDirty(obj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion
    }
}