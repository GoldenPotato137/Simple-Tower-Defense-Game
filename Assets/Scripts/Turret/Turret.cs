// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
//
// public class Turret : MonoBehaviour {
//
// 	public List<GameObject> enemys = new List<GameObject>();
// 	void OnTriggerEnter(Collider col)
//     {
//         if (col.tag =="Enemy")
//         {
//             enemys.Add(col.gameObject);
//         }
//         
//     }
//     void OnTriggerExit(Collider col)
//     {
//         if (col.tag =="Enemy")
//         {
//             enemys.Remove (col.gameObject);
//         }
//     }
//
//     public float  attackRateTime = 1;//多少秒攻击一次
//     public  float timer = 0;
//     public GameObject bulletPrefab;//子弹
//     public Transform firePosition;
//     public Transform head;
//     void Start()
//     {
//         timer = attackRateTime;
//     }
//     void  Update()
//     {
//         if (enemys.Count > 0 && enemys[0] != null)
//         {
//             Vector3 targetposition = enemys[0].transform.position;
//             targetposition.y = head.position.y;
//             head.LookAt(targetposition);
//         }
//         timer += Time.deltaTime;
//         if (enemys.Count>0&&timer >=attackRateTime )
//         {
//             timer = 0;
//             Attack();
//         }
//        
//     }
//     void Attack()
//     {
//         if (enemys [0]==null)
//         {
//             UpdateEnemys();
//         }
//         if (enemys .Count >0)
//         {
//             GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
//             bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
//         }
//       else
//         {
//             timer = attackRateTime;
//         }
//     }
//     void UpdateEnemys()
//     {
//         // enemys.RemoveAll(null);
//         List<int> emptyIndex = new List<int>();
//        for (int index =0; index <enemys .Count;index ++)
//         {
//             if (enemys [index]==null)
//             {
//                 emptyIndex.Add(index);
//             }
//         }
//         for (int i = 0; i < emptyIndex.Count ; i++)
//         {
//             enemys.RemoveAt(emptyIndex[i]-i);
//         }
//     }
// }
