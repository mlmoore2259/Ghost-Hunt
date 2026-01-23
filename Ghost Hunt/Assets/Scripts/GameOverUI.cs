using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
