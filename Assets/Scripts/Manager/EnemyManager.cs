using System.Collections;
using System.Collections.Generic;
using Helper;
using UnityEngine;
using Coroutine = Helper.Coroutine;

namespace Manager
{
    public class EnemyManager : ManagerBase
    {
        private readonly GameManager _gameManager;
        private readonly UiManager _uiManager;
        private static int _countEnemyAlive;

        public static readonly List<GameObject> Enemies = new();
        private readonly Coroutine _coroutine;

        public EnemyManager(GameManager game)
        {
            _gameManager = game;
            _uiManager = FindObjectOfType<UiManager>();

            _coroutine = CoroutineManager.Start(SpawnEnemy());
        }
        
        public override void Update()
        {
        }

        public override void Stop()
        {
            CoroutineManager.Stop(_coroutine);
        }

        public void RemoveEnemy(GameObject enemy, bool isKilled)
        {
            Enemies.Remove(enemy);
            if (isKilled) //击杀：生成金钱动画、添加金钱
            {
                var temp = enemy.GetComponent<Enemy.Enemy>();
                _gameManager.ChangeMoney(temp.money);
                _uiManager.PopAddMoney(enemy.transform.position, temp.money);
            }
            else //到达目的地
            {
                _gameManager.ChangeHp(-1);
            }

            _countEnemyAlive--;
        }

        IEnumerator SpawnEnemy()
        {
            Vector3 startPos = new(-5.06899977f, 2.17700005f, 0.0199999996f);
            const float waveRate = 3;
            Wave[] waves =
            {
                new()
                {
                    enemyPrefab = Resources.Load("Enemy/Initial/Initial") as GameObject,
                    count = 10,
                    rate = 0.3f
                },
                new()
                {
                    enemyPrefab = Resources.Load("Enemy/Initial/Initial") as GameObject,
                    count = 10,
                    rate = 0.3f
                }
            };
            GameObject root = GameObject.Find("Enemy");
            foreach (Wave wave in waves)
            {
                for (int i = 0; i < wave.count; i++)
                {
                    var temp = Instantiate(wave.enemyPrefab, startPos, Quaternion.identity);
                    temp.transform.parent = root.transform;
                    temp.GetComponent<Enemy.Enemy>().manager = this;
                    Enemies.Add(temp);
                    _countEnemyAlive++;
                    if (i != wave.count - 1)
                        yield return new YieldWaitForSeconds(wave.rate);
                }

                while (_countEnemyAlive > 0)
                {
                    yield return 0;
                }

                yield return new YieldWaitForSeconds(waveRate);
            }

            _gameManager.Win();
        }
    }
}