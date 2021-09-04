using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem 
{
    public class LayerBase : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed;

        public List<bool> TargetBools = new List<bool>();

        public void FixedUpdate()
        {
            transform.Rotate(Vector3.up * (_rotationSpeed * Time.deltaTime));
            
            if (!TargetBools.Contains(true))
            {
                //CanShoot = false;

                //add to list of all targets or smth
                //its to start the second wave
            }
        }
    }
}

