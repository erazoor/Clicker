using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [SerializeField] public TMPro.TMP_Text goldText;

    private int gold = 0;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;	
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

	public void Start() 
	{
		UpdateGoldUI();
	}

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }
    
    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            return true;
        }
        return false;
    }
    
    public int GetCurrentGold()
    {
        return gold;
    }
    
    public void LoadGame()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        Debug.Log("Loaded gold: " + gold);
        UpdateGoldUI();
    }
    
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }

	public void ResetCurrency()
	{
    	gold = 0;
    	UpdateGoldUI();
	}
    
    private void UpdateGoldUI()
    {
        goldText.text = "Gold: " + gold;
    }
}
