using GameSystem.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootSystem
{
    public class BulletPhysicsDamager : MonoBehaviour
    {
        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _pushStrength;

        private HitSounds _hitSounds;

        private void Start()
        {
            _hitSounds = FindObjectOfType<HitSounds>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyBase enemyBase))
            {
                enemyBase.TakeDamage(_damage, collision.GetContact(0), collision.relativeVelocity.normalized, _pushStrength);

                _hitSounds.InstantiateSound(this.transform.position);

                this.gameObject.SetActive(false);
            }
        }
    }
}

