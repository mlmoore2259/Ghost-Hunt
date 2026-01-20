using TMPro;
using UnityEngine;

public class PlayerGameInfo : MonoBehaviour
{
    // Variables to track player information
    public float health;
    public int score;
    public int coins;
    public float possessionLvl;
    public float spiritCleanupHealth;
    private GameObject player;
    private float healthDecrease;
    private bool possessedFlag;

    // UI Elements
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Awake()
    {
        health = 100f;
        score = 0;
        coins = 10;
        possessionLvl = 0;
        spiritCleanupHealth = 100f;
        healthDecrease = 1f;
        possessedFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Update health decrease rate based on possession level
        healthDecrease = possessionLvl / 100;
        
        // Start possession effects
        if (possessionLvl >= 100f && !possessedFlag)
        {
            possessedFlag = true;
            InvokeRepeating("UpdateHealth", 0f, 1f);
        }

       // End possession effects
       if (possessionLvl < 100f && possessedFlag)
        {
            possessedFlag = false;
            CancelInvoke("UpdateHealth");
        }
    }

    private void UpdateHealth()
    {
        health -= healthDecrease;
    }
}
