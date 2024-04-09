using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Nihuya ne ponyatno Ѕазовый класс врага

    /// <summary>
    /// Ѕазовый класс врага, где описаны свойства и методы свойственные каждому врагу,
    /// такие как ходьба до цели, получение урона, смерть, победа(при дохождение до конца живыи),
    /// брон€ и т.д
    /// </summary>
    [Header("References")]
    [SerializeField] protected Rigidbody2D _enemyBody;

    [Header("Properties")]
    [SerializeField, Range(1, 100)] public float _maxHealthPoints;
    [SerializeField, Range(1, 25)] protected float _movementSpeed;
    [SerializeField, Range(0, 1), Tooltip("Ќе измен€йте значение")] protected int _armorType;
    [SerializeField, Range(0.01f, 1)] protected float _armorPoint;
    [SerializeField, Range(0, 20)] protected int _damage;

    private HealthBar _healthBar;

    protected Transform _destination;
    protected Vector2 _direction;
    protected int _pathIndex;
    protected bool _isDead;
    public float _currentHealth;

    private float _originalSpeed, _damageTaken;

    private void Awake()
    {
        _healthBar = GetComponent<HealthBar>();
    }

    //ѕри старте уровниваем индекс пути к нулю, ровн€ем здоровье к максимальной и даем цель(куда идти)
    private void Start()
    {
        _pathIndex = 0;
        _currentHealth = _maxHealthPoints;
        _destination = LevelManager.main.pathPoints[_pathIndex];
        _healthBar.ChangeBarValue(_currentHealth, _maxHealthPoints);
        _isDead = false;
        _originalSpeed = _movementSpeed;
    }

    //Ёто метод который надо вызывать в апдейте любого дочерного врага
    //¬ этом методе провер€етс€ дошел ли враг до точки поворота,
    //если да, то враг получает следующую точку куда надо идти
    protected void CallInUpdate()
    {
        if (Vector2.Distance(_destination.position, transform.position) <= 0.1f)
        {
            _pathIndex++;

            //ѕри достижении конца, враг уничтожаетс€ и вычитывает здоровье у врага
            if (LevelManager.main.pathPoints.Length == _pathIndex)
            {
                Destroy(gameObject);
                EnemySpawner.onEnemyDestroy.Invoke();
                DoDamage();
                return;
            }
            //≈сли точка не конечна€, то враг идет дальше
            else
                _destination = LevelManager.main.pathPoints[_pathIndex];
        }

        //≈сли здоровье ровно или меньше нул€
        if (_currentHealth <= 0)
            EnemyDie();
    }

    //ћетод вызываетс€ в фиксет апдейте в каждом враге
    //Ѕлагодар€ ему враг двигаетс€ к последней точке
    protected void CallInFixedUpdate()
    {
        _direction = (_destination.position - transform.position).normalized;
        _enemyBody.velocity = _direction * _movementSpeed;
    }

    //Ќанесение урона игроку
    protected void DoDamage()
    {
        LevelManager.main.Health -= _damage;
    }

    //ѕолучение урона от башен
    public void TakeDamage(int damageType, float damageValue)
    {
        if (_armorType == damageType)
        {
            _damageTaken = damageValue * _armorPoint;
            _currentHealth -= _damageTaken;
        }
        else
        {
            _damageTaken = damageValue;
            _currentHealth -= _damageTaken;
        }

        _healthBar.ChangeBarValue(_currentHealth, _maxHealthPoints);

        if (_currentHealth < 0 && !_isDead)
            EnemyDie();
    }

    //—мерть
    protected void EnemyDie()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        _isDead = true;
        Destroy(gameObject);
        // Ќужно дать денег за убийство врага
        // Xyecoc ymiraet
    }

    //Slow effect
    public void ApplySlow(float duration, float slowFactor)
    {
        StartCoroutine(SlowEffect(duration, slowFactor));
    }

    //Coruutine to apply slow effect
    private IEnumerator SlowEffect(float duration, float slowFactor)
    {
        if (_originalSpeed == _movementSpeed)
        {
            _movementSpeed *= slowFactor;
            yield return new WaitForSeconds(duration);
            _movementSpeed = _originalSpeed;
        }
        else
        {
            yield return new WaitForSeconds(0);
        }
    }
}
