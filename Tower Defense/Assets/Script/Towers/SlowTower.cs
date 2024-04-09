using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : TurretLogical
{
    [Header("Slow Tower Attributes")]
    [SerializeField] private float slowDuration = 3f; // Slow Duration (secs) for the enemies
    [SerializeField] private float slowFactor = 0.7f; //Decreasing speed of the enemies

    /// <summary>
    /// Реп про Равшана
    /// БЕЕЕЕЕ! Это крик тех, кто свободен,
    /// Кто готов бороться, кто верит в себя.
    /// БЕЕЕЕЕ! Это не слабость, а мощь,
    /// Когда бараны ревут, знай, они на взводе.
    /// </summary>

    protected override void Update()
    {
        base.Update();
    }
    protected override void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); //shoot blyat
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        // Apply slow effect to the enemy
        ApplySlowEffect(target.GetComponent<Enemy>());
    }

    protected override void FindTarget()
    {
        base.FindTarget();
    }

    protected override void RotateTowardsTarget()
    {
        base.RotateTowardsTarget();
    }

    protected override bool CheckTargetIsInRange()
    {
        return base.CheckTargetIsInRange();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }

    private void ApplySlowEffect(Enemy enemy)
    {
        if (enemy != null)
        {
            //Apllying slow effect
            enemy.ApplySlow(slowDuration, slowFactor);
        }
    }
}
