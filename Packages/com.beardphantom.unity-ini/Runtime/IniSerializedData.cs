using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public class IniSerializedData : IEnumerable<IniQualifiedKeyValue>
    {
        #region Properties

        [field: SerializeField]
        public IniSection Global { get; private set; }

        [field: SerializeField]
        public IniSectionCollection Sections { get; private set; }

        #endregion

        #region Constructors

        public IniSerializedData(IniData parsedData)
        {
            Global = new IniSection(null, parsedData.Global);
            Sections = new IniSectionCollection(parsedData.Sections);
        }

        #endregion

        #region Methods

        public IniSection this[string sectionName] =>
            IniSection.IsGlobalSectionName(sectionName) ? Global : Sections[sectionName];

        public IniQualifiedKeyValue this[in IniQualifiedKey qualifiedKey]
        {
            get
            {
                var value = this[qualifiedKey.Section][qualifiedKey.Key];
                return new IniQualifiedKeyValue(new IniQualifiedKey(qualifiedKey.Section, qualifiedKey.Key), value);
            }
        }

        /// <inheritdoc />
        public IEnumerator<IniQualifiedKeyValue> GetEnumerator()
        {
            foreach (var keyValue in Global)
            {
                yield return keyValue;
            }

            foreach (var section in Sections)
            {
                foreach (var keyValue in section)
                {
                    yield return keyValue;
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}