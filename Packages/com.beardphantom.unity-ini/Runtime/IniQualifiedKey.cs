using System;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public struct IniQualifiedKey
    {
        #region Fields

        public const string KEY_SECTION_DEFAULT_SEPARATOR = "/";

        #endregion

        #region Properties

        [field: SerializeField]
        public string Section { get; private set; }

        [field: SerializeField]
        public string Key { get; private set; }

        #endregion

        #region Constructors

        public IniQualifiedKey(string section, string key)
        {
            Section = section;
            Key = key;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public readonly override string ToString()
        {
            return ToString(KEY_SECTION_DEFAULT_SEPARATOR);
        }

        public readonly string ToString(string separator)
        {
            return string.IsNullOrWhiteSpace(Section) ? Key : $"{Section}{separator}{Key}";
        }

        #endregion
    }
}