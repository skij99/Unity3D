using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool shieldActive = false;

    public int lives = 3;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _fireRate = 0.2f;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int _hitCount = 0;
    
    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnCoroutine();
        }

        _audioSource = GetComponent<AudioSource>();
        _hitCount = 0;
    }

    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.81f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isSpeedBoostActive)
        {
            _speed = 15.0f;
        }
        else
        {
            _speed = 5.0f;
        }

        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        //if player's y position is greater than 0, set y to 0
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //if player's x position is greater than 8.25 or less than -8.25, set x to 8.25 or -8.25
        if (transform.position.x > 8.25f)
        {
            transform.position = new Vector3(8.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.25f)
        {
            transform.position = new Vector3(-8.25f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (shieldActive)
        {
            shieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        _hitCount++;

        if (_hitCount == 1)
        {
            _engines[Random.Range(0,2)].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _engines[0].SetActive(true);
            _engines[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);


        if (lives <= 0)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if (_uiManager != null)
            {
                _uiManager.ShowTitle();
            }
            if (_gameManager != null)
            {
                _gameManager.gameOver = true;
            }
            Destroy(this.gameObject);
        }
    }

    public void EnableShield()
    {
        shieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void SpeedPowerUpOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
}
