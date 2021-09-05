using ShootSystem;
using System.Collections;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupRampage : PickupBase
    {
        private float _newFireRate = 20f;

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
            SetRampage();

            ArmShoot.FireRate = _newFireRate;

            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(Duration);

            DeactivateShooting();

            Destroy(this.gameObject);
        }

        private void SetRampage()
        {
            ArmShoot.IsRampage = true;
            ArmShoot.IsTriple = false;
            ArmShoot.IsLaser = false;
        }
    }
}
