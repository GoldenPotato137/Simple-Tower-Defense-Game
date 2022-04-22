using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
    public class Ax : Turret
    {
        protected override bool Attack()
        {
            List<Enemy.Enemy> toKill = new List<Enemy.Enemy>(); 
            foreach (var enemy in enemies)
                if((enemy.transform.position-transform.position).magnitude < fireRange)
                {
                    toKill.Add(enemy.GetComponent<Enemy.Enemy>());
                }
            foreach (var enemy in toKill)
                enemy.Damaged(damage);
            return toKill.Count!=0;
        }
    }
}