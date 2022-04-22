using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Turret;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {
	public TurretData AxData;
	public TurretData BowData;
	public TurretData LeafData;

	//表示当前选择的炮塔（要建造的炮塔）
	private TurretData selectedTurretData;
	//表示当前选择的炮塔（场景中的游戏物体）
	private GameObject selectedTurretGo;
	public Text moneyText;
	public Animator moneyAnimator;
	private int money = 350;
	public GameObject upgradeCanvas;
	private Animator upgradeCanvasAnimator;
	public Button buttonUpgrade;
	void ChangeMoney(int change = 0)
	{
		money += change;
		moneyText.text = "$" + money;
	}
	void Start()
    {
		// upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

	//表示鼠标的建造炮塔
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			if (EventSystem.current.IsPointerOverGameObject()==false)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool isCollider = Physics.Raycast(ray, out hit, 350, LayerMask.GetMask("MapCube"));
				if (isCollider)
				{
					MapCube mapCube = hit.collider.GetComponent<MapCube>();
					if (selectedTurretData != null && mapCube.turretGo == null)
					{
						//可以创建
						if (money > selectedTurretData.cost)
						{
							ChangeMoney(-selectedTurretData.cost);
							mapCube.BuildTurret(selectedTurretData.turretPrefab);

						}
						else 
						{
							//TODO提示钱不够
							moneyAnimator.SetTrigger("Flicker");
						}
					}
                    else if (mapCube.turretGo != null)
					{
						//升级处理
						if (mapCube.isUpgraded)
                        {

							ShowUpgradeUI(mapCube.transform.position, true);
							//if(mapCube.isUpgraded);
							//{
							//ShowUpgradeUI(mapCube.transform.position, true);
							//}
							//else
							//{
							//	ShowUpgradeUI(mapCube.transform.position, false);
							//}
							if(mapCube.turretGo==selectedTurretGo&& upgradeCanvas.activeInHierarchy)
                            {
								// StartCorountine(HideUpgradeUI());
                            }
							else
							{
							ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
							}
							selectedTurretGo = mapCube.turretGo;
						}
						
					}
				}
			}
		}
	}
	public void OnAxSelected(bool isOn)
	{
		if (isOn)
        {
			selectedTurretData = AxData;
        }
    }

	public  void OnBowSelected(bool isOn)
    {
		if (isOn)
		{
			selectedTurretData = BowData;
		}
	}

	public void OnLeafGrassSelected(bool isOn)
	{
		if (isOn)
		{
			selectedTurretData = LeafData;
		}
	}
	void ShowUpgradeUI(Vector3 pos,bool isDisablaUpgrade=false)
    {
		StopCoroutine("HideUpgradeUI");
		upgradeCanvas.SetActive(false);
		upgradeCanvas.SetActive(true);
		upgradeCanvas.transform.position = pos;
		buttonUpgrade.interactable =! isDisablaUpgrade;
    }
	IEnumerator HideUpgradeUI()
    {
		upgradeCanvasAnimator.SetTrigger("Hide");
		//upgradeCanvas.SetActive(false);
		yield return new WaitForSeconds(0.8f);
		upgradeCanvas.SetActive(false);
	}
	public void OnUpgradeButtonDown()
    {
		//TODO
    }
	public void OnDestroyButtonDown()
	{
		//TODO
	}
}
