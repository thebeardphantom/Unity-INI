namespace BeardPhantom.UnityINI.CVars
{
    public partial class CVar
    {
        #region Fields

        public const string BOOL_TRUE_STRING = "true";

        public const string BOOL_FALSE_STRING = "false";

        public const string BOOL_TRUE_STRING_SHORT = "1";

        public const string BOOL_FALSE_STRING_SHORT = "0";

        #endregion

        #region Properties

        public static bool ForceShortBoolStrings { get; set; } = true;

        public bool Bool
        {
            get
            {
                var floatValue = CVarRegistry.Instance.GetValue<float>(this);
                return floatValue != 0f;
            }
            set => SetBool(value);
        }

        #endregion

        #region Methods

        public static CVar CreateBool(in string id, in bool value)
        {
            var cvar = Create(id);
            cvar.Bool = value;
            return cvar;
        }

        private static string GetBoolString(in bool value)
        {
            if (ForceShortBoolStrings)
            {
                return value ? BOOL_TRUE_STRING_SHORT : BOOL_FALSE_STRING_SHORT;
            }

            return value ? BOOL_TRUE_STRING : BOOL_FALSE_STRING;
        }

        public void SetBool(in bool value, in CVarSetFlags setFlags = DEFAULT_SET_FLAGS)
        {
            SetValues(GetBoolString(value), value ? 1 : 0, CVarValueType.Bool, setFlags);
        }

        #endregion
    }
}