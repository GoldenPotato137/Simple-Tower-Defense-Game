using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
	public class GameManager : MonoBehaviour 
	{
		[SerializeField] private EnemyManager enemyManager;
		[SerializeField] private UiManager uiManager;
		public GameObject endUI;
		public Text endMessage;
		public static bool isPause;
		public static int money = 350; //当前金钱数
		
		void Awake()
		{
			//       Instance = this;
			// enemySpawner = GetComponent<EnemySpawner>();
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
		
		public void Win()
		{
			// endUI.SetActive(true);
			// endMessage.text = "胜利";

		}
	
		public void Failed()
		{
			// enemySpawner.Stop();
			// 	endUI.SetActive(true);
			// 	endMessage.text = "失败";
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
