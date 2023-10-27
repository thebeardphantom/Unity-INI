﻿using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public class IniSection : IEnumerable<IniQualifiedKeyValue>
    {
        #region Properties

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public IniKeyValueCollection KeyValueCollection { get; private set; }

        #endregion

        #region Constructors

        public IniSection(string name, KeyDataCollection keyDataCollection)
        {
            Name = name;
            KeyValueCollection = new IniKeyValueCollection(keyDataCollection);
        }

        public IniSection(SectionData iniDataSection) : this(iniDataSection.SectionName, iniDataSection.Keys) { }

        #endregion

        #region Methods

        public string this[string key] => KeyValueCollection[key];

        public static bool IsGlobalSectionName(string sectionName)
        {
            return string.IsNullOrWhiteSpace(sectionName);
        }

        /// <inheritdoc />
        public IEnumerator<IniQualifiedKeyValue> GetEnumerator()
        {
            foreach (var kvp in KeyValueCollection)
            {
                yield return new IniQualifiedKeyValue(new IniQualifiedKey(Name, kvp.Key), kvp.Value);
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