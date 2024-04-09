using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2f; //How many hits enemy need to die

    private bool isDestroyed = false;
    public void TakeDamege(float dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
