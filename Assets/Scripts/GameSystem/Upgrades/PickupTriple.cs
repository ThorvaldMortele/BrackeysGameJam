using ShootSystem;
using System.Collections;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupTriple : PickupBase
    {
        
        public override void OnTriggerEnter(Collider other)
        {
            if (!ArmShoot.IsRampage && !ArmShoot.IsTriple && !ArmShoot.IsLaser)
            {
                if (other.CompareTag("Player"))
                {
                    PickupSoundScript.PlaySoundEffect(0);
                    StartCoroutine(ApplyEffect());
                }
            }
            else return;
        }

        private IEnumerator ApplyEffect()
        {
            SetTriple();

            ArmShoot.FireRate = 6f;

            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(Duration);

            DeactivateShooting();

            Destroy(this.gameObject);
        }

        private void SetTriple()
        {
            ArmShoot.IsTriple = true;
            ArmShoot.IsRampage = false;
            ArmShoot.IsLaser = false;
        }
    }
}
