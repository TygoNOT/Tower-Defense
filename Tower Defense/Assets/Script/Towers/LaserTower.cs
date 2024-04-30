using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTower : TurretLogical
{
    private float defaultDamage = 0.25f;

    private void Start()
    {
        Bullet laser = bulletPrefab.GetComponent<Bullet>();
        if (laser != null)
        {
             defaultDamage = laser.bulletDamage;
             Debug.Log(defaultDamage);
        }
    }

    protected override void Shoot()
    {
        Bullet laser = bulletPrefab.GetComponent<Bullet>();
        laser.bulletDamage = defaultDamage;

        //Everytime it shoots, damage value increased in 2 times
        if(target != null)
        {
            if (laser != null)
            {
                defaultDamage *= 2f;
                Debug.Log(defaultDamage);

                Enemy enemy = gameObject.GetComponent<Enemy>();
                //if enemy died or left the range, damage value should reset to default value
                if (target == null)
                {
                    ResetDamage();
                }
                else
                {
                    Debug.Log("The damage value did not reset");
                }
            }
            base.Shoot();
        }
        
    }

    //Reset damage method
    private void ResetDamage()
    {
        Bullet laser = bulletPrefab.GetComponent<Bullet>();
        if (laser != null)
        {
            laser.bulletDamage = 0.25f;
            Debug.Log("Bullet damage has been reset to default: " + laser.bulletDamage);
        }
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
