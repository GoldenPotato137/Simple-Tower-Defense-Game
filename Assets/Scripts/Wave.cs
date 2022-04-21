using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//保存每一波敌人生成所需要的属性
[System.Serializable]
public class Wave {

	public GameObject enemyPrefab;
	public int count;
	public float rate;
}
