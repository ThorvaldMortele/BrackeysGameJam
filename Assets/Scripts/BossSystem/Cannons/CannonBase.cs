using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonBase : MonoBehaviour
    {
        [Header("ShootSettings")]
        [SerializeField]
        private float _cooldown = 3f; //Cooldown for the cannon to shoot again
        
        [SerializeField]
        private GameObject _bullet; //Bullet gameObject that will be instantiated

        [SerializeField]
        private float _bulletSpeed = 5f; //Speed of instantiated gameObject

        [SerializeField]
        private bool _canShoot = true; //Bool to (De)Activate the cannon      

        public void Update()
        {
            ShootCannon(_canShoot);
        }

        public virtual void ShootCannon(bool CanShoot) { }


        //If player is hit
        //PlayerMovement.JumpPlayer(0, -5)
        //just an assumption
    }
}
    



