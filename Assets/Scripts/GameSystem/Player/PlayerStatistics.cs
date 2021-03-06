using System.Collections;
using UnityEngine;

namespace GameSystem.Player
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int Health = 251;
        [SerializeField]
        private GameObject HealthBar;

        public bool IsAlive;

        private PlayerSounds _playerSounds;

        [SerializeField]
        private Camera _mainCam;

        [SerializeField]
        private GameObject _deathCameraPivot;

        [SerializeField]
        private GameObject _gameOverUI;


        private void Start()
        {
            IsAlive = true;

            HealthBar.GetComponent<HealthBar>().SetMaxHealth(251);

            _playerSounds = GetComponent<PlayerSounds>();
        }

        public void GetDamaged(int damage)
        {
            if (IsAlive)
            {
                Health -= damage;

                HealthBar.GetComponent<HealthBar>().SetHealth(Health);
                if (Health > 0)
                {
                    _playerSounds.PlaySoundEffect(0);
                }
                else if (Health <= 0)
                {
                    StartCoroutine(PlayerDies());
                }
            }
        }

        private IEnumerator PlayerDies()
        {
            _playerSounds.PlaySoundEffect(1);

            _deathCameraPivot.gameObject.SetActive(true);
            _mainCam.gameObject.SetActive(false);

            IsAlive = false;

            yield return new WaitForSeconds(5f);

            //Activate the Game Over UI
            _gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

