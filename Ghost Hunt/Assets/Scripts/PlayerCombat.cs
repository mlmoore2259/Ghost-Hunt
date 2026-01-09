using UnityEngine;
using UnityEngine.InputSystem;
/*
 Actions: 
    - Create an instance of the Blast prefab and launch it in the direction the player is facing when left mouse is clicked (new input system)
 */

public class PlayerCombat : MonoBehaviour
{
    public GameObject BlastPrefab; // Reference to the Blast prefab
    private Transform BlastSpawnPoint; // Point from which the blast will be launched
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Fire blast on left mouse click
        if (player != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnFire();
            }
        }
    }

    private void OnFire()
    {
        
         // Get player's current facing direction and position
         BlastSpawnPoint = player.transform;

        // Instantiate the Blast prefab at the spawn point's position and rotation
        GameObject blast = Instantiate(BlastPrefab, BlastSpawnPoint.position, BlastSpawnPoint.rotation);
            
        // Optionally, add force to the blast if it has a Rigidbody component
        Rigidbody2D blastRigidbody = blast.GetComponent<Rigidbody2D>();
        
        // Add logic here to handle the bullet's movement
    }
}
