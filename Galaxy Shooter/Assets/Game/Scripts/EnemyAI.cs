using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;
    private UIManager _uiManager;

	// Use this for initialization
	void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            _speed += 0.5f;
            transform.position = new Vector3(randomX, 6.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1.0f);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1.0f);
            Destroy(this.gameObject);
        }
    }
}
