using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    //MenuManager + GameLoop are both working on scenes and game state

    public class GameLoop : MonoBehaviour
    {
        private bool _gameHasEnded = false;
        public float RestartDelay = 2f;

        public void EndGame()
        {
            if (!_gameHasEnded)
            {
                _gameHasEnded = true;
                Debug.Log("Game Over");
                Invoke("RestartGame", RestartDelay); //needs cleanup
            }            
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}