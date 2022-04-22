using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
	public abstract class Enemy : MonoBehaviour 
	{
		/// waypoint数组
		private Transform[] wayPoints;
		/// 敌人移动速度
		public float speed = 1;
		/// 当前所到waypoint的下标
		private int index = 0;
		public EnemyManager manager;
			
		void Start()
		{
			wayPoints = WayPointManager.positions;
		}
	
		void Update()
		{
			Move();
		}
	
		void Move()
		{
			if (index > wayPoints.Length - 1) return;
			transform.Translate((wayPoints[index].position - transform.position).normalized * (Time.deltaTime * speed));
			if (Vector3 .Distance(wayPoints[index].position,transform.position)<0.2f)
			{
				index++;
			}
			if (index >wayPoints.Length-1)
			{
				ReachDestination();
			}
		}
	
		protected abstract void DestroyOperate();
		void ReachDestination()
		{
			DestroyOperate();
			manager.RemoveEnemy(gameObject);
			GameObject.Destroy(this.gameObject);
		}
	
		void OnDestroy()
		{
			;
		}
	}
}