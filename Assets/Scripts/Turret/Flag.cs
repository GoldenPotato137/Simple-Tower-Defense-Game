using UI;
using UnityEngine;

namespace Turret
{
    public class Flag : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;

        public void SetMaxHealth(float num)
        {
            healthBar.SetMaxHealth(num);
        }
        
        public void SetHealth(float per)
        {
            healthBar.SetHealth(per);
        }
    }
}