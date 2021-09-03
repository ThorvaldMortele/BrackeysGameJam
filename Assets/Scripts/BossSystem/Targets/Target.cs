using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float _targetHealth = 10;
    [SerializeField]
    private float _maxTargetHealth = 10;
    [SerializeField]
    private float _flipCooldown = 8;
    [SerializeField]
    private MeshRenderer _originalMaterial;
    [SerializeField]
    private Material _inactiveMaterial;

    private bool _isTargetActive = true;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_flipCooldown);
    }

    private void Start()
    {
        
        StartCoroutine(DelayFlip());
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_targetHealth == 0 && _isTargetActive)
        {
            _originalMaterial.sharedMaterial = _inactiveMaterial;
            _targetHealth = _maxTargetHealth;
        }
    }

    private IEnumerator DelayFlip()
    {
        for (; ; )
        {
            transform.Rotate(Vector3.up, 180);

            yield return _waitForSeconds;

            transform.Rotate(Vector3.up, 180);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTargetActive)
        {
            _targetHealth--;
        }
    }
}
