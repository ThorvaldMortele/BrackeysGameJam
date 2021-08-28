using ShootSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupManager : MonoBehaviour
    {
        private float _pickupSpawnRange = 40f;
        private float _timer;
        private float _pickupSpawnRate = 3;
        private float _maxPickupCount = 15;

        public List<GameObject> Pickups;
        public LayerMask PickupSpawnCheck;
        private Vector3 _offset = new Vector3(0, 2, 0);

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _pickupSpawnRate)
            {
                var pickups = FindObjectsOfType<PickupBase>().ToList();
                if (pickups.Count <= _maxPickupCount)
                {
                    var randomNr = UnityEngine.Random.Range(0, 2);
                    if (Pickups.Count != 0)
                    {
                        var pickup = Pickups[randomNr];

                        SpawnPickup(pickup);
                        _timer = 0;
                    }
                }
                else return;
            }
        }

        private void SpawnPickup(GameObject pickup)
        {
            RaycastHit hit;

            var spawnPos = new Vector3(
                UnityEngine.Random.Range(-_pickupSpawnRange, _pickupSpawnRange),
                15,
                UnityEngine.Random.Range(-_pickupSpawnRange, _pickupSpawnRange));

            if (Physics.Raycast(spawnPos, -transform.up, out hit, 50f, PickupSpawnCheck))
            {
                var go = Instantiate(pickup, hit.point + _offset, Quaternion.identity);

                go.GetComponent<PickupBase>().ArmShoot = FindObjectOfType<ArmShoot>();
            }
        }
    }
}
