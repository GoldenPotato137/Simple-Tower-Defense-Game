using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Turret
{
    public class BulletLeaf : MonoBehaviour 
    {
        [NonSerialized] public int damage = 10;
        public float speed = 2;
        public GameObject explosionEffectPrefab;
        private float distanceArriveTarget = 1f;
        public float aliveTime = 2f;//子弹存活时间
        public float deltaTime = 1f; //造成伤害的时间间隔
        public float rotationSpeed = 720f;
        private float flyTime;
        private List<GameObject> enemies;

        GameObject GetNearestEnemy()
        {
            enemies = EnemyManager.Enemies;
            if (enemies == null || enemies.Count == 0) return null;
            GameObject ans = enemies[0];
            foreach (var enemy in enemies)
                if ((enemy.transform.position - transform.position).magnitude < (transform.position - ans.transform.position).magnitude)
                    ans = enemy;
            return ans;
        }

        private float cntTime;
        void Update()
        {
            flyTime += Time.deltaTime;
            if (flyTime >= aliveTime) //飞行时间过长，子弹自爆
            {
                Die();
                return;
            }
            //计算飞行航向并飞向目标
            var temp = GetNearestEnemy();
            var heading = Vector3.zero;
            if (temp != null)
            {
                heading = temp.transform.position - transform.position;
                heading.z = 0;
                heading = heading.normalized;
            }
            transform.position += heading * Time.deltaTime * speed;
            //自旋
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
            //对在范围内的目标造成伤害
            cntTime += Time.deltaTime;
            if (enemies == null || cntTime < deltaTime) return; //没有敌人或攻击冷却中
            cntTime = 0;
            List<GameObject> toKill = new List<GameObject>();
            foreach (var target in EnemyManager.Enemies)
                if((target.transform.position - transform.position).magnitude< distanceArriveTarget)
                    toKill.Add(target);
            foreach (var target in toKill)
                target.GetComponent<Enemy.Enemy>().Damaged(damage);
        }
        
        void Die()
        {
            var transform1 = transform;
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform1.position, transform1.rotation);
            Destroy(effect, 1);
            Destroy(this.gameObject);
        }
    }
}