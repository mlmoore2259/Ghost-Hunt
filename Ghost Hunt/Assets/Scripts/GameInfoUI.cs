using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInfoUI : MonoBehaviour
{
    public GameObject possessionText;
    public GameObject cleanupHealthText;
    public GameObject scoreText;
    public GameObject coinText;
    public GameObject timeText;
    public GameObject healthText;
    public PlayerGameInfo playerGameInfo;
    private GameObject player;
    public int dayLengthInSeconds;
    public string time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    void Awake()
    {
        dayLengthInSeconds = 180;
        time = CalculateDayTime();
    }

    // Update is called once per frame
    void Update()
    {
        time = CalculateDayTime();
        if (playerGameInfo.possessionLvl >= 100f)
        {
            // Everything should be displayed as an int
            possessionText.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
            possessionText.GetComponent<TMPro.TextMeshProUGUI>().text = " Possession Level: " + playerGameInfo.possessionLvl.ToString("F0") + "%";
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = " Ghosts Defeated: " + playerGameInfo.score;
            coinText.GetComponent<TMPro.TextMeshProUGUI>().text = " Coins: " + playerGameInfo.coins;
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = " " + time;
            healthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Health: " + playerGameInfo.health.ToString("F0");
            cleanupHealthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Spiritcleanup: " + playerGameInfo.spiritCleanupHealth + "%";
        }
        else if (playerGameInfo.health > 0)
        {
            possessionText.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
            possessionText.GetComponent<TMPro.TextMeshProUGUI>().text = " Possession Level: " + playerGameInfo.possessionLvl.ToString("F0") + "%";
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = " Ghosts Defeated: " + playerGameInfo.score;
            coinText.GetComponent<TMPro.TextMeshProUGUI>().text = " Coins: " + playerGameInfo.coins;
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = " " + time;
            healthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Health: " + playerGameInfo.health;
            cleanupHealthText.GetComponent<TMPro.TextMeshProUGUI>().text = " Spiritcleanup: " + playerGameInfo.spiritCleanupHealth.ToString("F0") + "%";
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
            playerGameInfo.health = 0f;
            Destroy(player);
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    string CalculateDayTime()
    {
        // Every 180 seconds is a full day cycle
        // Ratio of real:game time is 180:86400 = 1:480
        // Game starts at 6:00 AM

        // Get game time in seconds
        float gameTime = Time.timeSinceLevelLoad;
        float dayTimeInSeconds = (gameTime % dayLengthInSeconds) + 45;

        // Convert to hours and minutes
        int totalGameSeconds = Mathf.FloorToInt(dayTimeInSeconds * 480);
        int hours = (totalGameSeconds / 3600) % 24;
        int minutes = (totalGameSeconds / 60) % 60;
        return string.Format("{0:D2}:{1:D2}", hours, minutes);
    }
}
