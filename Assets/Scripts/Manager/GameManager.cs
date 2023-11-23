using System.Threading.Tasks;
using Enums;
using Helper;
using Turret;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : ManagerBase
    {
        public static GameManager Instance;
        private EnemyManager _enemyManager;
        private BuildManager _buildManager;
        private UiManager _uiManager;

        private Flag _flag;
        private bool _gaming;
        private int _hp = 10; //血量
        public static bool IsPause;
        public static int Money = 350; //当前金钱数

        public GameManager()
        {
            EventBus.Register(Events.LevelStart, GameStart);
            EventBus.Register(Events.LevelQuit, GameEnd);
            EventBus.Register(Events.UIPausePushed, Pause);
            EventBus.Register(Events.UIResumePushed, Resume);
        }

        public override void Update()
        {
            if (_gaming == false) return;
            _enemyManager.Update();
            _buildManager.Update();
        }

        public override void Stop()
        {
            _enemyManager.Stop();
            _enemyManager = null;
            _buildManager.Stop();
            _buildManager = null;
        }

        private async void GameStart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            await Task.Delay(50);
            _enemyManager = new EnemyManager(this);
            _uiManager = FindObjectOfType<UiManager>();
            _buildManager = new BuildManager(this, _uiManager);

            _flag = FindObjectOfType<Flag>();
            _flag.SetMaxHealth(_hp);
            
            Resume();
            _gaming = true;
        }

        private void GameEnd()
        {
            _gaming = false;
            Stop();
            SceneManager.LoadScene(0);
        }

        private static void Resume()
        {
            Time.timeScale = 1f;
            IsPause = false;
        }

        private static void Pause()
        {
            Time.timeScale = 0f;
            IsPause = true;
        }

        public void ChangeMoney(int change = 0)
        {
            Money += change;
            _uiManager.FlushMoney();
        }

        public void ChangeHp(int x)
        {
            _hp += x;
            _flag.SetHealth(_hp);
            if (_hp <= 0)
                Failed();
        }

        public void Win()
        {
            Pause();
            _uiManager.ShowWinMenu();
        }
        
        private void Failed()
        {
            Pause();
            _uiManager.ShowFailedMenu();
        }
    }
}