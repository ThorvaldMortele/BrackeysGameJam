using ShootSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupRampage : PickupBase
    {
        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(ApplyEffect());
            }
        }

        private IEnumerator ApplyEffect()
        {
            ArmShoot.FireRate = 20f;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(3f);

            ArmShoot.FireRate = 10f;

            Destroy(this.gameObject);
        }
    }
}
