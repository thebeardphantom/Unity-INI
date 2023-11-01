using System;

namespace BeardPhantom.CVars
{
    [Flags]
    public enum CVarSetFlags
    {
        DontNotify = 1 << 0,
        BypassChangeCheck = 1 << 1
    }

    public static class CVarSetFlagsExtensions
    {
        #region Methods

        public static bool HasFlagFast(this CVarSetFlags value, CVarSetFlags flag)
        {
            return (value & flag) != 0;
        }

        #endregion
    }
}