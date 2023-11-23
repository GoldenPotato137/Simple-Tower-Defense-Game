using Turret;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
	public class BuildManager : ManagerBase 
	{
		private TurretData _axDataPrefab;
		private TurretData _bowDataPrefab;
		private TurretData _leafDataPrefab;
		private readonly UiManager _uiManager;
		private readonly GameManager _gameManager;

		//表示当前选择的炮塔（要建造的炮塔）
		public static TurretData SelectedTurretData;
		//表示当前选择的格子（场景中的游戏物体）
		private MapCube _mapCube;

		public BuildManager(GameManager gameManager, UiManager uiManager)
		{
			SelectedTurretData = null;
			_gameManager = gameManager;
			_uiManager = uiManager;
		}
		
		public override void Stop()
		{
			throw new System.NotImplementedException();
		}

		
		public override void Update()
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
						_mapCube = hit.collider.GetComponent<MapCube>();
						if (SelectedTurretData != null && _mapCube.turretGo == null)
						{
							if (GameManager.Money >= SelectedTurretData.cost) //可以创建
							{
								_gameManager.ChangeMoney(-SelectedTurretData.cost);
								_mapCube.BuildTurret(SelectedTurretData);
							}
							else
								_uiManager.ShowNoEnoughMoney();
						}
						else if (_mapCube.turretGo != null) //显示升级UI
						{
							var temp = _mapCube.GetComponent<MapCube>();
							_uiManager.ShowUpgradeMenu(_mapCube.transform.position, temp.GetUpgradePrice(),
								temp.GetDeletePrice());
						}
					}
				}
			}

			if (Input.GetMouseButton(1)) //右键取消当前选择&升级
			{
				SelectedTurretData = null;
				_uiManager.HideUpgradeMenu();
			}
		}

		public void OnAxSelected()
		{
			SelectedTurretData = _axDataPrefab;
		}

		public  void OnBowSelected()
		{
			SelectedTurretData = _bowDataPrefab;
		}

		public void OnLeafGrassSelected()
		{
			SelectedTurretData = _leafDataPrefab;
		}
		
		public void OnUpgradeButtonDown()
		{
			if (_mapCube == null || _mapCube.turretGo == null) return;
			if (!_mapCube.UpgradeTurret()) //不够钱
				_uiManager.ShowNoEnoughMoney();
			_uiManager.HideUpgradeMenu();
			_uiManager.FlushMoney();
		}
		
		public void OnDestroyButtonDown()
		{
			if (_mapCube == null || _mapCube.turretGo == null) return;
			_mapCube.DestroyTurret();
			_uiManager.HideUpgradeMenu();
			_uiManager.FlushMoney();
		}
	}
}
