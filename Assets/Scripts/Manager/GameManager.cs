using Turret;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
	public class GameManager : MonoBehaviour 
	{
		[SerializeField] private EnemyManager enemyManager;
		[SerializeField] private UiManager uiManager;
		[SerializeField] private Flag flag;
		[SerializeField] private int hp; //血量
		public GameObject endUI;
		public Text endMessage;
		public static bool isPause;
		public static int money = 350; //当前金钱数
		
		void Awake()
		{
			Resume();
			flag.SetMaxHealth(hp);
			//Instance = this;
			//enemySpawner = GetComponent<EnemySpawner>();
		}
		
		public static void Resume()
		{
			Time.timeScale = 1f;
			isPause = false;
		}

		public static void Pause()
		{
			Time.timeScale = 0f;
			isPause = true;
		}
		
		public void ChangeMoney(int change = 0)
		{
			money += change;
			uiManager.FlushMoney();
		}

		public void ChangeHp(int x)
		{
			hp += x;
			flag.SetHealth(hp);
			if (hp <= 0)
				 Failed();
		}
		
		public void Win()
		{
			Pause();
			uiManager.ShowWinMenu();
		}
	
		public void Failed()
		{
			Pause();
			uiManager.ShowFailedMenu();
		}
	
		public void OnButtonRetry()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void OnButtonMenu()
		{
			SceneManager.LoadScene(0);
		}

	}
}
