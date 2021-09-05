using GameSystem.Player;
using UnityEngine;

namespace GameSystem.Enemies
{
    public class DamageDisherOnContact : MonoBehaviour
    {
        [SerializeField]
        private int _contactDamage;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerStatistics playerStats))
            {
                playerStats.GetDamaged(_contactDamage);

                if (this.GetComponent<EnemyBase>() == null) // if this script is not on a enemy...
                {
                    Destroy(this.gameObject); // destroy this (cuzz its most likely a bullet
                }
            }
        }
    }
}

