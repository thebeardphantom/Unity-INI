using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public struct SerializedKeyValuePair<TKey, TValue> : IComparable<SerializedKeyValuePair<TKey, TValue>>
    {
        #region Properties

        [field: SerializeField]
        public TKey Key { get; private set; }

        [field: SerializeField]
        public TValue Value { get; private set; }

        #endregion

        #region Constructors

        public SerializedKeyValuePair(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Key = keyValuePair.Key;
            Value = keyValuePair.Value;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public int CompareTo(SerializedKeyValuePair<TKey, TValue> other)
        {
            return 0;
        }

        #endregion
    }
}