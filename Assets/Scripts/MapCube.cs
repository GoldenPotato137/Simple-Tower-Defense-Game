using UnityEngine;
using Manager;
using Turret;

public class MapCube : MonoBehaviour 
{
    [HideInInspector]
    public GameObject turretGo;//保存当前cube身上的炮塔
    public GameObject tempTurret; //用于预览的临时炮塔
    [HideInInspector] public GameObject buildEffect;
    public TurretData turretData;
    private int level;

    void Update()
    {
        if (Input.GetMouseButton(1)) //右键取消建筑
        {
            Destroy(tempTurret);
            tempPrefab = null;
        }
    }

    public void BuildTurret(TurretData turret)
    {
        turretData = turret;
        level = 1;
        var turretPrefab = turret.turretPrefab;
        var position = transform.position; 
        position.z -= 0.5f; 
        turretGo = GameObject.Instantiate(turretPrefab, position, Quaternion.identity); 
        turretGo.transform.parent = transform; 
        GameObject effect = Instantiate(buildEffect, position,Quaternion.identity); 
        Destroy(effect, 1); 
    }

    public int GetUpgradePrice()
    {
        int ans = turretData.costUpgraded;
        if (level == 2) ans = turretData.costUltimate;
        if (level == 3) ans = 9999;
        return ans;
    }

    public int GetDeletePrice()
    {
        int ans = turretData.cost;
        if (level >= 2) ans += turretData.costUpgraded;
        if (level >= 3) ans += turretData.costUltimate;
        return (int)(ans * 0.5);
    }

    public bool UpgradeTurret()
    {
        if (level == 3 || GameManager.money < GetUpgradePrice()) return false;
        Destroy(turretGo);
        var position = transform.position;
        position.z -= 0.5f;
        turretGo = Instantiate(level == 1 ? turretData.turretUpgradedPrefab : turretData.turretUltimatePrefab,
            position, Quaternion.identity, transform);
        if (level == 1) GameManager.money -= turretData.costUpgraded;
        if (level == 2) GameManager.money -= turretData.costUltimate;
        level++;
        GameObject effect = Instantiate(buildEffect, position, Quaternion.identity);
        Destroy(effect, 1);
        return true;
    }

    public void DestroyTurret()
    {
        Destroy(turretGo);
        var transform1 = transform;
        GameObject effect = Instantiate(buildEffect, transform1.position, Quaternion.identity, transform1);
        Destroy(effect, 1);
        GameManager.money += GetDeletePrice();
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
        if (GameManager.isPause) return; //暂停时停止预览
        if (BuildManager.selectedTurretData != null) tempPrefab = BuildManager.selectedTurretData.turretPrefab;
        if(tempPrefab != null && tempTurret==null && turretGo==null)
            TempBuild(tempPrefab);
    }
    
    void OnMouseExit()
    {
        if (tempTurret != null)
        {
            Destroy(tempTurret);
            // Debug.Log("Test");
        }
    }
}
