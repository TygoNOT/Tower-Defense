using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTower : TurretLogical
{
    private Transform lastHit;
    private Bullet laser;

    private void Start()
    {
        laser = bulletPrefab.GetComponent<Bullet>();
        if (laser != null)
        {
             laser.bulletDamage = 0.25f;
             Debug.Log(laser.bulletDamage);
        }

        lastHit = target;
    }

    protected override void Shoot()
    {
        Bullet laser = bulletPrefab.GetComponent<Bullet>();
        Debug.Log("Target: " + target);
        Debug.Log("Last Hit: " + lastHit);

        if (target != lastHit)
        {
            ResetDamage();
        }

        base.Shoot();
        Debug.Log("Current damage is: " + laser.bulletDamage);
        laser.bulletDamage *= 2f;
        lastHit = target;
    }


    //Reset damage method
    private void ResetDamage()
    {
        laser.bulletDamage = 0.25f;
        Debug.Log("Bullet damage has been reset to default");
    }
}
