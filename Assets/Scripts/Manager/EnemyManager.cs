using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
	public class EnemyManager : MonoBehaviour 
	{
		public static int countEnemyAlive;
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
			countEnemyAlive--;
		}
		
		IEnumerator SpawnEnemy()
		{
			foreach (Wave wave in waves)
			{
				for (int i = 0; i < wave.count; i++)
				{
					var temp = GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
					temp.transform.parent = transform;
					var component = temp.GetComponent<Enemy.Enemy>();
					component.manager = this;
					enemies.Add(temp);
					countEnemyAlive++;
					if(i!=wave.count-1)
						yield return new WaitForSeconds(wave.rate);
				}
				while (countEnemyAlive > 0)
				{
					yield return 0;
				}
				yield return new WaitForSeconds(waveRate);
			}
			
			while (countEnemyAlive>0)
			{
				yield return 0;
			}
			// GameManager.Instance.Win();
		}
	
	}
}
