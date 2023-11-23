using Manager;
using UnityEngine;

namespace Turret
{
    public class BulletBow : MonoBehaviour 
    {
        public int damage = 50;
        public float speed = 30;
        public GameObject explosionEffectPrefab;
        private float distanceArriveTarget = 1f;
        private Vector3 heading;
        private float flyTime;

        public void SetHeading(Vector3 newHeading)
        {
            newHeading.z = 0;
            heading = newHeading.normalized;
            // Debug.Log(heading.x + " " + heading.y + " " + heading.z);
        }
        
        void Update()
        {
            flyTime += Time.deltaTime;
            if (flyTime >= 2f) //飞行时间过长，子弹自爆
            {
                Destroy(gameObject);
                return;
            }

            transform.position += heading * Time.deltaTime * speed;
            foreach (var target in EnemyManager.Enemies)
                if((target.transform.position - transform.position).magnitude< distanceArriveTarget)
                {
                    target.GetComponent<Enemy.Enemy>().Damaged(damage);
                    Die();
                    break;
                }
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
