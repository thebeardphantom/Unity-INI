using BeardPhantom.UnityINI;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region Properties

    [field: SerializeField]
    private IniAssetBase IniAsset { get; set; }

    [field: SerializeField]
    private IniKeyValueAsset KeyValueAsset { get; set; }

    #endregion

    #region Methods

    private void Start()
    {
        Debug.Log(KeyValueAsset.Value);
    }

    #endregion
}