using UnityEngine;

namespace GameSystem.EnemyWaveSystem
{
    public class SpawnCollider : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), this.gameObject.GetComponent<BoxCollider>(), true);
            }
        }
    }
}