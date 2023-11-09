using UnityEngine;

namespace BeardPhantom.UnityINI.CVars
{
    /// <summary>
    /// Stores the full value state of a cvar.
    /// </summary>
    public readonly struct CVarValuesState
    {
        #region Fields

        public readonly string String;

        public readonly float Float;

        #endregion

        #region Properties

        public int Int => (int)Float;

        public bool Bool => Int != 0;

        #endregion

        #region Constructors

        private CVarValuesState(in string stringVal, in float floatVal)
        {
            String = stringVal;
            Float = floatVal;
        }

        #endregion

        #region Methods

        internal static CVarValuesState CreateFromCVar(CVar cvar)
        {
            return new CVarValuesState(cvar.String, cvar.Float);
        }

        internal CVarValuesState SetValues(
            in string stringValNew,
            in float floatValNew,
            out bool changed)
        {
            var stringVal = String;
            var floatVal = Float;
            changed = false;

            if (stringVal != stringValNew)
            {
                changed = true;
                stringVal = stringValNew;
            }

            if (!Mathf.Approximately(floatVal, floatValNew))
            {
                changed = true;
                floatVal = floatValNew;
            }

            return changed ? new CVarValuesState(stringVal, floatVal) : this;
        }

        #endregion
    }
}