using System.Collections;
using UnityEngine;

namespace Turret
{
    public class Bow : Turret
    {
        [SerializeField] private GameObject bulletBow;
        
        protected override bool Attack()
        {
            var target = GetNearestEnemy();
            if (target != null && (target.transform.position - transform.position).magnitude < fireRange)
            {
                Fire(target);
                return true;
            }
            return false;
        }

        void Fire(GameObject target)
        {
            var heading = target.transform.position - firePosition[0].position;
            foreach (var fire in firePosition)
            {
                GameObject bullet = Instantiate(bulletBow, fire.position, Quaternion.identity);
                bullet.GetComponent<BulletBow>().SetHeading(heading);
                bullet.GetComponent<BulletBow>().damage = this.damage;
            }
        }
    }
}