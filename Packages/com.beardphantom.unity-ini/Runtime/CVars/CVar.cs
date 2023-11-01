using System;
using UnityEngine;

namespace BeardPhantom.CVars
{
    public partial class CVar : IEquatable<CVar>
    {
        #region Types

        public delegate void OnCvarValueChanged(in CVarValueChangedArgs args);

        #endregion

        #region Events

        public event OnCvarValueChanged ValueChanged;

        #endregion

        #region Fields

        public const CVarSetFlags DEFAULT_SET_FLAGS = 0;

        internal const string STRING_NULL_FALLBACK = "NULL";

        public readonly string Id;

        public readonly PropertyName IdPropertyName;

        private bool _registeredDefaults;

        #endregion

        #region Properties

        public CVarValueType LastSetValueType { get; private set; }

        #endregion

        #region Constructors

        private CVar(string id, PropertyName idPropertyName)
        {
            Id = id;
            IdPropertyName = idPropertyName;
        }

        #endregion

        #region Methods

        public static CVar Create(string id)
        {
            var idPropertyName = new PropertyName(id);
            if (!CVarRegistry.Instance.TryFind(idPropertyName, out var cvar))
            {
                cvar = new CVar(id, idPropertyName);
                CVarRegistry.Instance.Register(cvar);
            }

            return cvar;
        }

        public void ResetToDefault()
        {
            var defaults = CVarRegistry.Instance.GetValue<CVarValuesState>(this);
            SetValues(defaults.String, defaults.Float, CVarValueType.String, DEFAULT_SET_FLAGS);
        }

        public string GetShortValueString()
        {
            return LastSetValueType == CVarValueType.Bool
                ? GetBoolString(Bool)
                : String ?? STRING_NULL_FALLBACK;
        }

        public string GetDebugPrintString()
        {
            return $"{Id} = \"{GetShortValueString()}\"";
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Id;
        }

        /// <inheritdoc />
        public bool Equals(CVar other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || IdPropertyName.Equals(other.IdPropertyName);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((CVar)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return IdPropertyName.GetHashCode();
        }

        public void ClearEventListeners()
        {
            ValueChanged = null;
        }

        private void SetValues(
            in string stringValNew,
            in float floatValNew,
            in CVarValueType invokingType,
            in CVarSetFlags setFlags)
        {
            var valuesStateOld = CVarValuesState.CreateFromCVar(this);
            var valuesStateNew = valuesStateOld.SetValues(stringValNew, floatValNew, out var changed);
            if (!changed && !setFlags.HasFlagFast(CVarSetFlags.BypassChangeCheck))
            {
                return;
            }

            if (!_registeredDefaults)
            {
                _registeredDefaults = true;
                CVarRegistry.Instance.SetValue(this, valuesStateNew);
            }

            CVarRegistry.Instance.SetState(this, valuesStateNew);
            LastSetValueType = invokingType;

            if (setFlags.HasFlagFast(CVarSetFlags.DontNotify))
            {
                return;
            }

            var args = new CVarValueChangedArgs(this, valuesStateOld, valuesStateNew, invokingType);
            ValueChanged?.Invoke(args);
        }

        #endregion
    }
}