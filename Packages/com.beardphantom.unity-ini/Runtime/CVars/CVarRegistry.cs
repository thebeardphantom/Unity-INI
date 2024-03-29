﻿using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BeardPhantom.UnityINI.CVars
{
    public partial class CVarRegistry : IEnumerable<CVar>
    {
        #region Fields

        private const string INDENT = "    ";

        public static readonly CVarRegistry Instance = new();

        private static readonly string _separator = $"{Environment.NewLine}{INDENT}";

        private readonly Dictionary<PropertyName, CVar> _registeredCVars = new();

        #endregion

        #region Properties

        public static bool IsPlaying
        {
            get
            {
#if UNITY_EDITOR
                return _isPlayingInEditor;
#else
                return true;
#endif
            }
        }

        #endregion

        #region Methods

        public void LoadFromIni(CompositeIniAsset iniAsset, bool log = true)
        {
            void LoadIniSection(KeyDataCollection section, string sectionName)
            {
                var hasSectionName = !string.IsNullOrWhiteSpace(sectionName);
                foreach (var keyData in section)
                {
                    var name = hasSectionName ? $"{sectionName}.{keyData.KeyName}" : keyData.KeyName;
                    var cvar = CVar.Create(name);
                    cvar.ValueChanged += OnCvarValueChanged;
                    cvar.String = keyData.Value;
                }
            }

            LoadIniSection(iniAsset.Data.Global, null);

            foreach (var section in iniAsset.Data.Sections)
            {
                LoadIniSection(section.Keys, section.SectionName);
            }

            if (log)
            {
                LogRegistrations();
            }
        }

        /// <inheritdoc />
        public IEnumerator<CVar> GetEnumerator()
        {
            return _registeredCVars.Values.GetEnumerator();
        }

        internal bool TryFind(PropertyName idPropertyName, out CVar cvar)
        {
            return _registeredCVars.TryGetValue(idPropertyName, out cvar);
        }

        internal void Register(CVar cvar)
        {
            _registeredCVars.Add(cvar.IdPropertyName, cvar);
            if (IsPlaying)
            {
                cvar.ValueChanged += OnCvarValueChanged;
            }

            _strings[cvar] = default;
            _floats[cvar] = default;
            _defaults[cvar] = default;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void LogRegistrations()
        {
            var sb = new StringBuilder($"Registered {_registeredCVars.Count} cvars:");
            sb.AppendLine();
            sb.Append(INDENT);
            sb.AppendJoin(
                _separator,
                _registeredCVars.Values.OrderBy(cvar => cvar.Id).Select(cvar => cvar.GetDebugPrintString()));
            sb.AppendLine();
            Debug.Log(sb);
        }

        #endregion
    }
}