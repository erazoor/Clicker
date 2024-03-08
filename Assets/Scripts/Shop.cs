using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public SwordBonus swordBonus;
    public CurrencyManager currencyManager;
    public BonusManager bonusManager;
    
    public void BuySwordBonus()
    {
        if (currencyManager.GetCurrentGold() >= swordBonus.cost)
        {
            currencyManager.SpendGold(swordBonus.cost);
            swordBonus.IncreaseLevel();
            bonusManager.AddBonus(swordBonus);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }
}
