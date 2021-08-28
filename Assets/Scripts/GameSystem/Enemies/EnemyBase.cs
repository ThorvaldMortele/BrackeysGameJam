using UnityEngine;

namespace GameSystem.Enemies
{
    //Base class for the enemy information, to be inherited from

    //Can be put on prefabs and change the specific values


    public class EnemyBase : MonoBehaviour
    {
        public int HP;
        //public int MovementSpeed;
        public int EnemyScoreValue = 30;

        public ParticleSystem HitParticle;
        public ParticleSystem DeathParticle;

        private ScoreCalculation _scoreScript;

        private void Start()
        {
            _scoreScript = FindObjectOfType<ScoreCalculation>();
        }




        // for physics collisions
        public void TakeDamage(int amount, ContactPoint pointOfContact)
        {
            HP -= amount;

            // show blood (or sumfin alike)
            Instantiate(HitParticle, pointOfContact.point, Quaternion.identity);

            if (HP <= 0)
            {
                Instantiate(DeathParticle, pointOfContact.point, Quaternion.identity);
                _scoreScript.IncreaseScore(EnemyScoreValue);
                Die();              
            }              
        }
        // for rayCast hits
        public void TakeDamage(int amount, Vector3 hitPosition)
        {
            HP -= amount;

            // show blood (or sumfin alike)
            Instantiate(HitParticle, hitPosition, Quaternion.identity);

            if (HP <= 0)
            {
                Instantiate(DeathParticle, hitPosition, Quaternion.identity);
                _scoreScript.IncreaseScore(EnemyScoreValue);
                Die();            
            }               
        }




        private void Die()
        {
            Destroy(gameObject);            
        }
    }
}