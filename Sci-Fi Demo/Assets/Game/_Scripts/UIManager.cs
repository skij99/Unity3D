using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _coinImage;

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void UpdateInventory(bool hasCoin)
    {
        _coinImage.SetActive(hasCoin);
    }
//    public void CollectedCoin()
//    {
//        _coinImage.SetActive(true);
//    }

//    public void RemoveCoin()
//    {
//        _coinImage.SetActive(false);
//    }
}
