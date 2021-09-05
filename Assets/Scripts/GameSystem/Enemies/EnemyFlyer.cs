using GameSystem.Score;
using MoveSystem;
using UnityEngine;

namespace GameSystem.Enemies
{
    public class EnemyFlyer : MonoBehaviour
    {
        [SerializeField]
        private Transform _bulletSpawn;

        [SerializeField]
        private GameObject _bullet;

        private PlayerMovement _player;

        [SerializeField]
        private float _minTimer, _maxTimer;

        private float _shootTimeLimit;
        private float _timer;

        [SerializeField]
        private float _shotSpeed;

        private Rigidbody _rigidBody;

        public int HP;
        public int EnemyScoreValue = 30;

        public ParticleSystem HitParticle;
        public ParticleSystem DeathParticle;

        private ScoreCalculation _scoreScript;

        public bool CanGetShoved;

        private void Start()
        {
            _scoreScript = FindObjectOfType<ScoreCalculation>();
            _rigidBody = GetComponent<Rigidbody>();

            //_enemyMovement.GetComponent<EnemyMovement>();
            _player = FindObjectOfType<PlayerMovement>();

            _shootTimeLimit = Random.Range(_minTimer, _maxTimer);
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _shootTimeLimit)
            {
                Shoot();
                _timer = 0;

                _shootTimeLimit = Random.Range(_minTimer, _maxTimer);
            }
        }

        void Shoot()
        {
            var playerLocation = _player.transform.position;
            var enemyLocation = transform.position;

            var shootDir = (playerLocation - enemyLocation).normalized;

            var bullet = Instantiate(_bullet, _bulletSpawn.position, Quaternion.identity);
            var bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.AddForce(shootDir * _shotSpeed, ForceMode.Impulse);
        }

        //For physics collisions
        public void TakeDamage(int amount, ContactPoint pointOfContact, Vector3 bulletDirection, int pushStrength)
        {
            HP -= amount;

            GetShoved(bulletDirection, pushStrength);

            //Show blood (or sumfin alike)
            Instantiate(HitParticle, pointOfContact.point, Quaternion.identity);

            if (HP <= 0)
            {
                Instantiate(DeathParticle, pointOfContact.point, Quaternion.identity);
                _scoreScript.IncreaseScore(EnemyScoreValue);
                Die();
            }
        }

        //For rayCast hits
        public void TakeDamage(int amount, Vector3 hitPosition, int pushStrength)
        {
            HP -= amount;

            GetShoved(pushStrength);

            //Show blood (or sumfin alike)
            Instantiate(HitParticle, hitPosition, Quaternion.identity);

            if (HP <= 0)
            {
                Instantiate(DeathParticle, hitPosition, Quaternion.identity);
                _scoreScript.IncreaseScore(EnemyScoreValue);
                Die();
            }
        }

        public void GetShoved(int pushStrength) //This one is for eventual raycast hits 
        {
            if (CanGetShoved)
            {
                _rigidBody.AddForce(-this.transform.forward * pushStrength, ForceMode.Impulse);
            }
        }

        public void GetShoved(Vector3 bulletDir, int pushStrength)
        {
            if (CanGetShoved)
            {
                _rigidBody.AddForce(-bulletDir * pushStrength, ForceMode.Impulse);
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

