using Manager;
using UnityEngine;

namespace Turret
{
    public class Leaf : Turret
    {
        protected override bool Attack()
        {
            if (EnemyManager.enemies.Count > 0)
            {
                var temp = Instantiate(bulletPrefab, firePosition[0].position, Quaternion.identity);
                temp.GetComponent<BulletLeaf>().damage = damage;
                return true;
            }
            return false;
        }
    }
}