using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    
    [SerializeField]
    private GameObject _pauseMenuUI;

    [SerializeField]
    private GameObject _gameOverUI;

    [SerializeField]
    private GameObject _gameWonUI;

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

        //Debug.Log("Resume Game");
    }

    private void PauseGame()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Freeze the game while paused
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;

        //Debug.Log("Pause Game");
    }

    public void LoadMenu() //Go to the menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenuScene");

        //Debug.Log("Load Menu");
    }

    public void WonGame() //Called when the player won the game
    {
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

    private void ExitGame() //Close the application
    {
        Application.Quit();
    }
}
