using UnityEngine;

namespace GameSystem.Player
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

