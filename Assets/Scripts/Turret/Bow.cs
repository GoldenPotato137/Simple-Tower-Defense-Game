namespace Turret
{
    public class Bow : Turret
    {
        protected override bool Attack()
        {
            var target = GetNearestEnemy();
            if (target != null && (target.transform.position - transform.position).magnitude < fireRange)
            {
                
                return true;
            }
            // if (enemys [0]==null)
            //     {
            //         UpdateEnemies();
            //     }
            //     if (enemys .Count >0)
            //     {
            //         GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            //         bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            //     }
            //     else
            //     {
            //         timer = attackRateTime;
            //     }
            return false;
        }
    }
}