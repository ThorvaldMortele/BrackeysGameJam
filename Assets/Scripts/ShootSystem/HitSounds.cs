using UnityEngine;

namespace ShootSystem
{
    public class HitSounds : MonoBehaviour
    {
        public AudioClip[] SoundEffects;

        public GameObject HitSoundPrefab;


        public void InstantiateSound(Vector3 position)
        {
            var randomSoundInt = UnityEngine.Random.Range(0, SoundEffects.Length);
            var randomSoundObject = Instantiate(HitSoundPrefab, position, Quaternion.identity);

            Destroy(randomSoundObject, 1f);

            randomSoundObject.GetComponent<AudioSource>().PlayOneShot(SoundEffects[randomSoundInt]);
        }
    }
}
