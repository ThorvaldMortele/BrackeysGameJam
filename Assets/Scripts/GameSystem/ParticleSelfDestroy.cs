using UnityEngine;

namespace GameSystem
{
    public class ParticleSelfDestroy : MonoBehaviour
    {
        [SerializeField]
        private float _lifeTime;

        void Start()
        {
            Destroy(this.gameObject, _lifeTime);
        }
    }
}