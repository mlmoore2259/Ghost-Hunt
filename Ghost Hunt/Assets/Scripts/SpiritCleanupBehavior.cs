using UnityEngine;

public class SpiritCleanupBehavior : MonoBehaviour
{
    [SerializeField] PlayerGameInfo playerGameInfo;
    private float decreaseAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    private void Awake()
    {
        decreaseAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameInfo.spiritCleanupHealth <= 0f)
        {
            playerGameInfo.spiritCleanupHealth = 0f;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            InvokeRepeating("UpdateHealth", 0f, 3f);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CancelInvoke("UpdateHealth");
        }
    }

    void UpdateHealth()
    {
        playerGameInfo.spiritCleanupHealth -= decreaseAmount;
    }
}
