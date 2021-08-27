using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        var mainMenuIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(mainMenuIndex + 1); //Loads the GameScene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
