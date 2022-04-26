using System.Collections;
using Turret;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Manager
{
	public class BuildManager : MonoBehaviour 
	{
		[FormerlySerializedAs("AxData")] public TurretData axData;
		[FormerlySerializedAs("BowData")] public TurretData bowData;
		[FormerlySerializedAs("LeafData")] public TurretData leafData;
		[SerializeField] private UiManager uiManager;
		[SerializeField] private GameManager gameManager;

		//表示当前选择的炮塔（要建造的炮塔）
		public static TurretData selectedTurretData;
		//表示当前选择的炮塔（场景中的游戏物体）
		private GameObject selectedTurretGo;
		public GameObject upgradeCanvas;
		// private Animator upgradeCanvasAnimator;
		public Button buttonUpgrade;
		private static readonly int Hide = Animator.StringToHash("Hide");

		void Start()
		{
			// upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
			selectedTurretData = null;
		}

		
		void Update()
		{
			if (Camera.main == null) return;
			if (Input.GetMouseButtonDown(0)) 
			{
				//表示鼠标的建造炮塔
				if (EventSystem.current.IsPointerOverGameObject()==false)
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					bool isCollider = Physics.Raycast(ray, out var hit, 350, LayerMask.GetMask("MapCube"));
					if (isCollider)
					{
						MapCube mapCube = hit.collider.GetComponent<MapCube>();
						if (selectedTurretData != null && mapCube.turretGo == null)
						{
							//可以创建
							if (GameManager.money > selectedTurretData.cost)
							{
								gameManager.ChangeMoney(-selectedTurretData.cost);
								mapCube.BuildTurret(selectedTurretData.turretPrefab);
							}
							else
								uiManager.ShowNoEnoughMoney();
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

			if (Input.GetMouseButton(1)) //右键取消当前选择
				selectedTurretData = null;
		}
		
		public void OnAxSelected(bool isOn)
		{
			if (isOn)
			{
				selectedTurretData = axData;
			}
		}

		public  void OnBowSelected(bool isOn)
		{
			if (isOn)
			{
				selectedTurretData = bowData;
			}
		}

		public void OnLeafGrassSelected(bool isOn)
		{
			if (isOn)
			{
				selectedTurretData = leafData;
			}
		}
		
		void ShowUpgradeUI(Vector3 pos,bool isDisablaUpgrade=false)
		{
			StopCoroutine(nameof(HideUpgradeUI));
			upgradeCanvas.SetActive(false);
			upgradeCanvas.SetActive(true);
			upgradeCanvas.transform.position = pos;
			buttonUpgrade.interactable =! isDisablaUpgrade;
		}
		
		IEnumerator HideUpgradeUI()
		{
			// upgradeCanvasAnimator.SetTrigger(Hide);
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
}
