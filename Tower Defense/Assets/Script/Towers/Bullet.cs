using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        if (enemyHealth != null)
        {
            Debug.Log(bulletDamage);
            enemyHealth.TakeDamege(bulletDamage);
            Destroy(gameObject);
        }
    }
}

