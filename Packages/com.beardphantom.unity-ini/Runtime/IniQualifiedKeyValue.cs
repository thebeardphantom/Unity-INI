namespace BeardPhantom.UnityINI
{
    public readonly struct IniQualifiedKeyValue
    {
        #region Fields

        public readonly IniQualifiedKey QualifiedKey;

        public readonly string Value;

        #endregion

        #region Constructors

        public IniQualifiedKeyValue(IniQualifiedKey qualifiedKey, string value)
        {
            QualifiedKey = qualifiedKey;
            Value = value;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString(IniQualifiedKey.KEY_SECTION_DEFAULT_SEPARATOR);
        }

        public string ToString(string separator)
        {
            return QualifiedKey.ToString(separator);
        }

        #endregion
    }
}