using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject endUI;
	public Text endMessage;
	public static GameManager Instance;
	private EnemySpawner enemySpawner;
	void Awake()
    {
  //       Instance = this;
		// enemySpawner = GetComponent<EnemySpawner>();
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
