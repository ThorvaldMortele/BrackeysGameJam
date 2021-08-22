using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootSystem
{
    public class Target : MonoBehaviour
    {
        public float Health = 50f;

        public void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health <= 0f) Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
