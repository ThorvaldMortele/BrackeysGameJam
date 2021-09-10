using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonDownwards : CannonBase
    {
        // cannon that aims at the player and shoot straight
        //gets locked in rotation right before shooting

        private float _timer;

        [SerializeField]
        private Transform Turret;

        public override void Start()
        {
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _timer += Time.deltaTime;
                if (_timer < 2f)
                {
                    Turret.LookAt(other.transform);
                }

                if (_timer >= 2f)
                {
                    _timer = 0;
                    ShootCannon(true);
                }
            }
        }

        public override void ShootCannon(bool CanShoot)
        {
            if (CanShoot)
            {
                var bullet = SpawnBullet();

                StartCoroutine(RemoveBullet(bullet, BulletDecay));
            }
        }

        public override GameObject SpawnBullet()
        {
            var bullet = ObjectPoolBoss.Instance.GetPooledObject(); //Just fill in the enemy object pool class
            if (bullet != null)
            {
                bullet.transform.position = Turret.position + Turret.up;
                bullet.transform.rotation = Turret.rotation;
                bullet.SetActive(true);
            }

            var bulletRB = bullet.GetComponent<Rigidbody>();

            bulletRB.velocity = Vector3.zero;  // This fixed the shooting (bullets still had an original velocity on them, thats why they shot amiss)
            bulletRB.useGravity = false;
            bulletRB.AddForce(Turret.forward * BulletSpeed, ForceMode.Impulse);

            return bullet;
        }
    }
}