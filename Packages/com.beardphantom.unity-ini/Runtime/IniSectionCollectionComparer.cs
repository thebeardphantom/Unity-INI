using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityINI
{
    internal class IniSectionCollectionComparer : IComparer<SerializedKeyValuePair<string, IniSection>>
    {
        #region Fields

        public static IniSectionCollectionComparer Instance = new();

        #endregion

        #region Methods

        public int Compare(SerializedKeyValuePair<string, IniSection> x, SerializedKeyValuePair<string, IniSection> y)
        {
            return string.Compare(x.Key, y.Key, StringComparison.Ordinal);
        }

        #endregion
    }
}