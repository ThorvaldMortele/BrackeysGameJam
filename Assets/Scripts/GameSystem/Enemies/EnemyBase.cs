using UnityEngine;

namespace GameSystem.Enemies
{
    //Base class for the enemy information, to be inherited from

    //Can be put on prefabs and chenge the specific values

    public class EnemyBase : MonoBehaviour
    {
        public int HP;
        //public int MovementSpeed;

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP <= 0)                 
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);            
        }
    }
}