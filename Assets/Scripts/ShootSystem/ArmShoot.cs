using UnityEngine;
using GameSystem.Enemies;
using System.Collections;

namespace ShootSystem
{
    public class ArmShoot : MonoBehaviour
    {
        [SerializeField]
        private Animator _characterAnimator;

        const string PLAYER_SHOOT = "Take_Shoot";

        public int Damage = 10;
        public float Range = 100f;
        public float FireRate = 10f;
        public float BulletSpeed = 100f;
        public float NextTimeToFire;

        public Camera FpsCam;
        public GameObject Bullet;

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1 / FireRate;
                Shoot();

                ChangeAnimationState(PLAYER_SHOOT);
            }
        }


        IEnumerator RemoveBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(2f);

            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;
        }

        private void Shoot()
        {
            RaycastHit hit;

            var bullet = ObjectPool.Instance.GetPooledObject();  
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position + transform.up;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
            }

            var bulletRB = bullet.GetComponent<Rigidbody>();

            bulletRB.velocity = Vector3.zero;  // This fixed the shooting (bullets still had an original velocity on them, thats why they shot amiss)
            bulletRB.AddForce(transform.up * BulletSpeed, ForceMode.Impulse);


            // rayCast damage (might work for a laser or sumfin)
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, Range))
            {              
                GameObject go = hit.transform.gameObject;
                if (go != null && go.tag == "Enemy")
                {
                    go.GetComponent<EnemyBase>().TakeDamage(Damage, hit.point);

                    Debug.Log("Enemy");
                }

            }

            StartCoroutine(RemoveBullet(bullet));
        }



        void ChangeAnimationState(string newState)
        {
            // play the animation
            _characterAnimator.Play(newState, 2, 0); // 2 is layer in animator, second 0 is setting animation at 0 seconds (restart)
        }
    }
}