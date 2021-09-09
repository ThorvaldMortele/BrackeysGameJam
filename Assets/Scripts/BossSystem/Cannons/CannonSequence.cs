using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    // Cannon that shoots a sequence of bullets
    public class CannonSequence : CannonBase
    {
        [Header("Sequence Cannon: ")]
        public WaitForSeconds SequenceCoolDown; //Cooldown between bullets of the sequence




        //Quick copy
        //still needs to be changed (mostly deleted)
        public override void Start()
        {
            SequenceCoolDown = new WaitForSeconds(1f);

            StartCoroutine(ShootDelay());
        }

        public override IEnumerator ShootDelay()
        {
            for (; ; ) //Infinite for loop
            {
                ShootCannon(true);

                yield return WaitForSeconds;
            }
        }

        public override void ShootCannon(bool CanShoot)
        {
            if (CanShoot)
            {
                //Tripple

                TippleBullet();





            }
        }

        public IEnumerator TippleBullet()
        {
            for (var shootCount = 0; shootCount <= 3; shootCount++)
            {
                var bullet = SpawnBullet();

                StartCoroutine(RemoveBullet(bullet, BulletDecay));
            }

            yield return SequenceCoolDown;
        }

        public override GameObject SpawnBullet(/*int shootcount*/)
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
            bulletRB.AddForce(transform.up * BulletSpeed, ForceMode.Impulse);

            return bullet;
        }

        public override IEnumerator RemoveBullet(GameObject bullet, float timer)
        {
            yield return new WaitForSeconds(timer);

            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;
        }
    }
}