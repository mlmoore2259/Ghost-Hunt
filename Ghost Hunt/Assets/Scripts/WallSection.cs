using UnityEditorInternal;
using UnityEngine;

public class WallSection : MonoBehaviour
{
    [SerializeField] int health;
    private int healthDecrease;
    private bool broken;
    private bool isBreaking;
    private bool withGhost;
    public GameObject physicalWall;
    public GameObject rebuildTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    void Awake()
    {
        health = 100;
        broken = false;
        isBreaking = false;
        withGhost = false;
        healthDecrease = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            broken = true;
            physicalWall.SetActive(false);
        }

        // Start Breaking
        if(withGhost && !isBreaking && !broken)
        {
            InvokeRepeating("ReduceHealth", 0f, 1f);
            isBreaking = true;
        }

        // Stop breaking
        if (!withGhost && isBreaking && !broken)
        {
            CancelInvoke("ReduceHealth");
        }
    }

    // Detect ghot collision and rebuild trigger collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            withGhost = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            withGhost = false;
        }
    }

    void ReduceHealth()
    {
        health -= healthDecrease;
    }
}
