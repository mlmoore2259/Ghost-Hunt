using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public float health;
    public float MoveSpeed;
    public PlayerGameInfo playerGameInfo;
    private bool atWall;
    private bool atGhost;

    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        atWall = false;
        atGhost = false;
        health = 100f;
        MoveSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!atWall && !atGhost)
        {
            MoveLeft();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug log the collided object's tag
        //Debug.Log("Collided with: " + other.gameObject.tag);

        // Pass through camera edge colliders
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }

        // Check for collision with Wall
        if (other.gameObject.CompareTag("Wall"))
        {
            atWall = true;
        }

        // Check for collision with another Enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            atGhost = true;
        }

        // Check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            // Reduce player health
            playerGameInfo.possessionLvl += 20;

            // Destroy this ghost
            Destroy(this.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            atWall = false;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            atGhost = false;
        }
    }

    void MoveLeft()
    {
        transform.position += Vector3.left * Time.deltaTime * MoveSpeed;
    }
}
