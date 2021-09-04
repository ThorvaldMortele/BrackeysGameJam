using BossSystem;
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
    [SerializeField]
    private Transform _initialTransform;

    public bool IsTargetActive = true;
    private WaitForSeconds _waitForSeconds;

    public LayerBase Layerbase;

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
        if (_targetHealth == 0 && IsTargetActive)
        {
            _originalMaterial.sharedMaterial = _inactiveMaterial;
            _targetHealth = _maxTargetHealth;
            IsTargetActive = false;
            Layerbase.TargetBools.Add(this.IsTargetActive);
        }
    }

    private IEnumerator DelayFlip()
    {
        for (; ; )
        {
            if (IsTargetActive)
            {
                transform.Rotate(Vector3.right, 180);

                yield return _waitForSeconds;

                transform.rotation = _initialTransform.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTargetActive)
        {
            _targetHealth--;
        }
    }
}
