using System.Collections.Generic;

namespace BeardPhantom.CVars
{
    internal class CVarValueLookup<T>
    {
        #region Fields

        private readonly Dictionary<CVar, T> _cvarToValues = new();

        #endregion

        #region Methods

        public T this[CVar cvar]
        {
            get => _cvarToValues[cvar];
            set => _cvarToValues[cvar] = value;
        }

        public bool TryGetValue(CVar cvar, out T value)
        {
            return _cvarToValues.TryGetValue(cvar, out value);
        }

        public void Remove(CVar cvar)
        {
            _cvarToValues.Remove(cvar);
        }

        public void Clear()
        {
            _cvarToValues.Clear();
        }

        #endregion
    }
}