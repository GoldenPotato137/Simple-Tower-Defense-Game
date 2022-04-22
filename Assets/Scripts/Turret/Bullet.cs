using UnityEngine;

namespace Turret
{
    public class Bullet : MonoBehaviour 
    {
        public int damage = 50;
        public float speed = 30;
        public GameObject explosionEffectPrefab;
        private float distanceArriveTarget = 0.05f;
        private Transform target;
        
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
        
        void Update()
        {
            if (target ==null ) //目标已死亡，子弹自爆
            {
                Die();
                return;
            }
            transform.LookAt(target.position);
            Transform transform1;
            (transform1 = transform).Translate(Vector3.forward * (speed * Time.deltaTime));
            if((target.position - transform1.position).magnitude< distanceArriveTarget)
            {
                target.GetComponent<Enemy.Enemy>().Damaged(damage);
                Die();
            }
        }
        
        void Die()
        {
            var transform1 = transform;
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform1.position, transform1.rotation);
            effect.transform.parent = this.transform;
            Destroy(effect, 1);
            Destroy(this.gameObject);
        }
    }
}
