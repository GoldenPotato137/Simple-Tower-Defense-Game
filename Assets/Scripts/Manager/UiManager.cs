using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject upgradeMenu;
        private static readonly int Flicker = Animator.StringToHash("Flicker");
        public Text moneyText;
        public Text upgradePrice, deletePrice;
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

        /// <summary>
        /// 在某个坐标旁边显示升级菜单
        /// </summary>
        /// <param name="pos">坐标</param>
        /// <param name="uPrice">升级价格</param>
        /// <param name="dPrice">删除返现</param>
        public void ShowUpgradeMenu(Vector3 pos,int uPrice,int dPrice)
        {
            upgradePrice.text = uPrice.ToString();
            deletePrice.text = dPrice.ToString();
            upgradeMenu.transform.position = new Vector3(pos.x + 0.8f, pos.y, pos.z - 1);
            upgradeMenu.SetActive(true);
        }

        /// <summary>
        /// 隐藏升级菜单
        /// </summary>
        public void HideUpgradeMenu()
        {
            upgradeMenu.SetActive(false);
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