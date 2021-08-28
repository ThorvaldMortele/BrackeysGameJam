using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int Health = 251;
        [SerializeField]
        private GameObject HealthBar;

        public bool IsAlive;

        private PlayerSounds _playerSounds;


        private void Start()
        {
            HealthBar.GetComponent<HealthBar>().SetMaxHealth(251);

            _playerSounds = GetComponent<PlayerSounds>();
        }

        public void GetDamaged(int damage)
        {
            Health -= damage;

            HealthBar.GetComponent<HealthBar>().SetHealth(Health);

            if (Health > 0)
            {
                _playerSounds.PlaySoundEffect(1);
            }
            else if (Health <= 0)
            {
                StartCoroutine(PlayerDies());
            }
        }

        private IEnumerator PlayerDies()
        {
            _playerSounds.PlaySoundEffect(2);

            //_deathCamera.SetActive(true);
            //_deathCamera.AddComponent<AudioListener>();
            //_deathCamera.transform.SetParent(null);
            //_mainCam.gameObject.SetActive(false);

            IsAlive = false;

            yield return new WaitForSeconds(5f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // is currently reloading the same scene

        }
    }
}

