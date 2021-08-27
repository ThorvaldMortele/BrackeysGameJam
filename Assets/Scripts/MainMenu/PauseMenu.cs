using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    
    [SerializeField]
    private GameObject _pauseMenuUI;

    private Scene _mainMenu;

    private void Start()
    {
        _mainMenu = SceneManager.GetSceneByBuildIndex(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("Resume Game");
    }

    private void PauseGame()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Freeze the game while paused
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Pause Game");
    }

    public void LoadMenu() //Go to the menu
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(_mainMenu.name);
        SceneManager.LoadScene("StartMenuScene");

        Debug.Log("Load Menu");
    }

    private void ExitGame() //Close the application
    {
        Application.Quit();
    }
}
