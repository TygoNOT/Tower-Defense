using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    

    [Header("Attributes")]
    [SerializeField] protected int damageType = 0;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] public float bulletDamage = 2;

    private Transform target; //Variable for enemy

    // Set Target
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    //bullet flight
    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

    }

    //What happend after Enemy got shot
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageType, bulletDamage);
            Destroy(gameObject);
        }
    }
}

