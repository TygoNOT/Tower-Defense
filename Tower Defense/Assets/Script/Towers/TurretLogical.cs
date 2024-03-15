using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;

public class TurretLogical : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint; //Variable for turret handling
    [SerializeField] private LayerMask enemyMask; //Variable for enemy layer
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f; //Variable to determine turret range
    [SerializeField] private float rotationSpeed = 200f; //Variable to determine the speed of rotation of the turret
    [SerializeField] private float bps = 1f;  //Bullets Per Second


    private Transform target; //Enemy variable
    private float timeUntilFire;


    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire > 1f / bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    //Tower shoot
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    //Enemy search method 
    private void FindTarget()
    {
        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask); //Searching for an enemy in the turret action range

        if (hit2Ds.Length > 0)
        {
            target = hit2Ds[0].transform;
        }
    }

    //Aiming the turret at the enemy
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y ,target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f; //Variable storing the degree of angle between the turret and the enemy

        //Rotate the turret towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
    }

    //Method to check is enemy in range turret
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    // Range visibility in Game
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red; //Range color
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange); //Drawing turretRange
    }
}
