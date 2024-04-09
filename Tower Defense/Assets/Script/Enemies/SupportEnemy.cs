using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemy : Enemy
{
    [Header("Attributes")]
    [SerializeField] private float _abilityRange = 5f;
    [SerializeField, Range(0, 1)] private float _healValue = 0.05f;
    [SerializeField, Range(1, 5)] private float _abilityCooldown = 5;
    [SerializeField] protected LayerMask enemyMask;

    private Enemy _ally;

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
            _ally = hit2Ds[0].transform.GetComponent<Enemy>();
    }

    private void HealAlly()
    {
        if (_ally == null)
            return;

        if (_ally._currentHealth == _ally._maxHealthPoints)
            return;

        _ally._currentHealth += _ally._maxHealthPoints * _healValue;
    }

    private IEnumerator Cooldown()
    {
        while (!_isDead)
        {
            FindAlly();
            HealAlly();
            yield return new WaitForSeconds(_abilityCooldown);
        }
    }
}
