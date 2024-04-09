using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void ChangeBarValue(float currentHealth, float maxHealth)
    {
        healthBar.value = currentHealth / maxHealth;
    }

    void Update()
    {
        
    }
}
