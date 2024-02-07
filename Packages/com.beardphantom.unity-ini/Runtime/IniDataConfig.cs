using IniParser.Model.Configuration;
using System;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public struct IniDataConfig : IEquatable<IniDataConfig>
    {
        #region Fields

        public const string COMMENT_STRING_DEFAULT = ";";

        #endregion

        #region Properties

        public static IniDataConfig Default => new()
        {
            DuplicateKeysUseLastValue = true,
            CommentString = COMMENT_STRING_DEFAULT
        };

        [field: SerializeField]
        public bool DuplicateKeysUseLastValue { get; set; }

        [field: Delayed]
        [field: SerializeField]
        public string CommentString { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is IniDataConfig other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(DuplicateKeysUseLastValue, CommentString);
        }

        /// <inheritdoc />
        bool IEquatable<IniDataConfig>.Equals(IniDataConfig other)
        {
            return DuplicateKeysUseLastValue == other.DuplicateKeysUseLastValue
                && CommentString == other.CommentString;
        }

        public static implicit operator IniParserConfiguration(IniDataConfig iniDataConfig)
        {
            return new IniParserConfiguration
            {
                AllowDuplicateKeys = true,
                AllowDuplicateSections = true,
                OverrideDuplicateKeys = iniDataConfig.DuplicateKeysUseLastValue,
                CommentString = iniDataConfig.CommentString
            };
        }

        #endregion
    }
}