using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static CurrencyManager currencyManager;
	public static BonusManager bonusManager;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProgression();
        }
        // TODO : change this by the main menu
    }

	void Start()
	{
		OnApplicationStart();
	}
    
    public void OnApplicationQuit()
    {
        SaveGame();
    }
    
    public void OnApplicationStart()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        if (CurrencyManager.instance != null)
        {
            CurrencyManager.instance.SaveGame();
        }
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.SaveGame();
        }
		if (BonusManager.instance != null)
        {
			BonusManager.instance.SaveBonuses();
        }
        PlayerPrefs.Save();
    }
    
    public void ResetProgression()
    {       
        if (CurrencyManager.instance != null)
        {
            CurrencyManager.instance.ResetCurrency();
        }
            
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.ResetEnemies();
        }

		if (BonusManager.instance != null)
        {
            BonusManager.instance.ResetBonuses();
        }
            
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
            
        SaveGame();
        LoadGame();
    }
    
    public void LoadGame()
    {
        if (CurrencyManager.instance != null)
        {
            CurrencyManager.instance.LoadGame();
        }
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.LoadGame();
        }
	    if (BonusManager.instance != null)
        {
            BonusManager.instance.LoadBonuses();
        }
    }
    
    public int CalculateGoldReward()
    {   
        return 1;
    }
}
