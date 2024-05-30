using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBulletArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
