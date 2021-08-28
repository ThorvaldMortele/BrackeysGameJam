using GameSystem.Enemies;
using ShootSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupTriple : PickupBase
    {

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ApplyEffect();
            }
        }

        private void ApplyEffect()
        {
            //FireRate = 4f;
        }

        

    }
}
