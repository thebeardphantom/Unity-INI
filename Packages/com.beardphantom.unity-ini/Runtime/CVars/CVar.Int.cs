namespace BeardPhantom.CVars
{
    public partial class CVar
    {
        #region Properties

        public int Int
        {
            get => (int)CVarRegistry.Instance.GetValue<float>(this);
            set => SetInt(value);
        }

        #endregion

        #region Methods

        public static CVar CreateInt(in string id, in int intValue)
        {
            var cvar = Create(id);
            cvar.Int = intValue;
            return cvar;
        }

        public void SetInt(in int value, in CVarSetFlags setFlags = DEFAULT_SET_FLAGS)
        {
            SetValues(value.ToString(), value, CVarValueType.Int, setFlags);
        }

        #endregion
    }
}