using UnityEngine;
using GameSystem.Enemies;
using System.Collections;
using System.Collections.Generic;
using GameSystem;

namespace ShootSystem
{
    public class ArmShoot : MonoBehaviour
    {
        [SerializeField]
        private Animator _characterAnimator;

        public string PLAYER_SHOOT = "Take_Shoot";

        public int LaserDamage = 12;
        public int LaserPushStrength = 100;
        public int LaserLength = 60;

        public float Range = 100f;
        public float FireRate = 10f;
        public float BulletSpeed = 100f;
        public float NextTimeToFire;

        public Camera FpsCam;
        public GameObject Bullet;

        public bool IsTriple;
        public bool IsRampage;
        public bool IsLaser;

        public LineRenderer LaserLine;

        [SerializeField]
        private PlayerSounds _playerSounds;

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1 / FireRate;

                if (IsTriple) ShootTriple();
                else if (IsLaser) ShootLaser();
                else ShootRegular();

                _playerSounds.PlaySoundEffect(4);

                ChangeAnimationState(PLAYER_SHOOT);
            }

            if (!IsLaser && LaserLine.GetPosition(1) != Vector3.zero)
            {
                LaserLine.SetPosition(0, Vector3.zero);
                LaserLine.SetPosition(1, Vector3.zero);
            }
        }

        public IEnumerator RemoveBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(2f);

            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;
        }

        public virtual void ShootRegular()
        {
            LaserLine.SetPosition(0, Vector3.zero);
            LaserLine.SetPosition(1, Vector3.zero);
            var bullet = SpawnBullet();
           
            StartCoroutine(RemoveBullet(bullet));
        }

        public void ShootTriple()
        {
            LaserLine.SetPosition(0, Vector3.zero);
            LaserLine.SetPosition(1, Vector3.zero);

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

            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(RemoveBullet(bullets[i]));
            }
        }

        private void ShootLaser()
        {
            RaycastHit hit;

            LaserLine.SetPosition(0, this.transform.position);
            LaserLine.SetPosition(LaserLine.positionCount - 1, transform.position + FpsCam.transform.forward * LaserLength);

            LaserLine.startColor = Color.red;
            LaserLine.endColor = Color.red;

            // rayCast damage (might work for a laser or sumfin)
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, Range))
            {
                GameObject go = hit.transform.gameObject;
                if (go != null && go.tag == "Enemy")
                {
                    if (go.TryGetComponent(out EnemyBase enemyB))
                    {
                        enemyB.TakeDamage(LaserDamage, hit.point, LaserPushStrength);
                    }
                    else if (go.TryGetComponent(out EnemyFlyer enemyF))
                    {
                        enemyF.TakeDamage(LaserDamage, hit.point, LaserPushStrength);
                    }
                }
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