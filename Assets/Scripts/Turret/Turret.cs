using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Turret
{
    public abstract class Turret : MonoBehaviour 
    {
        protected List<GameObject> enemies;
        [SerializeField] private bool faceEnemy;
        [SerializeField] private float attackRateTime = 1;//多少秒攻击一次
        [SerializeField] protected float fireRange; //射程
        [SerializeField] protected int damage;//伤害
        private float timer = 0;
        public bool isOn = true; //炮塔是否启用
        public GameObject bulletPrefab;//子弹
        public Transform firePosition;
        // public Transform head;
        
        protected abstract bool Attack();
        
        void Start()
        {
            timer = attackRateTime;
        }

        protected GameObject GetNearestEnemy()
        {
            enemies ??= EnemyManager.enemies;
            if (enemies == null || enemies.Count == 0) return null;
            GameObject ans = enemies[0];
            foreach (var enemy in enemies)
                if ((enemy.transform.position - transform.position).magnitude < (transform.position - ans.transform.position).magnitude)
                    ans = enemy;
            return ans;
        }
        
        void  Update()
        {
            enemies ??= EnemyManager.enemies;
            if (isOn == false) return;
            if (faceEnemy && GetNearestEnemy()!=null)
            {
                var target = GetNearestEnemy();
                var position = transform.position;
                transform.Rotate(0,0,Vector2.SignedAngle(firePosition.position - position,
                    target.transform.position - position));
            }
            timer += Time.deltaTime;
            if (enemies.Count>0 && timer >=attackRateTime)
            {
                if(Attack())
                    timer = 0;
            }
        }
        
        // void OnTriggerEnter(Collider col)
        // {
        //     if (col.tag =="Enemy")
        //     {
        //         enemys.Add(col.gameObject);
        //     }
        //
        // }
        // void OnTriggerExit(Collider col)
        // {
        //     if (col.tag =="Enemy")
        //     {
        //         enemys.Remove (col.gameObject);
        //     }
        // }
        //
    }
}
