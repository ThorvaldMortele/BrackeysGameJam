using UnityEngine;
using GameSystem.Enemies;

namespace ShootSystem
{
    public class ArmShoot : MonoBehaviour
    {
        public int Damage = 10;
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
                GameObject go = hit.transform.gameObject;
                if (go != null && go.tag == "Enemy")
                {
                    go.GetComponent<EnemyBase>().TakeDamage(Damage);

                    Debug.Log("Enemy");
                }
            }
        }
    }
}