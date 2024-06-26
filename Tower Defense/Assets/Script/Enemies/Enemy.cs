using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Nihuya ne ponyatno ������� ����� �����

    /// <summary>
    /// ������� ����� �����, ��� ������� �������� � ������ ������������ ������� �����,
    /// ����� ��� ������ �� ����, ��������� �����, ������, ������(��� ���������� �� ����� �����),
    /// ����� � �.�
    /// </summary>
    [Header("References")]
    [SerializeField] protected Rigidbody2D _enemyBody;
    [SerializeField] protected Animator _animator;

    [Header("Properties")]
    [SerializeField, Range(1, 100)] public float _maxHealthPoints;
    [SerializeField, Range(1, 25)] protected float _movementSpeed;
    [SerializeField, Range(0, 1), Tooltip("�� ��������� ��������")] protected int _armorType;
    [SerializeField, Range(0.01f, 1)] protected float _armorPoint;
    [SerializeField, Range(0, 20)] protected int _damage;
    [SerializeField] protected int _headBounty = 5;

    private HealthBar _healthBar;

    protected Transform _destination;
    protected Vector2 _direction;
    protected int _pathIndex;
    public bool _isDead;
    public float _currentHealth, _originalSpeed;

    private float _damageTaken;

    private void Awake()
    {
        _healthBar = GetComponent<HealthBar>();
    }

    //��� ������ ���������� ������ ���� � ����, ������� �������� � ������������ � ���� ����(���� ����)
    protected void CallInStart()
    {
        _pathIndex = 0;
        _currentHealth = _maxHealthPoints;
        _destination = LevelManager.main.pathPoints[_pathIndex];
        _healthBar.ChangeBarValue(_currentHealth, _maxHealthPoints);
        _isDead = false;
        _originalSpeed = _movementSpeed;
    }

    //��� ����� ������� ���� �������� � ������� ������ ��������� �����
    //� ���� ������ ����������� ����� �� ���� �� ����� ��������,
    //���� ��, �� ���� �������� ��������� ����� ���� ���� ����
    protected void CallInUpdate()
    {
        if (_isDead) 
            return;

        if (Vector2.Distance(_destination.position, transform.position) <= 0.1f)
        {
            _pathIndex++;

            //��� ���������� �����, ���� ������������ � ���������� �������� � �����
            if (LevelManager.main.pathPoints.Length == _pathIndex)
            {
                DoDamage();
                return;
            }
            //���� ����� �� ��������, �� ���� ���� ������
            else
                _destination = LevelManager.main.pathPoints[_pathIndex];
        }

        //���� �������� ����� ��� ������ ����
        if (_currentHealth <= 0)
            EnemyDie();
    }

    //����� ���������� � ������ ������� � ������ �����
    //��������� ��� ���� ��������� � ��������� �����
    protected void CallInFixedUpdate()
    {
        if (_isDead)
            return;

        _direction = (_destination.position - transform.position).normalized;
        _enemyBody.velocity = _direction * _movementSpeed;
    }

    //��������� ����� ������
    protected void DoDamage()
    {
        LevelManager.main.health -= _damage;
        Destroy(gameObject);
        EnemySpawner.onEnemyDestroy.Invoke();
        Debug.Log("Current Player Health: " + LevelManager.main.health);
        Handheld.Vibrate();
    }

    //��������� ����� �� �����
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

    //������
    public virtual void EnemyDie()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        LevelManager.main.IncreaseCurrency(_headBounty);
        LevelManager.main.doomLevel--;
        _isDead = true;
        Destroy(gameObject);
        // ����� ���� ����� �� �������� �����
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
