using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Manager;
using Turret;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour 
{
    [HideInInspector]
    public GameObject turretGo;//保存当前cube身上的炮塔
    public GameObject tempTurret; //用于预览的临时炮塔
    public  bool isUpgraded = false;
    [HideInInspector]
    public GameObject buildEffect;
    private Renderer renderer1;

    void Start()
    {
        renderer1 = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) //右键取消建筑
        {
            Destroy(tempTurret);
            tempPrefab = null;
        }
    }

     public void BuildTurret(GameObject turretPrefab)
    {
        isUpgraded = false;
        var position = transform.position;
        position.z -= 0.5f;
        turretGo = GameObject.Instantiate(turretPrefab, position, Quaternion.identity);
        turretGo.transform.parent = transform;
        GameObject effect = (GameObject)Instantiate(buildEffect, position,Quaternion.identity);
        Destroy(effect, 1);
    }

    public GameObject tempPrefab;
    void TempBuild(GameObject turretPrefab)
    {
        var position = transform.position;
        position.z -= 0.5f;
        tempTurret = GameObject.Instantiate(turretPrefab, position, Quaternion.identity);
        tempTurret.transform.parent = transform;
        var tempColor = tempTurret.GetComponent<SpriteRenderer>().color;
        tempColor.a = 0.67f;
        tempTurret.GetComponent<SpriteRenderer>().color = tempColor;
        tempTurret.GetComponent<Turret.Turret>().enabled = false;
        tempTurret.GetComponent<SphereCollider>().enabled = false;
    }


    void OnMouseEnter()
    {
        if (BuildManager.selectedTurretData != null) tempPrefab = BuildManager.selectedTurretData.turretPrefab;
        if(tempPrefab != null && tempTurret==null && turretGo==null)
            TempBuild(tempPrefab);
    }
    
    void OnMouseExit()
    {
        if (tempTurret != null)
        {
            Destroy(tempTurret);
            Debug.Log("Test");
        }
    }
}
