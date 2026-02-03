using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject statsText;
    [SerializeField] PlayerGameInfo playerGameInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        statsText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Stats\n" +
            "Ghosts Defeated: " + playerGameInfo.score + "\n\n" +
            "Coins Collected: " + playerGameInfo.coins + "\n\n" +
            "Days Survived: " + playerGameInfo.daysSurvived;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Quit to desktop
    public void QuitToDesktop()
    {
        Application.Quit();
    }

    // Restart game
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameMap");
    }
}
