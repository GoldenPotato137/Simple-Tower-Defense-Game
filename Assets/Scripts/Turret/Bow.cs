using UnityEngine;

namespace Turret
{
    public class Bow : Turret
    {
        protected override bool Attack()
        {
            var target = GetNearestEnemy();
            if (target != null && (target.transform.position - transform.position).magnitude < fireRange)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                bullet.transform.parent = this.transform;
                bullet.GetComponent<Bullet>().SetTarget(target.transform);
                bullet.GetComponent<Bullet>().damage = this.damage;
                return true;
            }
            return false;
        }
    }
}