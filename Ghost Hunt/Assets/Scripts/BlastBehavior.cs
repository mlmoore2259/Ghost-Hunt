using UnityEngine;

public class BlastBehavior : MonoBehaviour
{
    private float BlastSpeed;
    public PlayerGameInfo playerGameInfo;
    private EnemySpawner enemySpawner;
    public GameObject coin;
    public PlayerMovement playerMovement;
    private bool fireRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        GameObject player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        fireRight = playerMovement.facingRight;
    }

    private void Awake()
    {
        BlastSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // Move in the direction the player is facing at BlastSpeed
        if (fireRight)
        {
            transform.Translate(Vector2.right * BlastSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * BlastSpeed * Time.deltaTime);
        }
        //if (IsOutOfScreen(this.gameObject))
        //{
        //    Destroy(this.gameObject);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Get the enemies health and reduce it
            GhostBehavior ghostBehav = other.gameObject.GetComponent<GhostBehavior>();
            if (ghostBehav != null)
            {
                ghostBehav.health -= 20f;
                if (ghostBehav.health <= 0f)
                {
                    UpdateScore(1);
                    enemySpawner.enemiesYCoord.Remove(other.transform.position.y);
                    Destroy(other.gameObject); // Destroy the enemy if health is 0
                    DropCoin(); // Drop a coin upon enemy death
                }
            }
            Destroy(this.gameObject);
        }
    }

    // Access the PlayerGameInfo script to update score
    void UpdateScore(int points)
    {
        Debug.Log("Updating score by: " + points);
        playerGameInfo.GetComponent<PlayerGameInfo>().score += points;
    }

    void DropCoin()
    {
        Vector3 ghostPos = transform.position;
        Instantiate(coin, ghostPos, Quaternion.identity);
    }
}
