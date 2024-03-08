using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;
    private float maxHealth;
    
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        UpdateHealth(health);
    }
    
    public void UpdateHealth(float currentHealth)
    {
        
        float healthPercentage = (float)currentHealth / maxHealth;
        image.fillAmount = healthPercentage;
    }
}
