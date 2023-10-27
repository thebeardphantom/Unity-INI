using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        #region Fields

        public const string SERIALIZED_KEY_VALUE_PAIRS_PROPERTY_NAME = nameof(SerializedKeyValuePairs);

        private readonly IComparer<SerializedKeyValuePair<TKey, TValue>> _keyValueComparer;

        #endregion

        #region Properties

        [field: SerializeField]
        private List<SerializedKeyValuePair<TKey, TValue>> SerializedKeyValuePairs { get; set; } = new();

        #endregion

        #region Constructors

        public SerializedDictionary() : this(Comparer<SerializedKeyValuePair<TKey, TValue>>.Default) { }

        public SerializedDictionary(IComparer<SerializedKeyValuePair<TKey, TValue>> keyValueComparer)
        {
            _keyValueComparer = keyValueComparer;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            SerializedKeyValuePairs.Clear();
            SerializedKeyValuePairs.ToSerialized(this);
            SerializedKeyValuePairs.Sort(_keyValueComparer);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            this.FromSerialized(SerializedKeyValuePairs);
            SerializedKeyValuePairs.Clear();
        }

        #endregion
    }
}