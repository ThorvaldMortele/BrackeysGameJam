using GameSystem.Enemies;
using ShootSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupTriple : PickupBase
    {
        private float _duration = 3f;

        public override void OnTriggerEnter(Collider other)
        {
            if (!ArmShoot.IsRampage && !ArmShoot.IsTriple)
            {
                if (other.CompareTag("Player"))
                {
                    StartCoroutine(ApplyEffect());
                }
            }
            else return;
        }

        private IEnumerator ApplyEffect()
        {
            SetTriple();

            ArmShoot.FireRate = 6f;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(_duration);

            DeactivateShooting();

            Destroy(this.gameObject);
        }

        private void SetTriple()
        {
            ArmShoot.IsTriple = true;
            ArmShoot.IsRampage = false;
        }

        private void DeactivateShooting()
        {
            ArmShoot.IsRampage = false;
            ArmShoot.IsTriple = false;
            ArmShoot.FireRate = 10f;
        }
    }
}
