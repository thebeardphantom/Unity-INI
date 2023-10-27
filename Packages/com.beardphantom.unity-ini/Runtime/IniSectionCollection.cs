using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityINI
{
    [Serializable]
    public class IniSectionCollection : IEnumerable<IniSection>
    {
        #region Properties

        [field: SerializeField]
        private SerializedDictionary<string, IniSection> Sections { get; set; } = new(IniSectionCollectionComparer.Instance);

        #endregion

        #region Constructors

        public IniSectionCollection(SectionDataCollection iniDataSections)
        {
            foreach (var iniDataSection in iniDataSections)
            {
                Sections.Add(iniDataSection.SectionName, new IniSection(iniDataSection));
            }
        }

        #endregion

        #region Methods

        public IniSection this[string sectionName] => Sections[sectionName];

        /// <inheritdoc />
        public IEnumerator<IniSection> GetEnumerator()
        {
            return Sections.Values.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}