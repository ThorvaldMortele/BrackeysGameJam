using ShootSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupRampage : PickupBase
    {
        private float _duration = 3f;
        private float _newFireRate = 20f;

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
            SetRampage();

            ArmShoot.FireRate = _newFireRate;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(_duration);

            ArmShoot.FireRate = 10f;

            DeactivateShooting();

            Destroy(this.gameObject);
        }

        private void DeactivateShooting()
        {
            ArmShoot.IsRampage = false;
            ArmShoot.IsTriple = false;
        }

        private void SetRampage()
        {
            ArmShoot.IsRampage = true;
            ArmShoot.IsTriple = false;
        }

    }
}
