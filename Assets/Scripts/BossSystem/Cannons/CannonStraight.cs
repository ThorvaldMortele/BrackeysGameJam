using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonStraight : CannonBase
    {
        //Lower layer cannon that shoots straight

        WaitForSeconds WaitForSeconds;

        public void Awake()
        {
            WaitForSeconds = new WaitForSeconds(CoolDown);
        }

        public void Start()
        {
            StartCoroutine(ShootDelay());
        }

        public void ShootCannon(bool CanShoot)
        {
            Debug.Log("test");


            //instantiate bullet with bulletspeed

            SpawnBullet();

            //add force
        }

        private GameObject SpawnBullet()
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

        private IEnumerator ShootDelay()
        {
            for(; ; ) //Infinite for loop
            {
                ShootCannon(true);

                yield return WaitForSeconds;
            }            
        }
    }
}
