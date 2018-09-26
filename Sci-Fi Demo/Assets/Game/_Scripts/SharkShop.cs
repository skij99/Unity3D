using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private AudioSource _winSound;

    //[SerializeField]
    //private GameObject _weapon;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            _winSound = GetComponent<AudioSource>();
            if (player != null && Input.GetKeyDown(KeyCode.E))
            {
                if (player.hasCoin)
                {
                    player.hasCoin = false;
                    player.EnableWeapons();
                    _winSound.Play();

                    //_weapon.SetActive(true);
                }
                else
                {
                    Debug.Log("You can't buy the weapon now");
                }
            }
        }
    }
}
