using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    
    public Enemy currentEnemy;
    public Enemy enemyPrefab;
    public Enemy bossPrefab;

    public int enemiesDefeated = 0;
    public float enemyDifficultyIncrease = 1.2f;
    public float bossDifficultyIncrease = 1.6f;
    private int enemyBaseHealth = 10;

    private int lastEnemyHealthBeforeBoss;
    

    public int currentEnemyHealth;
    public bool isCurrentEnemyABoss;
    
    public TextMeshProUGUI timerText;
    public float timeForEnemy = 30f;
    private float currentTime;

	public TextMeshProUGUI enemiesDefeatedText;

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
        LoadGame();
    }

    void Start()
    {
        if(enemiesDefeated == 0 || currentEnemyHealth == 0)
        {
            SpawnEnemy();
        }
        else
        {
            ResumeEnemy();
        }
		LoadGame();
    }
    
    void Update()
    {
        if (currentEnemy != null)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
            if (currentTime <= 0)
            {
                ResetCurrentEnemyHealth();
            }
        }
    }

    public void SpawnEnemy()
    {
        Enemy newEnemy;
        int cycleIndex = enemiesDefeated / 5;
        bool isBoss = enemiesDefeated % 5 == 0 && enemiesDefeated != 0;

		Vector3 bottomRightScreen = new Vector3(Screen.width, 0, Camera.main.nearClipPlane);
    	Vector3 bottomRightWorld = Camera.main.ScreenToWorldPoint(bottomRightScreen);

		float enemyOffsetX = 5f;
		float enemyOffsetY = isBoss ? 2f : 1.5f;

		bottomRightWorld.x -= enemyOffsetX;
    	bottomRightWorld.y += enemyOffsetY;
        
        if (!isBoss && enemiesDefeated % 5 == 1 && lastEnemyHealthBeforeBoss > 0) 
        {
            enemyBaseHealth = Mathf.RoundToInt(lastEnemyHealthBeforeBoss * enemyDifficultyIncrease);
        }

        newEnemy = Instantiate(isBoss ? bossPrefab : enemyPrefab, bottomRightWorld, Quaternion.identity);
        int health;

        if (isBoss) 
        {
            health = Mathf.RoundToInt(enemyBaseHealth * 2.2f * Mathf.Pow(bossDifficultyIncrease, cycleIndex - 1));
        } 
        else 
        {
            health = Mathf.RoundToInt(enemyBaseHealth * Mathf.Pow(enemyDifficultyIncrease, enemiesDefeated % 5));
            if (enemiesDefeated % 5 == 4) 
            {
                lastEnemyHealthBeforeBoss = health;
            }
        }
        
        currentTime = timeForEnemy;
        UpdateTimerText();

        SetEnemy(newEnemy, health, isBoss);
    }

    private void ResumeEnemy()
    {
		Vector3 bottomRightScreen = new Vector3(Screen.width, 0, Camera.main.nearClipPlane);
    	Vector3 bottomRightWorld = Camera.main.ScreenToWorldPoint(bottomRightScreen);

		float enemyOffsetX = 5f;
		float enemyOffsetY = isCurrentEnemyABoss ? 2f : 1.5f;

		bottomRightWorld.x -= enemyOffsetX;
    	bottomRightWorld.y += enemyOffsetY;
		
        Enemy newEnemy = Instantiate(isCurrentEnemyABoss ? bossPrefab : enemyPrefab, bottomRightWorld, Quaternion.identity);
        SetEnemy(newEnemy, currentEnemyHealth, isCurrentEnemyABoss);
    }

    private void SetEnemy(Enemy enemy, int health, bool isBoss)
    {
        if (currentEnemy != null) 
        {
            Destroy(currentEnemy.gameObject);
        }
        
        currentEnemy = enemy;
     
        enemy.health = health;
        enemy.maxHealth = health;
        isCurrentEnemyABoss = isBoss;
        currentEnemyHealth = health;
        if (enemy.healthBar != null)
        {
            enemy.healthBar.SetMaxHealth(health);
            enemy.healthBar.UpdateHealth(health);
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
		UpdateEnemiesDefeatedText();
        SpawnEnemy();
    }

    public void ResetEnemies()
    {
        enemiesDefeated = 0;
        currentEnemyHealth = enemyBaseHealth;
        isCurrentEnemyABoss = false;
        
        lastEnemyHealthBeforeBoss = enemyBaseHealth;
        
        if (currentEnemy != null) 
        {
            Destroy(currentEnemy.gameObject);
            currentEnemy = null;
        }
        
        currentTime = timeForEnemy;
        UpdateTimerText();
        
        SpawnEnemy();
    }
    
    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.Max(currentTime, 0):0.0}";
        }
    }
    
    private void ResetCurrentEnemyHealth()
    {
        if (currentEnemy != null)
        {
            currentEnemy.health = currentEnemy.maxHealth;
            if (currentEnemy.healthBar != null)
            {
                currentEnemy.healthBar.UpdateHealth(currentEnemy.health);
            }
            currentTime = timeForEnemy;
        }
    }

	void UpdateEnemiesDefeatedText()
    {
        if (enemiesDefeatedText != null)
        {
            enemiesDefeatedText.text = "Level: " + enemiesDefeated.ToString();
        }
    }
    
    public void SaveGame()
    {
        PlayerPrefs.SetInt("EnemiesDefeated", enemiesDefeated);
        PlayerPrefs.SetInt("CurrentEnemyHealth", currentEnemyHealth);
        PlayerPrefs.SetInt("IsCurrentEnemyABoss", isCurrentEnemyABoss ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        enemiesDefeated = PlayerPrefs.GetInt("EnemiesDefeated", 0);
        currentEnemyHealth = PlayerPrefs.GetInt("CurrentEnemyHealth", enemyBaseHealth);
        isCurrentEnemyABoss = PlayerPrefs.GetInt("IsCurrentEnemyABoss", 0) == 1;
		UpdateEnemiesDefeatedText();
    }
}
