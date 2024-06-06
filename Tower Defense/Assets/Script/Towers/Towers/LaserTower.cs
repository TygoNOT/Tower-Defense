using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTower : TurretLogical
{
    private Transform lastHit;
    private Bullet laser;
    private GameObject laserObj;
    private LineController laserBeam;


    [SerializeField]
    private Transform origin;
    [SerializeField]
    public GameObject laserPrefab;


    private void Start()
    {
        base.Start();
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

        if (target != null && laserBeam == null)
        {
            GameObject laserObj = Instantiate(laserPrefab, origin.position, Quaternion.identity);
            laserBeam = laserObj.GetComponent<LineController>();
            laserBeam.SetStartPosition(origin.position);
            laserBeam.SetEndPosition(target.position);
            StartCoroutine(UpdateLaserBeamCoroutine(target.position, laserBeam));
        }

    }

    protected override void Update()
    {
        base.Update();

        if (target != null)
        {
            if (lastHit != target || laserBeam == null)
            {
                if (laserObj != null)
                {
                    laserObj.SetActive(false);
                }

                if (laserBeam != null)
                {
                    Destroy(laserBeam.gameObject);
                    laserBeam = null;
                }

                laserObj = Instantiate(laserPrefab, origin.position, Quaternion.identity);
                laserBeam = laserObj.GetComponent<LineController>();
            }

            if (laserBeam != null)
            {
                laserBeam.SetStartPosition(origin.position);
                laserBeam.SetEndPosition(target.position);
                StartCoroutine(UpdateLaserBeamCoroutine(target.position, laserBeam));
            }
        }
        else
        {
            if (laserBeam != null)
            {
                Destroy(laserBeam.gameObject);
                laserBeam = null;
            }

            if (laserObj != null)
            {
                laserObj.SetActive(false);
            }
        }
    }
    private void ResetDamage()
    {
        laser.bulletDamage = 0.25f;
        Debug.Log("Bullet damage has been reset to default");
    }

    
    private IEnumerator UpdateLaserBeamCoroutine(Vector3 targetPosition, LineController lineController)
    {
        float elapsedTime = 0f;
        float duration = 0.1f;

        Vector3 startPosition = origin.position;
        Vector3 endPosition = targetPosition;

        while (elapsedTime < duration)
        {
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            lineController.SetEndPosition(currentPosition);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lineController.SetEndPosition(endPosition);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        if (laser != null)
        {
            laser.bulletDamage = 0.25f; 
        }
    }
}
