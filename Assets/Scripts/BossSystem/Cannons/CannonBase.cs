using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonBase : MonoBehaviour
    {
        [Header("ShootSettings")]
        [SerializeField]
        private float _cooldown = 3f;

        [SerializeField]
        private float _bulletSpeed = 5f;

        [SerializeField]
        private bool _canShoot = true;

        [SerializeField]
        private GameObject _bullet;

        public void Update()
        {
            ShootCannon(_canShoot);
        }

        public virtual void ShootCannon(bool CanShoot) { }

    }
}
    



