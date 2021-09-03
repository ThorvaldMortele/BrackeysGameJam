using UnityEngine;

namespace GameSystem.Enemies
{
    //Base class for the enemy information, to be inherited from

    //Can be put on prefabs and change the specific values
    
    public class EnemyBase : MonoBehaviour
    {
        private Rigidbody _rigidBody;

        public int HP;
        //public int MovementSpeed;
        public int EnemyScoreValue = 30;

        public ParticleSystem HitParticle;
        public ParticleSystem DeathParticle;

        private ScoreCalculation _scoreScript;

        public bool CanGetShoved;

        private void Awake()
        {
            _scoreScript = FindObjectOfType<ScoreCalculation>();
            _rigidBody = GetComponent<Rigidbody>();
        }


        // for physics collisions
        public void TakeDamage(int amount, ContactPoint pointOfContact, Vector3 bulletDirection, int pushStrength
            , ParticleSystem hitParticle, ParticleSystem deathParticle)
        {
            HP -= amount;

            GetShoved(bulletDirection, pushStrength);

            // show blood (or sumfin alike)


            Instantiate(hitParticle, pointOfContact.point, Quaternion.identity);
            
            

            if (HP <= 0)
            {
                Instantiate(deathParticle, pointOfContact.point, Quaternion.identity);
                _scoreScript.IncreaseScore(EnemyScoreValue);
                Die();              
            }              
        }

        // for rayCast hits
        public void TakeDamage(int amount, Vector3 hitPosition, int pushStrength)
        {
            HP -= amount;

            GetShoved(pushStrength);

            // show blood (or sumfin alike)
            Instantiate(HitParticle, hitPosition, Quaternion.identity);

            if (HP <= 0)
            {
                Instantiate(DeathParticle, hitPosition, Quaternion.identity);
                _scoreScript.IncreaseScore(EnemyScoreValue);
                Die();            
            }               
        }

        public void GetShoved(int pushStrength) // this one is for eventual raycast hits 
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