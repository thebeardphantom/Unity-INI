namespace BeardPhantom.UnityINI
{
    public readonly struct IniQualifiedKeyValue
    {
        #region Fields

        public readonly string Section;

        public readonly string Key;

        public readonly string Value;

        #endregion

        #region Constructors

        public IniQualifiedKeyValue(string section, string key, string value)
        {
            Section = section;
            Key = key;
            Value = value;
        }

        #endregion
    }
}