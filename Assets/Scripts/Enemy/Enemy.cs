using Manager;
using UI;
using UnityEngine;

namespace Enemy
{
	public abstract class Enemy : MonoBehaviour 
	{
		private Transform[] wayPoints; //waypoint数组
		private int index; //当前所到waypoint的下标
		public float speed = 1; //敌人移动速度
		public int hp;//怪物血量
		public HealthBar healthBar;
		public EnemyManager manager;
			
		void Start()
		{
			healthBar.SetMaxHealth(hp);
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

		void Killed()
		{
			KilledOperate();
			manager.RemoveEnemy(gameObject);
			GameObject.Destroy(this.gameObject);
		}
		
		internal void Damaged(int damage)
		{
			hp -= damage;
			healthBar.SetHealth(hp);
			DamagedOperate(damage);
			if (hp <= 0)
				Killed();
		}
	
		/// 到达目的地时执行操作
		protected abstract void ReachTargetOperate();

		/// 被击杀时执行操作
		protected abstract void KilledOperate();

		/// 收到伤害时执行操作
		protected abstract void DamagedOperate(int damage);
		
		void ReachDestination()
		{
			ReachTargetOperate();
			manager.RemoveEnemy(gameObject);
			GameObject.Destroy(this.gameObject);
		}
	}
}