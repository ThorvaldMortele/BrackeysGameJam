using System.Collections;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupLaser : PickupBase
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
            SetLaser();

            ArmShoot.FireRate = 20f;

            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(Duration);

            DeactivateShooting();

            Destroy(this.gameObject);
        }

        private void SetLaser()
        {
            ArmShoot.IsLaser = true;
            ArmShoot.IsRampage = false;
            ArmShoot.IsTriple = false;
        }
    }
}
