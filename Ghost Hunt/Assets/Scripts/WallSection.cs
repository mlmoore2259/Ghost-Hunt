using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallSection : MonoBehaviour
{
    public int health;
    private int healthDecrease;
    private int coins;
    public bool broken;
    [SerializeField] bool isBreaking;
    [SerializeField] bool atGhost;
    public GameObject physicalWall;
    public Collider2D rebuildTrigger;
    public PlayerGameInfo playerGameInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        physicalWall = this.gameObject;
    }

    void Awake()
    {
        health = 100;
        broken = false;
        isBreaking = false;
        atGhost = false;
        healthDecrease = 10;
    }

    // Update is called once per frame
    void Update()
    {
        coins = playerGameInfo.coins;
        if (health <= 0)
        {
            broken = true;
            physicalWall.SetActive(false);

            // Stop breaking
            CancelInvoke("ReduceHealth");
            isBreaking = false;
        }

        // Start Breaking
        else if(atGhost && !isBreaking && !broken)
        {
            InvokeRepeating("ReduceHealth", 0f, 1f);
            isBreaking = true;
        }

        // Stop breaking
        else if (!atGhost && isBreaking)
        {
            CancelInvoke("ReduceHealth");
            isBreaking = false;
        }
    }

    // Detect ghost collision and rebuild trigger collision
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            atGhost = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            atGhost = false;
        }
    }

    void ReduceHealth()
    {
        health -= healthDecrease;
    }
}
