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

            var bulletSpawnPosition = this.transform.position + (transform.forward + new Vector3(0, 5, 0));

            Instantiate(Bullet, bulletSpawnPosition, Quaternion.identity);

            
            //coroutine wait cooldown
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
