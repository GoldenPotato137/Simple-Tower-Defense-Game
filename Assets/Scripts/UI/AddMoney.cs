using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AddMoney : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private Text text;
        
        private void OnEnable()
        {
            Destroy(gameObject,0.5f);
        }

        private void Update()
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }

        public void SetMoney(int money)
        {
            text.text = money.ToString();
        }
    }
}