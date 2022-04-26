using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Manager
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private static readonly int Flicker = Animator.StringToHash("Flicker");
        public Text moneyText;
        public Animator moneyAnimator;
        
        /// <summary>
        /// 刷新金钱显示
        /// </summary>
        public void FlushMoney()
        {
            moneyText.text = "$" + GameManager.money;
        }

        public void ShowNoEnoughMoney()
        {
            moneyAnimator.SetTrigger(Flicker);
        }
        
        public void Resume()
        {
            pauseMenu.SetActive(false);
            GameManager.Resume();
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);
            GameManager.Pause();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.isPause)
                    Resume();
                else
                    Pause();
            }
        }
    }
}