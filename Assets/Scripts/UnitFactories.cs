using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactories : MonoBehaviour
{
    [SerializeField] GameObject UnitPrefab;
    //[SerializeField] int num = 3;
    [SerializeField] GameObject start, end;
    bool click = false;

    //public IEnumerator GenerateUnits()
    //{
    //    for (int i = 0; i < num; i++)
    //    {
    //        UnitMovement.setPosition(start.GetComponent<Building>(), end.GetComponent<Building>());
    //        var temp = Instantiate(UnitPrefab, transform.position, Quaternion.identity);

    //        yield return new WaitForSeconds(1f);
    //    }
    //}
    private void GenerateUnit()
    {
        start.GetComponent<Building>().GoAllyGo();
        UnitMovement.setPosition(start.GetComponent<Building>(), end.GetComponent<Building>());
        Instantiate(UnitPrefab, transform.position, Quaternion.identity);
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
                if (click)
                {
                    end = temp;
                    print(end.name);
                    if(!start.Equals(end))
                        GenerateUnit();
                    Unselected();
                }
                else
                {
                    ((Behaviour)temp.GetComponent("Halo")).enabled = true;                    
                    start = temp;
                    print(start.name);
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
