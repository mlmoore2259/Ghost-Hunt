using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebuildWall : MonoBehaviour
{
    public GameObject interactText;
    public WallSection wallSection;
    public PlayerGameInfo playerGameInfo;
    public GameObject physicalWall;
    [SerializeField] private int coins;
    [SerializeField] bool broken;
    [SerializeField] private bool inRebuildTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        // Get wall child object
        wallSection = this.gameObject.transform.GetChild(0).gameObject.GetComponent<WallSection>();
        physicalWall = this.gameObject.transform.GetChild(0).gameObject;
    }
    
    void Awake()
    {
        inRebuildTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        coins = playerGameInfo.coins;
        broken = wallSection.broken;
        // Rebuild when in rebuildTrigger and press 'r'
        if (inRebuildTrigger && Keyboard.current.rKey.wasPressedThisFrame && coins >= 10 && broken)
        {
            Rebuid();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRebuildTrigger = true;
            interactText.SetActive(true);
            interactText.GetComponent<TMPro.TextMeshProUGUI>().text = "[R] (10 Coins)";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRebuildTrigger = false;
            interactText.SetActive(false);
        }
    }

    void Rebuid()
    {
        physicalWall.SetActive(true);
        playerGameInfo.coins -= 10;
        wallSection.health = 100;
        wallSection.broken = false;
    }
}