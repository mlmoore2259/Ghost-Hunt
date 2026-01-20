using UnityEngine;

public class SpiritCleanupTrigger : MonoBehaviour
{
    [SerializeField] private bool inTrigger;
    [SerializeField] private PlayerGameInfo playerGameInfo;
    [SerializeField] private bool playerPossessed;
    [SerializeField] private bool depossessing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    private void Awake()
    {
        inTrigger = false;
        playerPossessed = false;
        depossessing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameInfo.possessionLvl >= 100f)
        {
            playerPossessed = true;
        }
        else
        {
            playerPossessed = false;
        }
        if (playerGameInfo.possessionLvl == 0)
        {
            depossessing = false;
        }

        if (inTrigger && (playerPossessed || depossessing))
        {
            LowerPossessionLvl();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    void LowerPossessionLvl()
    {
        playerGameInfo.possessionLvl -= 0.001f * playerGameInfo.spiritCleanupHealth;
    }
}
