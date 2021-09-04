using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonBase : MonoBehaviour
    {
        public WaitForSeconds WaitForSeconds;

        [Header("ShootSettings")]
        
        public float CoolDown = 3f; //Cooldown for the cannon to shoot again

        public float BulletDecay = 12f;

        public float BulletSpeed = 15f; //Speed of instantiated gameObject

        public bool CanShoot = true; //Bool to (De)Activate the cannon      

        public void Awake()
        {
            WaitForSeconds = new WaitForSeconds(CoolDown);
        }

        public virtual void Start()
        {
            StartCoroutine(ShootDelay());
        }

        private IEnumerator ShootDelay()
        {
            for (; ; ) //Infinite for loop
            {
                ShootCannon(true);

                yield return WaitForSeconds;
            }
        }

        public void ShootCannon(bool CanShoot)
        {
            if (CanShoot)
            {
                var bullet = SpawnBullet();

                StartCoroutine(RemoveBullet(bullet, BulletDecay));
            }
        }

        public virtual GameObject SpawnBullet()
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

        private IEnumerator RemoveBullet(GameObject bullet, float timer)
        {
            yield return new WaitForSeconds(timer);

            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;
        }


        //public void ShootCannon(bool CanShoot) { }


        //If player is hit
        //PlayerMovement.JumpPlayer(0, -5)
        //just an assumption
    }
}
    



