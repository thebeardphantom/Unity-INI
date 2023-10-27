using UnityEngine;

namespace BeardPhantom.UnityINI
{
    public class IniKeyValueAsset : ScriptableObject
    {
        #region Properties

        [field: SerializeField]
        public IniAsset IniAsset { get; set; }

        [field: SerializeField]
        public IniQualifiedKey QualifiedKey { get; set; }

        public IniQualifiedKeyValue QualifiedKeyValue => IniAsset.Data[QualifiedKey];

        public string Value => QualifiedKeyValue.Value;

        #endregion
    }
}