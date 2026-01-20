using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerGameInfo playerGameInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerGameInfo.coins += 1;
            Destroy(gameObject);
        }
    }
}
