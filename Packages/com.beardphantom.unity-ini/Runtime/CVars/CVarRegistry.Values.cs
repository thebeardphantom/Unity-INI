using System;
using UnityEngine;

namespace BeardPhantom.CVars
{
    /*
     * Manages current values for all cvars. CVars system is optimized for retrieving values of a single type,
     * so struct-of-arrays pattern is used for memory access optimization.
     */
    public partial class CVarRegistry
    {
        #region Fields

        private readonly CVarValueLookup<string> _strings = new();

        private readonly CVarValueLookup<float> _floats = new();

        private readonly CVarValueLookup<CVarValuesState> _defaults = new();

        #endregion

        #region Methods

        private static void OnCvarValueChanged(in CVarValueChangedArgs args)
        {
            var stringValueOld = args.ValuesStateOld.String ?? CVar.STRING_NULL_FALLBACK;
            var stringValueNew = args.ValuesStateNew.String ?? CVar.STRING_NULL_FALLBACK;
            Debug.Log($"{args.Cvar} value changed from \"{stringValueOld}\" to \"{stringValueNew}\".");
        }

        internal T GetValue<T>(CVar cvar)
        {
            var lookup = GetLookupForType<T>();
            return lookup[cvar];
        }

        internal void SetValue<T>(CVar cvar, T value)
        {
            var lookup = GetLookupForType<T>();
            lookup[cvar] = value;
        }

        internal void SetState(CVar cvar, CVarValuesState state)
        {
            _strings[cvar] = state.String;
            _floats[cvar] = state.Float;
        }

        private CVarValueLookup<T> GetLookupForType<T>()
        {
            var type = typeof(T);
            if (type == typeof(string))
            {
                return _strings as CVarValueLookup<T>;
            }

            if (type == typeof(float))
            {
                return _floats as CVarValueLookup<T>;
            }

            if (type == typeof(CVarValuesState))
            {
                return _defaults as CVarValueLookup<T>;
            }

            throw new Exception($"Invalid type for lookup: {type}");
        }

        #endregion
    }
}