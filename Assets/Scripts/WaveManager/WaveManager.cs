using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
// using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("The wave data to use for this wave")]
    [SerializeField] List<WaveData> _waveData;
    [SerializeField] private ItemPoolData itemPoolData;
    [Space(10)]

    [Header("Prefabs")]
    [AssetsOnly]
    [Required("Please add a reference to the warning prefab")]
    [SerializeField] GameObject _warningPrefab;
    [Required("Please add a reference to the elite warning prefab")]
    [AssetsOnly]
    [SerializeField] GameObject _eliteWarningPrefab;
    [Space(10)]

    [Header("Spawn Settings")]
    [Tooltip("The time it takes for the warning prefab to fade out and spawn the enemy")]
    [Range(0f, 3f)]
    [SerializeField] private float _spawnDelay;

    [Space(10)]

    [Header("TransitionCurtain")]
    [Tooltip("The thing that transitions to the next level")]
    [SerializeField]
    private TransitionBlackout curtain;

    [Header("Next Level")]
    [Tooltip("String pointing to the next level")]
    [SerializeField]
    public UnityEvent<GameObject> eliteEnemySpawned;
    public UnityEvent<int> WaveStarted;

    private string nextLevel = "";
    public ItemPoolData ItemPoolData { get; private set; }
    private GameObject _player;
    private List<WaveData>.Enumerator _currentWaveEnumerator;
    private List<Enemy> _enemies = new List<Enemy>(); // TODO: Replace with Enemy script
    private List<GameObject> _warningObjects = new List<GameObject>(); // Keep track of all the warning objects so we can destroy them when the wave ends
    private WaveData _currentWaveData;
    private int _currentWaveIndex;
    public int CurrentWaveIndex => _currentWaveIndex;
    private float _spawnCooldown = 0f;
    private float _currentWaveTime = 0f;
    private bool _eliteMobsSpawned = false;
    private uint _eliteMobsAlive = 0;
    private uint _totalMobsAlive = 0;
    private void Start()
    {
        ItemPoolData = itemPoolData; // Make this data accessible for ItemSpawner scripts
        _player = Player.Instance.gameObject;
        if (_player == null)
        {
            Debug.LogError("No player found in scene, disabling WaveManager. Please make sure there is a GameObject tagged as \"Player\" in the scene");
            enabled = false;
            return;
        }
        // For testing purposes
        StartStage(); // TODO: Remove this
    }

    public void StartStage()
    {
        StartNextWave();
    }

    private void EndStage()
    {
        if (nextLevel == "")
        {
            StartCoroutine(curtain.LoadAsyncSceneWithFadeOut("Scenes/WinScene"));
        }
        else
        {
            StartCoroutine(curtain.LoadAsyncSceneWithFadeOut(nextLevel));
        }
    }

    private void StartNextWave()
    {
        if (_currentWaveData == null) // First wave
        {
            _currentWaveEnumerator = _waveData.GetEnumerator();
            _currentWaveIndex = 0;
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
                _currentWaveIndex++;
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
        WaveStarted?.Invoke(CurrentWaveIndex + 1);
        _spawnCooldown = 0f;
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
        List<Enemy> enemiesToKill = new List<Enemy>(_enemies);
        foreach (Enemy enemy in enemiesToKill)
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
                StartCoroutine(EnemySpawnCoroutine(randomPosition, spawnInfo.EnemyPrefab));
            }
        }
        else
        {
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 randomPosition = new Vector2(playerPosition.x, playerPosition.y) + randomDirection * 6;
                StartCoroutine(EnemySpawnCoroutine(randomPosition, spawnInfo.EnemyPrefab, true));
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
        Enemy enemyAI = enemy.GetComponent<Enemy>();
        enemyAI.died.AddListener(() => OnEnemyDied(enemyAI, isElite));
        _enemies.Add(enemyAI);
    }

    private void OnEnemyDied(Enemy enemyAI, bool isElite)
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
