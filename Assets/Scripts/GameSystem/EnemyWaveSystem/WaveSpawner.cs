using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.EnemyWaveSystem
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    };

    [System.Serializable]
    public class Wave
    {
        public string name; //Name of the wave
        public Transform enemy; //The enemy prefab to be spawned
        public int count; //The amount of enemies
        public float rate; //Spawn rate
    }

    public class WaveSpawner : MonoBehaviour
    {
        //https://youtu.be/Vrld13ypX_I

        [SerializeField]
        private GameObject _waveCompletedText;
        [SerializeField]
        private Text _waitTimeText; 


        public Wave[] waves;
        private int _index = 0; //Index of the wave
               
        [SerializeField]
        private Transform[] _spawnPoints;

        public float timeBetweenWaves = 5f;
        public float waveCountDown;

        private float searchCountdown = 1f; //Used in check if the enemies are still alive

        private SpawnState _state = SpawnState.Counting; //Default state set to "Counting"

        [SerializeField]
        private PauseMenu _gameUI;

        private void Start()
        {
            _waveCompletedText.SetActive(false);

            waveCountDown = timeBetweenWaves;    
        }

        private void Update()
        {
            if(_state == SpawnState.Waiting)
            {                
                if(!EnemyIsAlive()) //Check if player killed all enemies
                {
                    WinGame();
                    BeginNewWave();
                }
                else
                {
                    return; //Enemies are still alive
                }
            }

            if(waveCountDown <= 0)
            {
                if (_state != SpawnState.Spawning) //Check if already spawning waves
                {
                    StartCoroutine(SpawnWave(waves[_index]));

                    //Spawning wave
                }
            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }
        }

        private void WinGame()
        {
            //HARD CODED -> waves restart automatically else this wouldn't get called
            //Still has to be fixed

            if (_index >= 6) //To make sure the last wave is still played
            {
                _gameUI.WonGame();
            }
        }

        private void BeginNewWave()
        {
            ShowWaveCompletedText(true);

            _state = SpawnState.Counting;
            waveCountDown = timeBetweenWaves;

            RestartIndex();
        }

        private void RestartIndex()
        {
            if (_index + 1 > waves.Length - 1) //automatically resets
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
        }

        private bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if(searchCountdown <=0f)
            {
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                    return false;
            }
            return true;
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            ShowWaveCompletedText(false);

            _state = SpawnState.Spawning;
            
            for(int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate); //Wait
            }

            _state = SpawnState.Waiting; //Wait on the player


            yield break;
        }

        private void SpawnEnemy(Transform enemy)
        {
            //Spawn enemy
            
            Transform spawnpoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Instantiate(enemy, spawnpoint.position, spawnpoint.rotation);    
        }

        #region Game UI
        private void ShowWaveCompletedText(bool activated)
        {
            if(activated)
            {
                _waveCompletedText.SetActive(true);
                _waitTimeText.text = "Next wave incoming !"; //WaveCountDown doesn't have the right value here, 
            }
            else
            {
                _waveCompletedText.SetActive(false);
            }
        }
        #endregion
    }
}