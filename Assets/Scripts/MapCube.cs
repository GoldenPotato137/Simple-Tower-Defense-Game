using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;//保存当前cube身上的炮塔
    public  bool isUpgraded = false;
    [HideInInspector]
    public GameObject buildEffect;
    private Renderer renderer1;

    void Start()
    {
        renderer1 = GetComponent<Renderer>();
    }
    public void BuildTurret(GameObject turretPrefab)
    {
        isUpgraded = false;
        var position = transform.position;
        turretGo = GameObject.Instantiate(turretPrefab, position, Quaternion.identity);
        turretGo.transform.parent = transform;
        GameObject effect = (GameObject)Instantiate(buildEffect, position,Quaternion.identity);
        Destroy(effect, 1);

    }


    void OnMouseEnter()
    {
        // Debug.Log("OnMouseEnter");
        // if (turretGo ==null && EventSystem.current.IsPointerOverGameObject()==false)
        // {
        //     renderer1.material.color = Color.red;
        //     Debug.Log("Change to red!!!");
        // }
    }
    void OnMouseExit()
    {
        renderer1.material.color = Color.white;
    }
}
