using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Data")]
    [Tooltip("The wave data to use for this wave")]
    [SerializeField] WaveData _waveData;

    [Header("Object References")]
    [Tooltip("Used for locating the player's position")]
    [SceneObjectsOnly]
    [Required("Please add a reference to the player object")]
    [SerializeField] GameObject _player;

    [Header("Prefabs")]
    [AssetsOnly]
    [Required("Please add a reference to the warning prefab")]
    [SerializeField] GameObject _warningPrefab;
    [Required("Please add a reference to the elite warning prefab")]
    [AssetsOnly]
    [SerializeField] GameObject _eliteWarningPrefab;

    [Header("Spawn Settings")]
    [Tooltip("The time it takes for the warning prefab to fade out and spawn the enemy")]
    [Range(0f, 3f)]
    [SerializeField] private float _spawnDelay;

    private float _spawnCooldown = 0f;
    private float _currentWaveTime = 0f;
    private bool _eliteMobsSpawned = false;
    private uint _eliteMobsAlive = 0;
    private uint _totalMobsAlive = 0;
    private void Start()
    {
        // For testing purposes
        StartWave();
    }
    private void EndWave()
    {
        Debug.Log("Wave ended");
    }

    public void StartWave()
    {
        StartCoroutine(WaveCouroutine());
    }

    private IEnumerator WaveCouroutine()
    {
        Debug.Log("Wave started");
        _spawnCooldown = _waveData.SpawnInterval;
        _currentWaveTime = 0f;

        while (!IsWaveOver())
        {
            if (_totalMobsAlive >= _waveData.MaxUnitsAlive) // Wait for some units to die before spawning more
            {
                yield return null;
                continue;
            }
            // Spawn elite mobs after the wave duration has passed if they haven't already been spawned
            if (_spawnCooldown <= 0 && _currentWaveTime >= _waveData.EliteSpawnTime && !_eliteMobsSpawned)
            {
                SpawnInfo spawnInfo = _waveData.GetRandomEliteSpawnInfo();
                _eliteMobsSpawned = true;
                _eliteMobsAlive += spawnInfo.Count;
                _totalMobsAlive += spawnInfo.Count;
                SpawnEnemies(spawnInfo);
                _spawnCooldown = _waveData.SpawnInterval;
            }

            if (_spawnCooldown <= 0)
            {
                SpawnInfo spawnInfo = _waveData.GetRandomSpawnInfo();
                _totalMobsAlive += (uint)spawnInfo.Count;
                SpawnEnemies(spawnInfo);
                _spawnCooldown = _waveData.SpawnInterval;
            }

            _spawnCooldown -= Time.deltaTime;
            _currentWaveTime += Time.deltaTime;


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
                StartCoroutine(SpawnEnemy(randomPosition, spawnInfo.EnemyPrefab));
            }
        }
        else
        {
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 randomPosition = new Vector2(playerPosition.x, playerPosition.y) + randomDirection * 6;
                StartCoroutine(SpawnEnemy(randomPosition, spawnInfo.EnemyPrefab, true));
            }
        }
    }

    IEnumerator SpawnEnemy(Vector3 position, GameObject enemyPrefab, bool isElite = false)
    {
        GameObject warning;
        if (isElite)
        {
            warning = Instantiate(_eliteWarningPrefab, position, Quaternion.identity);
        }
        else
        {
            warning = Instantiate(_warningPrefab, position, Quaternion.identity);
        }
        yield return new WaitForSeconds(_spawnDelay);
        Destroy(warning);
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        // enemy.OnDeath += isElite ? OnEliteEnemyDeath : OnNormalEnemyDeath; // TODO: Add this back in when the enemy class is ready
    }

    private bool IsWaveOver()
    {
        return _eliteMobsSpawned && _eliteMobsAlive == 0;
    }
}
