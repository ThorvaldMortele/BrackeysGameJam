using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BossSystem
{
    public class ObjectPoolBoss : MonoBehaviour
    {
        public static ObjectPoolBoss Instance;
        public List<GameObject> PooledObjects;
        public GameObject ObjectToPool;
        public int AmountToPool;

        private void Awake()
        {
            Instance = this;

            PooledObjects = new List<GameObject>();
            GameObject tmp;

            for (int i = 0; i < AmountToPool; i++)
            {
                tmp = Instantiate(ObjectToPool, this.gameObject.transform);
                tmp.SetActive(false);
                PooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject() //Possibly a problem with the deactivation
        {
            for (int i = 0; i < AmountToPool; i++)
            {
                if (!PooledObjects[i].activeInHierarchy)
                {
                    return PooledObjects[i]; //"Het werkt" - Dizz 2021
                }
            }
            return null;
        }
    }
}
