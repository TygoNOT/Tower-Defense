using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _enemiesPrefabs;

    [Header("Attributes")]
    [SerializeField, Range(1, 15)] private int _baseEnemies = 8;
    [SerializeField, Range(0.5f, 5f)] private float _enemiesPerSecond = 0.5f;
    [SerializeField, Range(1, 10)] private int _timeBetweenWaves = 5;
    [SerializeField, Range(0.01f, 5f)] private float _difficulityFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy;

    private GameObject _enemyToSpawn;

    [SerializeField] private int _waveIndex, _enemiesAlive, _enemiesLeftToSpawn;
    [SerializeField] private float _timeSinceLastSpawn;
    [SerializeField] private bool _isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy = new UnityEvent();
        onEnemyDestroy.AddListener(EnemyKilled);
    }

    private void Start()
    {
        _waveIndex = 1;
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!_isSpawning)
            return;

        _timeSinceLastSpawn += Time.deltaTime;
        if (_timeSinceLastSpawn > (1 / _enemiesPerSecond) && _enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            _enemiesLeftToSpawn--;
            _enemiesAlive++;
            _timeSinceLastSpawn = 0;
        }

        if (_enemiesAlive == 0 && _enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        _isSpawning = false;
        _waveIndex++;
        _timeSinceLastSpawn = 0;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        _enemyToSpawn = _enemiesPrefabs[0];
        Instantiate(_enemyToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_waveIndex, _difficulityFactor));
    }

    private void EnemyKilled()
    {
        _enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
    }
}
