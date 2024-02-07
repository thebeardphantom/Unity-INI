namespace BeardPhantom.UnityINI.CVars
{
    public partial class CVar
    {
        #region Properties

        public string String
        {
            get => CVarRegistry.Instance.GetValue<string>(this);
            set => SetString(value);
        }

        #endregion

        #region Methods

        public static CVar CreateString(in string id, in string stringValue = default)
        {
            var cvar = Create(id);
            cvar.String = stringValue;
            return cvar;
        }

        public void SetString(string value, in CVarSetFlags setFlags = DEFAULT_SET_FLAGS)
        {
            float floatValue;
            if (bool.TryParse(value, out var boolValue))
            {
                floatValue = boolValue ? 1 : 0;
                value = GetBoolString(boolValue);
            }
            else
            {
                float.TryParse(value, out floatValue);
            }

            SetValues(value, floatValue, CVarValueType.String, setFlags);
        }

        #endregion
    }
}