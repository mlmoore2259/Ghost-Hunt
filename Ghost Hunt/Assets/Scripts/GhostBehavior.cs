using UnityEngine;
using UnityEngine.UI;

public class GhostBehavior : MonoBehaviour
{
    public Image healthBar;
    public float health;
    [SerializeField] float MoveSpeed;
    public PlayerGameInfo playerGameInfo;
    [SerializeField] bool atWall;
    [SerializeField] bool atGhost;
    public bool cleanupFlag;
    [SerializeField] float cleanupDelimeter;

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
        cleanupFlag = true;
        cleanupDelimeter = 13f;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHealthBar();

        if (!atWall && !atGhost)
        {
            MoveLeft();
        }
        if (this.gameObject.transform.position.x <= cleanupDelimeter) 
        { 
            cleanupFlag = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug log the collided object's tag
        //Debug.Log("Collided with: " + other.gameObject.tag);

        // Check what direction the collision is coming from
        Vector3 contactPoint = other.contacts[0].point;
        Vector3 center = GetComponent<Collider2D>().bounds.center;
        bool fromRight = contactPoint.x > center.x;

        // Pass through camera edge colliders
        if (other.gameObject.CompareTag("MainCamera") && !fromRight)
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }

        // Check for collision with Wall
        if (other.gameObject.CompareTag("Wall") && !fromRight)
        {
            atWall = true;
        }

        // Check for collision with another Enemy
        if (other.gameObject.CompareTag("Enemy") && !fromRight)
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

    //public void UpdateHealthBar()
    //{
    //    healthBar.fillAmount = health / 100f;
    //}
}
