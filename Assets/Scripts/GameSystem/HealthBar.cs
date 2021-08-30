using UnityEngine;
using UnityEngine.UI;

namespace GameSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetMaxHealth(float health)
        {
            _slider.maxValue = health;
            _slider.value = health;
        }

        public void SetHealth(float health)
        {
            _slider.value = health;
        }
    }
}
