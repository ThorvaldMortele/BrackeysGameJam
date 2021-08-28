using UnityEngine;
using GameSystem.Enemies;
using System.Collections;
using System.Collections.Generic;

namespace ShootSystem
{
    public class ArmShoot : MonoBehaviour
    {
        [SerializeField]
        private Animator _characterAnimator;

        public string PLAYER_SHOOT = "Take_Shoot";

        public int Damage = 10;
        public float Range = 100f;
        public float FireRate = 10f;
        public float BulletSpeed = 100f;
        private float _spreadAngle = 3f;
        public float NextTimeToFire;

        public Camera FpsCam;
        public GameObject Bullet;

        public bool IsTriple;
        public bool IsRampage;

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1 / FireRate;

                if (IsTriple) ShootTriple();
                else ShootRampage();

                ChangeAnimationState(PLAYER_SHOOT);
            }
        }


        public IEnumerator RemoveBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(2f);

            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;
        }

        public virtual void ShootRampage()
        {
            RaycastHit hit;

            var bullet = SpawnBullet();
            
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

        public void ShootTriple()
        {
            RaycastHit hit;

            var bullets = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                var bullet = ObjectPool.Instance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = this.transform.position + transform.up;
                    bullet.transform.rotation = this.transform.rotation * Quaternion.Euler((-1 + i) * 8, 0, 0); 
                    bullet.SetActive(true);
                }

                var bulletRB = bullet.GetComponent<Rigidbody>();

                bulletRB.velocity = Vector3.zero;  // This fixed the shooting (bullets still had an original velocity on them, thats why they shot amiss)
                bulletRB.AddForce(bullet.transform.up * BulletSpeed, ForceMode.Impulse);

                bullets.Add(bullet);
            }

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

            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(RemoveBullet(bullets[i]));
            }
        }

        private GameObject SpawnBullet()
        {
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

            return bullet;
        }

        

        public void ChangeAnimationState(string newState)
        {
            // play the animation
            _characterAnimator.Play(newState, 2, 0); // 2 is layer in animator, second 0 is setting animation at 0 seconds (restart)
        }
    }
}