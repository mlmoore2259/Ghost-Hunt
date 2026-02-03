using UnityEngine;
using UnityEngine.InputSystem;

public class SpiritCleanupTrigger : MonoBehaviour
{
    public GameObject interactText;
    [SerializeField] private PlayerGameInfo playerGameInfo;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            interactText.GetComponent<TMPro.TextMeshProUGUI>().text = "[E]";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Keyboard.current.eKey.IsPressed() && (playerGameInfo.possessionLvl > 0f))
        {
            LowerPossessionLvl();
        }
    }

    void LowerPossessionLvl()
    {
        playerGameInfo.possessionLvl -= 0.01f * playerGameInfo.spiritCleanupHealth;
    }
}
