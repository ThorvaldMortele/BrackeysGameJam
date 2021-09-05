using GameSystem;
using GameSystem.Enemies;
using GameSystem.Player;
using System.Collections;
using UnityEngine;

namespace ShootSystem
{
    public class BulletPhysicsDamager : MonoBehaviour
    {
        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _pushStrength;
        [SerializeField]
        private ParticleSystem _hitParticle;
        [SerializeField]
        private ParticleSystem _deathParticle;

        private HitSounds _hitSounds;

        private void Awake()
        {
            _hitSounds = FindObjectOfType<HitSounds>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyBase enemyBase))
            {
                enemyBase.TakeDamage(_damage, collision.GetContact(0), collision.relativeVelocity.normalized, _pushStrength, _hitParticle, _deathParticle);

                _hitSounds.InstantiateSound(this.transform.position);

                this.gameObject.SetActive(false);
            }
            else if (collision.gameObject.TryGetComponent(out EnemyFlyer enemyFlyer))
            {
                enemyFlyer.TakeDamage(_damage, collision.GetContact(0), collision.relativeVelocity.normalized, _pushStrength);

                _hitSounds.InstantiateSound(this.transform.position);

                this.gameObject.SetActive(false);
            }
            else if (collision.gameObject.TryGetComponent(out PlayerStatistics playerStats))
            {
                playerStats.GetDamaged(_damage);

                _hitSounds.InstantiateSound(this.transform.position);

                this.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(DeactivateBullet());
            }
        }

        private IEnumerator DeactivateBullet()
        {
            yield return new WaitForSeconds(0.1f);
            this.gameObject.SetActive(false);
        }
    }
}

