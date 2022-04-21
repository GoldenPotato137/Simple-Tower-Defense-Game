using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public static int CountEnemyAlive = 0;
	public Wave[] waves;
	public Transform START;
	public float waveRate =3;
	private Coroutine coroutine;


	void Start()
	{
		coroutine = StartCoroutine(SpawmEnemy());
	}

	 public void Stop()
    {
		StopCoroutine(coroutine);
    }
	IEnumerator SpawmEnemy()
    {
		foreach (Wave wave in waves)
		{
			for (int i = 0; i < wave.count; i++)
			{
				GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
				CountEnemyAlive++;
				if(i!=wave.count-1)
				yield return new WaitForSeconds(wave.rate);
			}
			while (CountEnemyAlive > 0)
			{
				yield return 0;
			}
			yield return new WaitForSeconds(waveRate);
		}
		while (CountEnemyAlive>0)
        {
			yield return 0;

        }
		// GameManager.Instance.Win();
    }
	
}
