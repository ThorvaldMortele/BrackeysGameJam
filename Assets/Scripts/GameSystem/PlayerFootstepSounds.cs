using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _clips;

    [SerializeField]
    private AudioSource _audioSource;

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        _audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return _clips[UnityEngine.Random.Range(0, _clips.Length)];
    }
}
