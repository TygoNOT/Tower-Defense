using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2f; //How many hits enemy need to die
    [SerializeField] private int CurrenceWorth = 5;
    private bool isDestroyed = false;
    public void TakeDamege(float dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(CurrenceWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
