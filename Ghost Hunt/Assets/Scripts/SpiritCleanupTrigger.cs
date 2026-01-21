using UnityEngine;
using UnityEngine.InputSystem;

public class SpiritCleanupTrigger : MonoBehaviour
{
    [SerializeField] private bool inTrigger;
    [SerializeField] private PlayerGameInfo playerGameInfo;
    [SerializeField] private bool playerPossessed; // player has >= 100 possession level
    [SerializeField] private bool depossessing; // player is in trigger and actively lowering level
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Keyboard.current.eKey.IsPressed() && playerGameInfo.possessionLvl >= 100f)
        {
            LowerPossessionLvl();
        }
    }

    void LowerPossessionLvl()
    {
        playerGameInfo.possessionLvl -= 0.001f * playerGameInfo.spiritCleanupHealth;
    }
}
