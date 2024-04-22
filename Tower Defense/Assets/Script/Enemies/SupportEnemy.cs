using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemy : Enemy
{
    [Header("Attributes")]
    [SerializeField] private float _abilityRange = 10f;
    [SerializeField, Range(0, 1)] private float _healValue = 0.05f;
    [SerializeField, Range(1, 5)] private float _abilityCooldown = 5;
    [SerializeField] protected LayerMask enemyMask;

    private Enemy _ally;

    private void Start()
    {
        CallInStart();
        StartCoroutine(Cooldown());    
    }

    void Update()
    {
        CallInUpdate();
    }

    private void FixedUpdate()
    {
        CallInFixedUpdate();
    }

    private void FindAlly()
    {
        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(transform.position, _abilityRange, (Vector2)transform.position, 0f, enemyMask);
        if (hit2Ds != null && hit2Ds.Length > 0)
        {
            foreach (var hit2D in hit2Ds)
            {
                Enemy pEnemy = hit2D.transform.GetComponent<Enemy>();
                if (pEnemy == this)
                    continue;
                else
                {
                    _ally = pEnemy;
                    break;
                }
            }
        }
    }

    private void HealAlly()
    {
        if (_ally._currentHealth == _ally._maxHealthPoints)
            return;

        _ally._currentHealth = _ally._maxHealthPoints;
    }

    private IEnumerator Cooldown()
    {
        while (!_isDead)
        {
            yield return new WaitForSeconds(_abilityCooldown);
            _movementSpeed = 0;
            FindAlly();

            if (_ally == null)
            {
                _movementSpeed = _originalSpeed;
                continue;
            }

            HealAlly();
            Debug.Log("Ally healed");
            _movementSpeed = _originalSpeed;

        }
    }
}
