using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPickupSound;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null && Input.GetKeyDown(KeyCode.E))
            {
                player.hasCoin = true;
                AudioSource.PlayClipAtPoint(_coinPickupSound, transform.position, 1.0f);
                Destroy(this.gameObject);
            }
        }
    }
}
