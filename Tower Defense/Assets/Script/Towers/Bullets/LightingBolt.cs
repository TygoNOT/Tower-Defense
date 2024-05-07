using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBolt : Bullet
{
    [SerializeField, Range(5, 10)] private float range = 5f;
    [SerializeField, Range(1, 3)] private int maxTarget = 4;
    int targetsHit = 0;

    public LayerMask enemyLayer;

    private readonly string enemyTag = "Enemy";
    private Transform nextTarget;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageType, bulletDamage);
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
            if (targetsHit < maxTarget)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject != collision.gameObject)
                    {
                        nextTarget = collider.gameObject.transform;
                        break;
                    }
                }

                if (nextTarget != null)
                    RedirectBolt(nextTarget);
                else
                {
                    enemyHealth.TakeDamage(damageType, bulletDamage);
                    Destroy(gameObject);
                }

                targetsHit++;
                Debug.Log("I hit this many targets: " + targetsHit);
            }
            else
                Destroy(gameObject);
        }
    }

    private void RedirectBolt(Transform targetPosition)
    {
        base.SetTarget(targetPosition);
    }

}
