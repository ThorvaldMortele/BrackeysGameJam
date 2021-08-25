using ShootSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootSystem
{
    public class ArmShoot : MonoBehaviour
    {
        public float Damage = 10f;
        public float Range = 100f;
        public float FireRate = 10f;
        public float NextTimeToFire;

        public Camera FpsCam;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }

        private void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, Range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(Damage);
                    Debug.Log("aw");
                }
            }
        }
    }
}