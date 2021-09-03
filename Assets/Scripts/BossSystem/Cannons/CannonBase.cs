using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonBase : MonoBehaviour
    {
        [Header("ShootSettings")]
        
        public float CoolDown = 3f; //Cooldown for the cannon to shoot again
        
        public GameObject Bullet; //Bullet gameObject that will be instantiated

        [SerializeField]
        private float _bulletSpeed = 5f; //Speed of instantiated gameObject

        [SerializeField]
        private bool _canShoot = true; //Bool to (De)Activate the cannon      

        public virtual void Update()
        {
            ShootCannon(_canShoot);
        }

        public virtual void ShootCannon(bool CanShoot) { }


        //If player is hit
        //PlayerMovement.JumpPlayer(0, -5)
        //just an assumption
    }
}
    



