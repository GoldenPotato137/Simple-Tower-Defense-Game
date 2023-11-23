using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
    [System.Serializable]
    public class TurretData
    {
        public GameObject turretPrefab;
        public int cost;
        public GameObject turretUpgradedPrefab;
        public int costUpgraded;
        public GameObject turretUltimatePrefab;
        public int costUltimate;
        public TurretType type;
    }

    public enum TurretType
    {
        Ax,
        Bow,
        LeafGrass
    }
}