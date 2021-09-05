using ShootSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.Upgrades
{
    public class PickupManager : MonoBehaviour
    {
        [Header("PickupSettings")]
        [SerializeField]
        private float _pickupSpawnRange = 40f;
        [SerializeField]
        private float _pickupSpawnRate = 3;
        [SerializeField]
        private float _maxPickupCount = 15;

        private float _timer;
        
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
                    var randomNr = UnityEngine.Random.Range(0, 3);
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

            var spawnPos = new Vector3(
                UnityEngine.Random.Range(-_pickupSpawnRange, _pickupSpawnRange),
                15,
                UnityEngine.Random.Range(-_pickupSpawnRange, _pickupSpawnRange));

            if (Physics.Raycast(spawnPos, -transform.up, out RaycastHit hit, 50f, PickupSpawnCheck))
            {
                var go = Instantiate(pickup, hit.point + _offset, Quaternion.identity);

                go.GetComponent<PickupBase>().ArmShoot = FindObjectOfType<ArmShoot>();
            }
        }
    }
}
