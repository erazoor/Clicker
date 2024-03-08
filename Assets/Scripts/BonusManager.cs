using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public static BonusManager instance;
    private List<IBonus> activeBonuses = new List<IBonus>();
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;	
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach (var bonus in activeBonuses)
        {
            bonus.UpdateBonus(deltaTime);
        }
    }
    
    public void AddBonus(IBonus bonus)
    {
        activeBonuses.Add(bonus);
        Debug.Log("Added bonus " + bonus.GetType());
    }

	public void SaveBonuses()
	{
		foreach (var bonus in activeBonuses)
        {
            bonus.SaveBonus();
        }
        PlayerPrefs.Save();
	}

	public void LoadBonuses()
    {
        foreach (var bonus in activeBonuses)
        {
            bonus.LoadBonus();
        }
    }

	public void ResetBonuses()
    {
        foreach (var bonus in activeBonuses)
        {
            bonus.ResetBonus();
        }
    }
}
