using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private GameObject _shieldPrefab;

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = speed boost, 2 = shield

    [SerializeField]
    private AudioClip _clip;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotPowerupOn();
                        break;
                    case 1:
                        player.SpeedPowerUpOn();
                        break;
                    case 2:
                        player.EnableShield();
                        break;
                    default:
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1.0f);
            Destroy(this.gameObject);
        }
    }
}
