using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D _enemyBody;

    [Header("Properties")]
    [SerializeField, Range(1, 100)] protected int _maxHealthPoints;
    [SerializeField, Range(1, 25)] protected float _movementSpeed;
    [SerializeField, Range(0, 1), Tooltip("Не изменяйте значение")] protected int _armorType;
    [SerializeField, Range(0, 15)] protected int _armorPoint;
    [SerializeField, Range(0, 20)] protected int _damage;

    protected Transform _destination;
    protected Vector2 _direction;
    protected int _pathIndex, _currentHealth, _damageTaken;

    private void Start()
    {
        _pathIndex = 0;
        _currentHealth = _maxHealthPoints;
        _destination = LevelManager.main.pathPoints[_pathIndex];
    }

    protected void CallInUpdate()
    {
        if (Vector2.Distance(_destination.position, transform.position) <= 0.1f)
        {
            _pathIndex++;

            if (LevelManager.main.pathPoints.Length == _pathIndex)
            {
                Destroy(gameObject);
                EnemySpawner.onEnemyDestroy.Invoke();
                DoDamage();
                return;
            }
            else
                _destination = LevelManager.main.pathPoints[_pathIndex];
        }

        if (_currentHealth <= 0)
            EnemyDie();
    }

    protected void CallInFixedUpdate()
    {
        _direction = (_destination.position - transform.position).normalized;
        _enemyBody.velocity = _direction * _movementSpeed;
    }

    protected void DoDamage()
    {
        LevelManager.main.Health -= _damage;
    }

    public void TakeDamage(int damageType, int damageValue)
    {
        if (_armorType == damageType)
        {
            _damageTaken = damageValue - _armorPoint;
            if (_damageTaken > 0)
            {
                _currentHealth -= _damageTaken;
            }
        }
        else
        {
            _damageTaken = damageValue;
            _currentHealth -= _damageTaken;
        }
    }

    protected void EnemyDie()
    {
        Destroy(gameObject);
        // Нужно дать денег за убийство врага
    }
}
