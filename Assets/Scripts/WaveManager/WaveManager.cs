using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InfoBox("**NOT READY** This script does not have Enemy script integration", InfoMessageType.Warning)]
public class WaveManager : MonoBehaviour
{


    [Header("Wave Data")]
    [Tooltip("The wave data to use for this wave")]
    [SerializeField] List<WaveData> _waveData;

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


    private List<WaveData>.Enumerator _currentWaveEnumerator;
    private List<MockEnemyAI> _enemies = new List<MockEnemyAI>(); // TODO: Replace with Enemy script
    private List<GameObject> _warningObjects = new List<GameObject>(); // Keep track of all the warning objects so we can destroy them when the wave ends
    private WaveData _currentWaveData;
    private float _spawnCooldown = 0f;
    private float _currentWaveTime = 0f;
    private bool _eliteMobsSpawned = false;
    private uint _eliteMobsAlive = 0;
    private uint _totalMobsAlive = 0;
    private void Start()
    {
        // For testing purposes
        StartStage(); // TODO: Remove this
    }

    public void StartStage()
    {
        StartNextWave();
    }

    private void EndStage()
    {
        Debug.Log("Stage ended");
    }

    private void StartNextWave()
    {
        if (_currentWaveData == null) // First wave
        {
            _currentWaveEnumerator = _waveData.GetEnumerator();
            _currentWaveData = _currentWaveEnumerator.MoveNext() ? _currentWaveEnumerator.Current : null; // Get the first wave data
            if (_currentWaveData == null)
            {
                Debug.LogError("No wave data found");
                return;
            }
            StartCoroutine(WaveCoroutine());
        }
        else
        {
            if (_currentWaveEnumerator.MoveNext()) // More waves
            {
                _currentWaveData = _currentWaveEnumerator.Current;
                StartCoroutine(WaveCoroutine());
            }
            else // No more waves
            {
                _currentWaveData = null;
                EndStage();
                return;
            }
        }

    }

    private IEnumerator WaveCoroutine()
    {
        Debug.Log("Wave started");
        _spawnCooldown = _currentWaveData.SpawnInterval;
        _currentWaveTime = 0f;

        while (!IsWaveOver())
        {
            // Wait for some units to die before spawning more
            if (_totalMobsAlive >= _currentWaveData.MaxUnitsAlive)
            {
                yield return null;
                continue;
            }

            // Spawn elite mobs after time is more than eliteSpawnTime has passed if they haven't already been spawned
            if (_spawnCooldown <= 0 && _currentWaveTime >= _currentWaveData.EliteSpawnTime && !_eliteMobsSpawned)
            {
                SpawnEliteMobs();
                _spawnCooldown = _currentWaveData.SpawnInterval;
            }
            else if (_spawnCooldown <= 0) // Spawn normal mobs if the cooldown is up, and elite mobs aren't spawning in this loop iteration
            {
                SpawnNormalMobs();
                _spawnCooldown = _currentWaveData.SpawnInterval;
            }

            _spawnCooldown -= Time.deltaTime;
            _currentWaveTime += Time.deltaTime;

            yield return null;
        }
        EndWave();
    }

    private void EndWave()
    {
        StopAllCoroutines(); // Stop any running waves and enemy spawn coroutines
        CleanUpWarningSigns();
        CleanUpEnemies();
        StartNextWave(); // start the next wave in the list
    }

    private void CleanUpEnemies()
    {
        List<MockEnemyAI> enemiesToKill = new List<MockEnemyAI>(_enemies);
        foreach (MockEnemyAI enemy in enemiesToKill)
        {
            enemy.Kill();
        }
        _enemies.Clear();
    }

    private void CleanUpWarningSigns()
    {
        foreach (GameObject warningObject in _warningObjects)
        {
            Destroy(warningObject);
        }
        _warningObjects.Clear();
    }

    private void SpawnEliteMobs()
    {
        SpawnInfo spawnInfo = _currentWaveData.GetRandomEliteSpawnInfo();
        _eliteMobsSpawned = true;
        _eliteMobsAlive += spawnInfo.Count;
        _totalMobsAlive += spawnInfo.Count;
        SpawnEnemies(spawnInfo);
    }

    void SpawnNormalMobs()
    {
        SpawnInfo spawnInfo = _currentWaveData.GetRandomSpawnInfo();
        _totalMobsAlive += spawnInfo.Count;
        SpawnEnemies(spawnInfo);
    }

    private void SpawnEnemies(SpawnInfo spawnInfo)
    {
        Vector3 playerPosition = _player.transform.position;
        if (spawnInfo.IsGrouped) // Spawn enemies close by a random center point
        {
            Vector2 groupCenterPosition = new Vector2(playerPosition.x, playerPosition.y) + Random.insideUnitCircle * 10;
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Vector2 randomPosition = groupCenterPosition + Random.insideUnitCircle * 2;
                Coroutine spawnCoroutine = StartCoroutine(EnemySpawnCoroutine(randomPosition, spawnInfo.EnemyPrefab));
            }
        }
        else
        {
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 randomPosition = new Vector2(playerPosition.x, playerPosition.y) + randomDirection * 6;
                Coroutine spawnCoroutine = StartCoroutine(EnemySpawnCoroutine(randomPosition, spawnInfo.EnemyPrefab, true));
            }
        }
    }

    IEnumerator EnemySpawnCoroutine(Vector3 position, GameObject enemyPrefab, bool isElite = false)
    {
        GameObject warning; // Create a warning object to show where the enemy will spawn
        if (isElite)
        {
            warning = Instantiate(_eliteWarningPrefab, position, Quaternion.identity);
        }
        else
        {
            warning = Instantiate(_warningPrefab, position, Quaternion.identity);
        }
        _warningObjects.Add(warning); // Add the warning object to the list of warning objects so we can destroy it when the wave ends

        yield return new WaitForSeconds(_spawnDelay);

        _warningObjects.Remove(warning);
        Destroy(warning);

        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        MockEnemyAI enemyAI = enemy.GetComponent<MockEnemyAI>();
        enemyAI.died.AddListener(() => OnEnemyDied(enemyAI, isElite));
        _enemies.Add(enemyAI);
    }

    private void OnEnemyDied(MockEnemyAI enemyAI, bool isElite)
    {
        _enemies.Remove(enemyAI);
        _totalMobsAlive--;
        if (isElite)
        {
            _eliteMobsAlive--;
        }
    }


    private bool IsWaveOver()
    {
        return _eliteMobsSpawned && _eliteMobsAlive == 0;
    }
}
