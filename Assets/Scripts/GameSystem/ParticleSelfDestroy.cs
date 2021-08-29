using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestroy : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime;

    void Start()
    {
        Destroy(this.gameObject, _lifeTime);
    }

}
