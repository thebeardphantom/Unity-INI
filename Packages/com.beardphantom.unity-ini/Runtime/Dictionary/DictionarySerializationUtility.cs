using System.Collections.Generic;

namespace BeardPhantom.UnityINI
{
    public static class DictionarySerializationUtility
    {
        #region Methods

        public static void ToSerialized<TKey, TValue>(
            this ICollection<SerializedKeyValuePair<TKey, TValue>> collection,
            IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                collection.Add(new SerializedKeyValuePair<TKey, TValue>(keyValuePair));
            }
        }

        public static bool FromSerialized<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            IEnumerable<SerializedKeyValuePair<TKey, TValue>> serializedKeyValuePairs)
        {
            var addedAll = true;
            foreach (var serializedKeyValuePair in serializedKeyValuePairs)
            {
                var didAdd = dictionary.TryAdd(serializedKeyValuePair.Key, serializedKeyValuePair.Value);
                if (!didAdd)
                {
                    addedAll = false;
                }
            }

            return addedAll;
        }

        #endregion
    }
}