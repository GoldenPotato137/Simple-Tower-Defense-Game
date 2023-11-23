using System.Collections.Generic;
using Enums;
using Helper;
using Turret;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
	public class BuildManager : ManagerBase 
	{
		private readonly UiManager _uiManager;
		private readonly GameManager _gameManager;
		private readonly Dictionary<TurretType, TurretData> _turretDataDic = new();

		//表示当前选择的炮塔（要建造的炮塔）
		public static TurretData SelectedTurretData;
		//表示当前选择的格子（场景中的游戏物体）
		private MapCube _mapCube;

		public BuildManager(GameManager gameManager, UiManager uiManager)
		{
			SelectedTurretData = null;
			_gameManager = gameManager;
			_uiManager = uiManager;

			_turretDataDic[TurretType.Ax] = new TurretData
			{
				type = TurretType.Ax,
				cost = 160,
				costUpgraded = 200,
				costUltimate = 300,
				turretPrefab = Resources.Load("Turret/Ax/Ax_one") as GameObject,
				turretUpgradedPrefab = Resources.Load("Turret/Ax/Ax_two") as GameObject,
				turretUltimatePrefab = Resources.Load("Turret/Ax/Ax_three") as GameObject,
			};
			_turretDataDic[TurretType.Bow] = new TurretData
			{
				type = TurretType.Bow,
				cost = 100,
				costUpgraded = 250,
				costUltimate = 350,
				turretPrefab = Resources.Load("Turret/Bow/Bow_one") as GameObject,
				turretUpgradedPrefab = Resources.Load("Turret/Bow/Bow_two") as GameObject,
				turretUltimatePrefab = Resources.Load("Turret/Bow/Bow_three") as GameObject,
			};
			_turretDataDic[TurretType.LeafGrass] = new TurretData
			{
				type = TurretType.LeafGrass,
				cost = 120,
				costUpgraded = 300,
				costUltimate = 400,
				turretPrefab = Resources.Load("Turret/LeafGrass/LeafGrass_one") as GameObject,
				turretUpgradedPrefab = Resources.Load("Turret/LeafGrass/LeafGrass_two") as GameObject,
				turretUltimatePrefab = Resources.Load("Turret/LeafGrass/LeafGrass_three") as GameObject,
			};

			EventBus.Register<TurretType>(Events.UISelectTurret, SelectTurret);
			EventBus.Register(Events.UIUpgradePushed, OnUpgradeButtonDown);
			EventBus.Register(Events.UISellPushed, OnDestroyButtonDown);
		}
		
		public override void Stop()
		{
			EventBus.UnRegister<TurretType>(Events.UISelectTurret, SelectTurret);
			EventBus.UnRegister(Events.UIUpgradePushed, OnUpgradeButtonDown);
			EventBus.UnRegister(Events.UISellPushed, OnDestroyButtonDown);
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

		private void OnUpgradeButtonDown()
		{
			if (_mapCube == null || _mapCube.turretGo == null) return;
			if (!_mapCube.UpgradeTurret()) //不够钱
				_uiManager.ShowNoEnoughMoney();
			_uiManager.HideUpgradeMenu();
			_uiManager.FlushMoney();
		}
		
		private void OnDestroyButtonDown()
		{
			if (_mapCube == null || _mapCube.turretGo == null) return;
			_mapCube.DestroyTurret();
			_uiManager.HideUpgradeMenu();
			_uiManager.FlushMoney();
		}
		
		private void SelectTurret(TurretType type)
		{
			SelectedTurretData = type switch
			{
				TurretType.Ax => _turretDataDic[TurretType.Ax],
				TurretType.Bow => _turretDataDic[TurretType.Bow],
				TurretType.LeafGrass => _turretDataDic[TurretType.LeafGrass],
				_ => null
			};
		}
	}
}
