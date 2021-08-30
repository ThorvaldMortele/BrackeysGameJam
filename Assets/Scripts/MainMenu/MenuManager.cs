using UnityEngine;
using UnityEngine.SceneManagement;

//Script to be called to pull up different menus/scenes
public class MenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField]
    private GameObject _gameMenuUI;

    [SerializeField]
    private GameObject _pauseMenuUI;

    [SerializeField]
    private GameObject _gameOverUI;

    [SerializeField]
    private GameObject _gameWonUI;


    #region Game Scene
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void ResumeGame()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PauseGame()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Freeze the game while paused
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu() //Go to the main menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenuScene");
    }

    public void WonGame() //Called when the player won the game
    {
        Debug.Log("You won");
        _gameMenuUI.SetActive(false);
        _gameWonUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
    }

    private void LostGame() //Called when the player lost the game
    {
        _gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
    }
    #endregion


    #region Main Menu
    public void PlayGame()
    {
        //var mainMenuIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameScene"); //Loads the GameScene
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }
}
