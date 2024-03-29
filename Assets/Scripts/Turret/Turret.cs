﻿using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Turret
{
    public abstract class Turret : MonoBehaviour 
    {
        protected List<GameObject> enemies;
        [SerializeField] private bool faceEnemy;
        [SerializeField] private float attackRateTime = 1;//多少秒攻击一次
        [SerializeField] protected float fireRange; //射程
        [SerializeField] protected int damage;//伤害
        private float timer;
        public bool isOn = true; //炮塔是否启用
        public GameObject bulletPrefab;//子弹
        public List<Transform> firePosition;
        // public Transform head;
        
        protected abstract bool Attack();
        
        void Start()
        {
            timer = attackRateTime;
        }

        protected GameObject GetNearestEnemy()
        {
            enemies ??= EnemyManager.Enemies;
            if (enemies == null || enemies.Count == 0) return null;
            GameObject ans = enemies[0];
            foreach (var enemy in enemies)
                if ((enemy.transform.position - transform.position).magnitude < (transform.position - ans.transform.position).magnitude)
                    ans = enemy;
            return ans;
        }
        
        void  Update()
        {
            enemies ??= EnemyManager.Enemies;
            if (isOn == false) return;
            if (faceEnemy && GetNearestEnemy()!=null && (GetNearestEnemy().transform.position-transform.position).magnitude <= fireRange)
            {
                var target = GetNearestEnemy();
                var position = transform.position;
                transform.Rotate(0, 0, Time.deltaTime * 10 * Vector2.SignedAngle(firePosition[0].position - position,
                    target.transform.position - position));
            }
            timer += Time.deltaTime;
            if (enemies.Count>0 && timer >=attackRateTime)
            {
                if(Attack())
                    timer = 0;
            }
        }
    }
}
