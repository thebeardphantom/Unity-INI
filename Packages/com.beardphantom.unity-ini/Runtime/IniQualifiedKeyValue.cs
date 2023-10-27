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
    }
}