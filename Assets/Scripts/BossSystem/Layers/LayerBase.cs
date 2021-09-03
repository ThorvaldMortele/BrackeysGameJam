using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem 
{
    public class LayerBase : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed;

        public void FixedUpdate()
        {
            transform.Rotate(Vector3.up * (_rotationSpeed * Time.deltaTime));
        }
    }
}

