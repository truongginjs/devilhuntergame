using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    Building building;
    [SerializeField] List<GameObject> UnitEnemyPrefab = new List<GameObject>();

    void Start()
    {
        building = GetComponent<Building>();
        
    }
    void Update()
    {
        if (!building.unit.isPlayer && !building.unit.isNeutral)
        {
            AiAttack();
        }
    }

    private void AiAttack()
    {
        if (CheckCanUpgrade())
        {
            foreach (var b in building.ways)
            {

                if (CheckIsNotMyBuilding(b.Key))
                {
                    AttackBuilding(b.Key);
                    return;
                }
            }
            UpgradeBuilding();

        }
    }

    private void UpgradeBuilding()
    {
        building.Upgrade();
    }

    private void AttackBuilding(Building b)
    {
        var start = building;
        var end = b;
        int healthOfUnit = start.GetComponent<Building>().GoAllyGo();
        if (healthOfUnit == 0) return;
        UnitMovement.setPosition(start.GetComponent<Building>(), end.GetComponent<Building>());
        Units unitBuilding = start.GetComponent<Units>();

            UnitEnemyPrefab[0].GetComponent<Units>().amount = healthOfUnit;
            Instantiate(UnitEnemyPrefab[0], transform.position, Quaternion.identity);

    }

    private bool CheckIsNotMyBuilding(Building b)
    {
        var temp = b.unit;
        if (temp.isNeutral || temp.isPlayer) return true;
        return false;
    }

    private bool CheckCanUpgrade()
    {
        return ManagerGame.Buildings[building.level].Amount/2 < building.currentAmount;
    }


}
