using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachGun : TurretLogical
{
    //MachineGun script for second Firing Point
    [Header("MachGun Attributes")]
    [SerializeField] private Transform firingPoint2;
    [SerializeField] private AudioSource shot;

    private void Awake()
    {
        shot = GameObject.Find("Heavy Shot").GetComponent<AudioSource>();
        if (shot != null )
        {
            Debug.Log("I found it");
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
            shot.Play();
            GameObject bulletObj1 = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            GameObject bulletObj2 = Instantiate(bulletPrefab, firingPoint2.position, Quaternion.identity);

            // Set target for both bullets
            Bullet bulletScript1 = bulletObj1.GetComponent<Bullet>();
            Bullet bulletScript2 = bulletObj2.GetComponent<Bullet>();
            bulletScript1.SetTarget(target);
            bulletScript2.SetTarget(target);
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
}
