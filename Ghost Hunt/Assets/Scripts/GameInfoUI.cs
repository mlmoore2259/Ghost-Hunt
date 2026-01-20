using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    public GameObject possessionText;
    public GameObject cleanupHealthText;
    public GameObject scoreText;
    public GameObject coinText;
    public GameObject timeText;
    public GameObject healthText;
    public GameObject gameOverPanel;
    public PlayerGameInfo playerGameInfo;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        gameOverPanel = GameObject.Find("GameOverPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameInfo.possessionLvl >= 100f)
        {
            possessionText.GetComponent<TMPro.TextMeshProUGUI>().text = " Possession Level: " + playerGameInfo.possessionLvl;
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "YOU ARE POSSESSED!";
            coinText.GetComponent<TMPro.TextMeshProUGUI>().text = "YOU ARE POSSESSED!";
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = "YOU ARE POSSESSED!";
            healthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Health: " + playerGameInfo.health;
            cleanupHealthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Spirit cleanup Health: " + playerGameInfo.spiritCleanupHealth + "%";
        }
        else if (playerGameInfo.health > 0)
        {
            possessionText.GetComponent<TMPro.TextMeshProUGUI>().text = " Possession Level: " + playerGameInfo.possessionLvl;
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = " Ghosts Defeated: " + playerGameInfo.score;
            coinText.GetComponent<TMPro.TextMeshProUGUI>().text = " Coins: " + playerGameInfo.coins;
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = " " + Mathf.FloorToInt(Time.timeSinceLevelLoad) + "s";
            healthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Health: " + playerGameInfo.health;
            cleanupHealthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Spiritcleanup Health: " + playerGameInfo.spiritCleanupHealth + "%";
        }
        if (playerGameInfo.health <= 0f)
        {
            // Deactivate other UI elements
            //possessionText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            //scoreText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            //coinText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            //timeText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            //healthText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            //cleanupHealthText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;

            // Activate game over panel
            gameOverPanel.SetActive(true);

            Destroy(player);
        }
    }
}
