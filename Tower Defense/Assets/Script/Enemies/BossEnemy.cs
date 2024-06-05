using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    private void Start() => CallInStart();

    private void Update() => CallInUpdate();

    private void FixedUpdate() => CallInFixedUpdate();

    public override void EnemyDie()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        LevelManager.main.IncreaseCurrency(_headBounty);
        _isDead = true;

        _animator.SetTrigger("isDead");
        StartCoroutine(AfterLife());
    }

    private IEnumerator AfterLife()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLenght);
        Destroy(_animator);
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
        LevelManager.main.overDoom = true;
    }
}
