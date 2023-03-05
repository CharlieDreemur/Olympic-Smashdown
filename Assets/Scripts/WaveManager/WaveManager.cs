using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Data")]
    [Tooltip("The wave data to use for this wave")]
    [SerializeField] WaveData _waveData;

    [Header("Object References")]
    [Tooltip("Used for locating the player's position")]
    [SerializeField] GameObject _player;


    [Header("Prefabs")]
    [Tooltip("The prefab instantiated on enemy spawn locations")]
    [SerializeField] GameObject _warningPrefab;
    [SerializeField] private float _spawnDelay;


    private float _spawnCooldown;
    private float _timeLeft;

    private void Start()
    {
        // For testing purposes
        OnStartingWave(); // TODO: Remove this line
    }

    public void OnStartingWave()
    {
        StartWave();
    }

    private void StartWave()
    {
        Debug.Log("Wave started");
        _spawnCooldown = _waveData.SpawnInterval;
        _timeLeft = _waveData.Duration;

        StartCoroutine(SpawnEnemies());
    }

    private void EndWave()
    {
        Debug.Log("Wave ended");
    }

    private IEnumerator SpawnEnemies()
    {
        while (_timeLeft > 0)
        {
            if (_spawnCooldown <= 0)
            {
                SpawnEnemies(_waveData.GetRandomSpawnInfo());
                _spawnCooldown = _waveData.SpawnInterval;
            }

            _spawnCooldown -= Time.deltaTime;
            _timeLeft -= Time.deltaTime;

            yield return null;
        }
        EndWave();
    }


    private void SpawnEnemies(SpawnInfo spawnInfo)
    {
        Vector3 playerPosition = _player.transform.position;
        if (spawnInfo.IsGrouped)
        {
            Vector2 groupCenterPosition = new Vector2(playerPosition.x, playerPosition.y) + Random.insideUnitCircle * 10;
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Vector2 randomPosition = groupCenterPosition + Random.insideUnitCircle * 2;
                StartCoroutine(SpawnEnemy(randomPosition));
            }
        }
        else
        {
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 randomPosition = new Vector2(playerPosition.x, playerPosition.y) + randomDirection * 6;
                StartCoroutine(SpawnEnemy(randomPosition));
            }
        }
    }

    IEnumerator SpawnEnemy(Vector3 position)
    {
        GameObject warning = Instantiate(_warningPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(_spawnDelay);
        Destroy(warning);
        Instantiate(_waveData.GetRandomSpawnInfo().EnemyPrefab, position, Quaternion.identity);
    }
}
