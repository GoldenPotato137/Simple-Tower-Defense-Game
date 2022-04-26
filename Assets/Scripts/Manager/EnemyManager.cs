using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] private GameManager gameManager;
		private static int _countEnemyAlive;
		public Wave[] waves;
		[FormerlySerializedAs("START")] public Transform start;
		public float waveRate =3;
		public static List<GameObject> enemies;
		private Coroutine coroutine;

		void Start()
		{
			enemies = new List<GameObject>();
			coroutine = StartCoroutine(SpawnEnemy());
		}

		public void Stop()
		{
			StopCoroutine(coroutine);
		}

		public void RemoveEnemy(GameObject enemy)
		{
			enemies.Remove(enemy);
			_countEnemyAlive--;
		}
		
		IEnumerator SpawnEnemy()
		{
			foreach (Wave wave in waves)
			{
				for (int i = 0; i < wave.count; i++)
				{
					var temp = GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
					temp.transform.parent = transform;
					var enemy = temp.GetComponent<Enemy.Enemy>();
					enemy.manager = this;
					enemy.gameManager = gameManager;
					enemies.Add(temp);
					_countEnemyAlive++;
					if(i!=wave.count-1)
						yield return new WaitForSeconds(wave.rate);
				}
				while (_countEnemyAlive > 0)
				{
					yield return 0;
				}
				yield return new WaitForSeconds(waveRate);
			}
			
			while (_countEnemyAlive>0)
			{
				yield return 0;
			}
			// GameManager.Instance.Win();
		}
	
	}
}
