using Turret;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

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
		//表示当前选择的格子（场景中的游戏物体）
		private MapCube mapCube;

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
						mapCube = hit.collider.GetComponent<MapCube>();
						if (selectedTurretData != null && mapCube.turretGo == null)
						{
							if (GameManager.money > selectedTurretData.cost) //可以创建
							{
								gameManager.ChangeMoney(-selectedTurretData.cost);
								mapCube.BuildTurret(selectedTurretData);
							}
							else
								uiManager.ShowNoEnoughMoney();
						}
						else if (mapCube.turretGo != null) //显示升级UI
							uiManager.ShowUpgradeMenu(mapCube.transform.position);
					}
				}
			}

			if (Input.GetMouseButton(1)) //右键取消当前选择&升级
			{
				selectedTurretData = null;
				uiManager.HideUpgradeMenu();
			}
		}
		
		public void OnAxSelected()
		{
			selectedTurretData = axData;
		}

		public  void OnBowSelected()
		{
			selectedTurretData = bowData;
		}

		public void OnLeafGrassSelected()
		{
			selectedTurretData = leafData;
		}
		
		// IEnumerator HideUpgradeUI()
		// {
		// 	// upgradeCanvasAnimator.SetTrigger(Hide);
		// 	//upgradeCanvas.SetActive(false);
		// 	yield return new WaitForSeconds(0.8f);
		// 	upgradeCanvas.SetActive(false);
		// }
		
		public void OnUpgradeButtonDown()
		{
			Debug.Log("test1");
			if (mapCube == null || mapCube.turretGo == null) return;
			Debug.Log("test2");
			if (!mapCube.UpgradeTurret()) //不够钱
				uiManager.ShowNoEnoughMoney();
			uiManager.FlushMoney();
		}
		
		public void OnDestroyButtonDown()
		{
			//TODO
		}
	}
}
