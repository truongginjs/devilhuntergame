using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactories : MonoBehaviour
{
    [SerializeField] List<GameObject> UnitAllyPrefab = new List<GameObject>();

    [SerializeField] GameObject start, end;
    bool click = false;


    private void GenerateAllyUnit()
    {
        int healthOfUnit = start.GetComponent<Building>().GoAllyGo();
        if (healthOfUnit == 0) return;
        UnitMovement.setPosition(start.GetComponent<Building>(), end.GetComponent<Building>());
        Units unitBuilding = start.GetComponent<Units>();
       
            UnitAllyPrefab[0].GetComponent<Units>().amount = healthOfUnit;
            Instantiate(UnitAllyPrefab[0], transform.position, Quaternion.identity);
       
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            var temp = hit.collider.transform.gameObject;
            if (Input.GetMouseButtonDown(0) && temp.GetComponent<Building>() != null)//chuot trai
            {
                if (click)//lan 2
                {
                    end = temp;
                    print(end.name);
                    if (!start.Equals(end))
                        GenerateAllyUnit();
                    Unselected();
                }
                else //lan 1
                {
                    if (temp.GetComponent<Building>().unit.isPlayer)
                    {
                        ((Behaviour)temp.GetComponent("Halo")).enabled = true;
                        start = temp;
                        print(start.name);

                    }
                    else
                        click = true;
                }
                click = !click;
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Unselected();
            click = false;
        }
    }

    private void Unselected()
    {
        ((Behaviour)start.GetComponent("Halo")).enabled = false;
        start = null;
        end = null;
    }
}
