using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSound : MonoBehaviour
{
    private AudioSource _source;

    [SerializeField]
    private AudioClip[] _soundEffects;

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // for most of it's sounds...
    public void PlaySoundEffect(int soundNumber)
    {
        _source.PlayOneShot(_soundEffects[soundNumber]);
    }
}
