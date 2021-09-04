using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonLob : CannonBase
    {
        private float _animation;

        private GameObject _bullet;

        public override void Start()
        {
            StartCoroutine(ShootTiming());
        }

        private void Update()
        {
            if (_bullet != null)
            {
                _animation += Time.deltaTime;

                _animation = _animation % 5;

                _bullet.transform.position = MathParabole.Parabola(this.transform.position, Vector3.up * 20, 5f, _animation / 5f);
            }
        }

        private IEnumerator ShootTiming()
        {
            for (; ; )
            {
                _bullet = SpawnBullet();

                yield return new WaitForSeconds(6f);

                StartCoroutine(RemoveBullet(_bullet, 2));
                _bullet = null;
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

            return bullet;
        }

        private IEnumerator RemoveBullet(GameObject bullet, float timer)
        {
            yield return new WaitForSeconds(timer);

            bullet.SetActive(false);
            bullet.transform.position = this.transform.position + transform.up;
            bullet.transform.rotation = this.transform.rotation;
        }
    }
}