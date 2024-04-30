using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTower : TurretLogical
{
    [Header("ElectricTower Attributes")]
    [SerializeField] private Transform firingPoint2;
    [SerializeField] private Transform firingPoint3;

    public int numberOfTargets = 3;

    protected override void Shoot()
    {
        List<Transform> targetsInRange = FindTargets();

        if (targetsInRange.Count > 0)
        {
            int targetsToShoot = Mathf.Min(numberOfTargets, targetsInRange.Count);

            for (int i = 0; i < targetsToShoot; i++)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                bulletScript.SetTarget(targetsInRange[i]);
            }
        }
    }

    private List<Transform> FindTargets()
    {
        List<Transform> targets = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        //Adding targets 
        foreach (Collider2D collider in colliders)
        {
            targets.Add(collider.transform);
        }

        return targets;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void RotateTowardsTarget()
    {
        base.RotateTowardsTarget();
    }

    protected override void FindTarget()
    {
        base.FindTarget();
    }

    protected override bool CheckTargetIsInRange()
    {
        return base.CheckTargetIsInRange();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
