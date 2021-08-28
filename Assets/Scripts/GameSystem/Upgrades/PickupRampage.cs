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
            if (other.CompareTag("Player"))
            {
                StartCoroutine(ApplyEffect());
            }
        }

        private IEnumerator ApplyEffect()
        {
            ArmShoot.FireRate = _newFireRate;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(_duration);

            ArmShoot.FireRate = 10f;

            Destroy(this.gameObject);
        }
    }
}
