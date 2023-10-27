using System;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public struct IniQualifiedKey
    {
        #region Fields

        public string Section;

        public string Key;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Section) ? Key : $"{Section}/{Key}";
        }

        #endregion
    }
}