using System.Globalization;

namespace BeardPhantom.UnityINI.CVars
{
    public partial class CVar
    {
        #region Properties

        public float Float
        {
            get => CVarRegistry.Instance.GetValue<float>(this);
            set => SetFloat(value);
        }

        #endregion

        #region Methods

        public static CVar CreateFloat(in string id, in float floatValue)
        {
            var cvar = Create(id);
            cvar.Float = floatValue;
            return cvar;
        }

        public void SetFloat(in float value, in CVarSetFlags setFlags = DEFAULT_SET_FLAGS)
        {
            SetValues(value.ToString(CultureInfo.InvariantCulture), value, CVarValueType.Float, setFlags);
        }

        #endregion
    }
}