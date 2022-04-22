using System.Collections;
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
		private Coroutine coroutine;


		void Start()
		{
			coroutine = StartCoroutine(SpawnEnemy());
		}

		public void Stop()
		{
			StopCoroutine(coroutine);
		}
		
		IEnumerator SpawnEnemy()
		{
			foreach (Wave wave in waves)
			{
				for (int i = 0; i < wave.count; i++)
				{
					var temp = GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
					temp.transform.parent = transform;
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
