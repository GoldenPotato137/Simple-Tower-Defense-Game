using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxHealth(float max)
        {
            slider.maxValue = max;
            slider.value = max;
        }

        public void SetHealth(float per)
        {
            slider.value = per;
        }
    }
}