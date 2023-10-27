using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public class IniKeyValueCollection : IEnumerable<KeyValuePair<string, string>>
    {
        #region Properties

        [field: SerializeField]
        private SerializedDictionary<string, string> KeysToValues { get; set; } = new(IniKeyValueComparer.Instance);

        #endregion

        #region Constructors

        public IniKeyValueCollection(KeyDataCollection keyDataCollection)
        {
            foreach (var keyData in keyDataCollection)
            {
                KeysToValues.Add(keyData.KeyName, keyData.Value);
            }
        }

        #endregion

        #region Methods

        public string this[string key] => KeysToValues[key];

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return KeysToValues.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}