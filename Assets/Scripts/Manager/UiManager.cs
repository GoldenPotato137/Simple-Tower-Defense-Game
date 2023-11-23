using Enums;
using Helper;
using Turret;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject upgradeMenu;
        [SerializeField] private GameObject addMoney;
        [SerializeField] private GameObject failedMenu;
        [SerializeField] private GameObject winMenu;
        private static readonly int Flicker = Animator.StringToHash("Flicker");
        public Text moneyText;
        public Text upgradePrice, deletePrice;
        public Animator moneyAnimator;
        
        /// <summary>
        /// 刷新金钱显示
        /// </summary>
        public void FlushMoney()
        {
            moneyText.text = "$" + GameManager.Money;
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

        /// <summary>
        /// 在指定位置播放加金钱*动画*
        /// </summary>
        /// <param name="pos">位置</param>
        /// <param name="money">钱数</param>
        public void PopAddMoney(Vector3 pos,int money)
        {
            GameObject temp = Instantiate(addMoney, new Vector3(pos.x, pos.y, pos.z - 1), Quaternion.identity,
                transform);
            temp.GetComponent<AddMoney>().SetMoney(money);
        }

        /// <summary>
        /// 显示失败菜单
        /// </summary>
        public void ShowFailedMenu()
        {
            failedMenu.SetActive(true);
        }
        
        /// <summary>
        /// 显示胜利菜单
        /// </summary>
        public void ShowWinMenu()
        {
            winMenu.SetActive(true);
        }
        
        public void Resume()
        {
            pauseMenu.SetActive(false);
            EventBus.Trigger(Events.UIResumePushed);
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);
            EventBus.Trigger(Events.UIPausePushed);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.IsPause)
                    Resume();
                else
                    Pause();
            }
        }
        
        public void OnButtonRetry()
        {
            EventBus.Trigger(Events.LevelStart);
        }

        public void OnButtonMenu()
        {
            EventBus.Trigger(Events.LevelQuit);
        }

        public void OnSelectAx()
        {
            EventBus.Trigger(Events.UISelectTurret, TurretType.Ax);
        }
        
        public void OnSelectBow()
        {
            EventBus.Trigger(Events.UISelectTurret, TurretType.Bow);
        }
        
        public void OnSelectLeafGrass()
        {
            EventBus.Trigger(Events.UISelectTurret, TurretType.LeafGrass);
        }

        public void OnUpdatedPushed() => EventBus.Trigger(Events.UIUpgradePushed);
        
        public void OnSellPushed() => EventBus.Trigger(Events.UISellPushed);
    }
}