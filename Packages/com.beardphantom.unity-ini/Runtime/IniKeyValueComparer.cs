using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityINI
{
    public class IniKeyValueComparer : IComparer<SerializedKeyValuePair<string, string>>
    {
        #region Fields

        public static readonly IniKeyValueComparer Instance = new();

        #endregion

        #region Methods

        public int Compare(SerializedKeyValuePair<string, string> x, SerializedKeyValuePair<string, string> y)
        {
            return string.Compare(x.Key, y.Key, StringComparison.Ordinal);
        }

        #endregion
    }
}