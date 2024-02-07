using System;

namespace BeardPhantom.UnityINI.CVars
{
    public partial class CVar
    {
        #region Methods

        public static CVar CreateEnum<T>(in string id, in T enumValue = default) where T : Enum
        {
            var cvar = Create(id);
            cvar.SetEnum(enumValue);
            return cvar;
        }

        public void SetEnum<T>(T value, in CVarSetFlags setFlags = DEFAULT_SET_FLAGS) where T : Enum
        {
            var strValue = value.ToString();
            var intValue = (int)(object)value;
            SetValues(strValue, intValue, CVarValueType.String, setFlags);
        }

        public bool TryToEnum<T>(out T value) where T : Enum
        {
            var success = Enum.TryParse(typeof(T), String, out var rawValue);
            value = success ? (T)rawValue : default;
            return success;
        }

        public T ToEnum<T>() where T : Enum
        {
            var success = Enum.TryParse(typeof(T), String, out var rawValue);
            var result = success ? (T)rawValue : default;
            return result;
        }

        #endregion
    }
}