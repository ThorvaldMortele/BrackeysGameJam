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

        public GameObject RampagePickup;
        private List<PickupBase> _pickups;

        private void Awake()
        {
            _pickups = FindObjectsOfType<PickupBase>().ToList();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _pickupSpawnRate)
            {
                _pickups = FindObjectsOfType<PickupBase>().ToList();
                if (_pickups.Count <= _maxPickupCount)
                {
                    SpawnPickup();
                    _timer = 0;
                }
            }
        }

        private void SpawnPickup()
        { 
            var spawnPos = new Vector3(
                UnityEngine.Random.Range(-_pickupSpawnRange, _pickupSpawnRange),
                1.25f,
                UnityEngine.Random.Range(-_pickupSpawnRange, _pickupSpawnRange));

            var go = Instantiate(RampagePickup, spawnPos, Quaternion.identity);

            go.GetComponent<PickupRampage>().ArmShoot = FindObjectOfType<ArmShoot>();
        }
    }
}
