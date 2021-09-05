using ShootSystem;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupBase : PickupManager
    {
        public ArmShoot ArmShoot;
        public float Duration = 3f;

        public PickupSound PickupSoundScript;

        public void DeactivateShooting()
        {
            ArmShoot.IsRampage = false;
            ArmShoot.IsTriple = false;
            ArmShoot.IsLaser = false;

            ArmShoot.FireRate = 10f;
        }

        public virtual void OnTriggerEnter(Collider other) { }
    }
}
