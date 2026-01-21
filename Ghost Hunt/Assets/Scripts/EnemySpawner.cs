using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*
     Starts at offscreen positions (X = 15)
     Uses a Y range of -6 to 5 (accounts for height of ghost sprite)
     Spawns enemies at random Y position every 2 seconds
     */
    public GameObject enemy;
    private GameObject player;
    public List<float> enemiesYCoord;
    public float spawnInterval;
    public float timeUntilStart;
    public float spawnYMin = -2f;
    public float spawnYMax = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create timer to spawn enemies every 2 seconds
        InvokeRepeating("SpawnEnemy", timeUntilStart, spawnInterval);
        player = GameObject.FindWithTag("Player");
    }
    private void Awake()
    {
        spawnInterval = 4.0f;
        timeUntilStart = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private float BestCandidate()
    {
        float bestCandidate;
        int numCandidates = 3 * enemiesYCoord.Count;
        float[] candidates = new float[numCandidates];

        // Starting case
        if (enemiesYCoord.Count == 0)
        {
            return Random.Range(spawnYMin, spawnYMax);
        }

        // Generate candidates
        for (int i = 0; i < numCandidates; i++)
        {
            candidates[i] = Random.Range(spawnYMin, spawnYMax);
        }

        bestCandidate = candidates[0];

        // Compare candidates and choose the farthest from existing enemies
        for (int i = 0; i < numCandidates; i++)
        {
            for (int j = 0; j < enemiesYCoord.Count; j++)
            {
                if (Mathf.Abs(candidates[i] - enemiesYCoord[j]) > bestCandidate)
                {
                    bestCandidate = candidates[i];
                }
            }
        }
        return bestCandidate;
    }

    // Create an instance of an enemy at a random Y position
    void SpawnEnemy()
    {
        // Load the enemy prefab
        if (enemy != null)
        {
            // Generate a random Y position between -6 and 5
            float randomY = BestCandidate();
            enemiesYCoord.Add(randomY);
            Vector3 spawnPosition = new Vector3(15f, randomY, 0f);
            // Instantiate the enemy at the spawn position
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab is not assigned in the EnemySpawner script.");
        }
    }
}
