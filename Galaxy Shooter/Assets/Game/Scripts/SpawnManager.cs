using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

	// Use this for initialization
	void Start ()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void StartSpawnCoroutine()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerups = Random.Range(0, 3);
            float randomX = Random.Range(-8.5f, 8.5f);
            Instantiate(_powerups[randomPowerups], new Vector3(randomX, 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(6.0f);
        }
    }

    public IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(4.0f);
        }
    }
}
