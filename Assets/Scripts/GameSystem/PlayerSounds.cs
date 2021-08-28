using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class PlayerSounds : MonoBehaviour
    {
        private AudioSource _source;

        [SerializeField]
        private AudioClip[] _soundEffects;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        // for most if it's sounds...
        public void PlaySoundEffect(int soundNumber)
        {
            _source.PlayOneShot(_soundEffects[soundNumber]);
        }
    }
}

