using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
	public float maxHealth;

    public HealthBar healthBar;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (healthBar != null)
        {
            healthBar.UpdateHealth(health);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        CurrencyManager.instance.AddGold(GameManager.instance.CalculateGoldReward());
        FindObjectOfType<EnemyManager>().EnemyDefeated();
        Destroy(gameObject);
    }
}
