using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBonus : MonoBehaviour, IBonus
{
    public float damagePerSecond = .1f;
	public int cost = 10; 
    public int level = 1;
    public CurrencyManager currencyManager;
    public EnemyManager enemyManager;
	private float damageAccumulator = 0f;

	void Start()
	{
	    LoadBonus();
	}

    public void UpdateBonus(float deltaTime)
    {
        if (enemyManager.currentEnemy != null) {
            damageAccumulator += damagePerSecond * deltaTime;
            int damageToApply = Mathf.FloorToInt(damageAccumulator);

            if (damageToApply > 0) {
                enemyManager.currentEnemy.TakeDamage(damageToApply);
                currencyManager.AddGold(damageToApply);
                damageAccumulator -= damageToApply;
            }
        }
    }

    public void IncreaseLevel()
    {
        if(level < 100)
        {
            level++;
            damagePerSecond *= 1.075f;
			cost = Mathf.FloorToInt(cost * 1.15f);
        }
    }

	public void ResetBonus()
 	{
        level = 1;
        damagePerSecond = .1f;
        damageAccumulator = 0f;
		cost = 10;
    }

	public void SaveBonus()
    {
        PlayerPrefs.SetInt("SwordBonusLevel", level);
        PlayerPrefs.SetFloat("SwordBonusDamagePerSecond", damagePerSecond);
		PlayerPrefs.SetInt("SwordBonusCost", cost);
    }

	    public void MakeBonusActive()
    	{
        	BonusManager.instance.AddBonus(this);
    	}

	public void LoadBonus()
    {
        level = PlayerPrefs.GetInt("SwordBonusLevel", 1);
        damagePerSecond = PlayerPrefs.GetFloat("SwordBonusDamagePerSecond", .1f);
		cost = PlayerPrefs.GetInt("SwordBonusCost", 10);

		for (int i = 1; i < level; i++) {
    	    damagePerSecond *= 1.075f;
    	}

		if (level > 1) {
        	MakeBonusActive();
    	}
    }
}
