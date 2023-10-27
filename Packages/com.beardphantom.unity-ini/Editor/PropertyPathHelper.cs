using System.Text;

namespace BeardPhantom.UnityINI.Editor
{
    public static class PropertyPathHelper
    {
        #region Fields

        private static readonly StringBuilder _sb = new();

        #endregion

        #region Methods

        public static string BuildPath(params string[] parts)
        {
            _sb.Clear();
            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                _sb.Append('<');
                _sb.Append(part);
                _sb.Append(">k__BackingField");
                if (i < parts.Length - 1)
                {
                    _sb.Append('.');
                }
            }

            return _sb.ToString();
        }

        #endregion
    }
}