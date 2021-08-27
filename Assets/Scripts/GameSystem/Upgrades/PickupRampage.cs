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
            ArmShoot.FireRate = 20f;

            StartCoroutine(ResetFireRate());
        }

        private IEnumerator ResetFireRate()
        {
            yield return new WaitForSeconds(1f);

            ArmShoot.FireRate = 10f;

            Destroy(this.gameObject);
        }
    }
}
