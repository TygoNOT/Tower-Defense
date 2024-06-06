using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTower : TurretLogical
{
    [SerializeField] private AudioSource shot;

    private void Awake()
    {
        shot = GameObject.Find("Cum").GetComponent<AudioSource>();
        if (shot != null)
        {
            Debug.Log("I found it");
        }
    }

    protected override void Shoot()
    {
        shot.Play();
        base.Shoot();
    }    
}
