using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    //MenuManager + GameLoop are both working on scenes and game state

    public class GameLoop : MonoBehaviour
    {
        private bool gameHasEnded = false;
        public float restartDelay = 2f;

        public void EndGame()
        {
            if(gameHasEnded == false)
            {
                gameHasEnded = true;
                Debug.Log("Game Over");
                Invoke("RestartGame", restartDelay);
            }            
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}