using IniParser.Model.Configuration;
using System;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public struct SerializedIniParserConfig
    {
        #region Properties

        public static SerializedIniParserConfig Default => new()
        {
            AllowDuplicateKeys = true,
            DuplicateKeysUseLastValue = true
        };

        [field: SerializeField]
        public bool AllowDuplicateKeys { get; set; }

        [field: SerializeField]
        public bool DuplicateKeysUseLastValue { get; set; }

        #endregion

        #region Methods

        public static implicit operator IniParserConfiguration(SerializedIniParserConfig serializedIniParserConfig)
        {
            return new IniParserConfiguration
            {
                AllowDuplicateKeys = serializedIniParserConfig.AllowDuplicateKeys,
                OverrideDuplicateKeys = serializedIniParserConfig.DuplicateKeysUseLastValue
            };
        }

        #endregion
    }
}