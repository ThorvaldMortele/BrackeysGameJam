using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonLob : CannonBase
    {
        public override void Start()
        {
            StartCoroutine(ShootTiming());
        }

        private IEnumerator ShootTiming()
        {
            for (; ; )
            {
                var bullet = SpawnBullet();

                yield return WaitForSeconds;

                RemoveBullet(bullet);
            }
        }

        // Cannon that shoots parabollic
        public override GameObject SpawnBullet()
        {
            var bullet = ObjectPoolBoss.Instance.GetPooledObject(); //Just fill in the enemy object pool class
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position + transform.up;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
            }

            var bulletRB = bullet.GetComponent<Rigidbody>();

            bulletRB.velocity = Vector3.zero;  // This fixed the shooting (bullets still had an original velocity on them, thats why they shot amiss)
            bulletRB.useGravity = true;
            bulletRB.AddForce(transform.up * BulletSpeed, ForceMode.Impulse);

            return bullet;
        }

        private void RemoveBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;

        }
    }
}