using IniParser.Model;

namespace BeardPhantom.UnityINI
{
    public interface IIniAsset
    {
        #region Properties

        IniData Data { get; }

        #endregion
    }
}