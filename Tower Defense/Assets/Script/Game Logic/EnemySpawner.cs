using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    //Класс благодря кому враги спавнятся

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

    //В эвейке настраиваем событие на смерть врага
    private void Awake()
    {
        onEnemyDestroy = new UnityEvent();
        onEnemyDestroy.AddListener(EnemyKilled);
    }

    //Назначаем индекс волны и начинаем корутину
    private void Start()
    {
        _waveIndex = 1;
        StartCoroutine(StartWave());
    }

    //Если враги не спавнятся, то ничего не делаем
    //А если спавнятся, сравниваем время с ласт спавном и спавним врагов
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

        //Если живый врагов и врагов для спана на этой волне не осталось,
        //то заканчиваем волну
        if (_enemiesAlive == 0 && _enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    //Заканчиваем волну, меняя индекс волны и начиная новую
    private void EndWave()
    {
        _isSpawning = false;
        _waveIndex++;
        _timeSinceLastSpawn = 0;
        StartCoroutine(StartWave());
    }

    //Спавн врага из массива
    private void SpawnEnemy()
    {
        
        _enemyToSpawn = _enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length - 1)];
        Instantiate(_enemyToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    //Рассчет сколько врагов должно быть на текущей волне
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_waveIndex, _difficulityFactor));
    }

    //Убиство врага
    private void EnemyKilled()
    {
        _enemiesAlive--;
    }

    //Начало новой волны
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
    }
}
