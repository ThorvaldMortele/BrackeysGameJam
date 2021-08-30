using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //GOING TO BE REPLACED BY MenuManager

    public void PlayGame()
    {
        //var mainMenuIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameScene"); //Loads the GameScene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
