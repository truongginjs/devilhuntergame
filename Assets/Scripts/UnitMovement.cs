using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float speed;
    private Animator unitAnimator;
    public static Building start, end;
    [SerializeField] List<Building> path;
    private int count = 1;
    bool fighting;


    void Start()
    {

        //speed = 100;
        transform.position = start.transform.position;
        unitAnimator = GetComponent<Animator>();

        fighting = GetComponent<Units>().isAttack;
        path = GetComponent<FindWay>().getWays(start, end);

    }



    void Update()
    {

        if (count < path.Count)
        {
            if (fighting)
                GoToTagrget(0);
            else
                GoToTagrget(speed);
        }
        if (Vector3.Distance(transform.position, path[path.Count - 1].transform.position) < 5)
        {
            end.currentAmount += (int)gameObject.GetComponent<Units>().amount;
            Destroy(gameObject);
        }
    }

    public static void setPosition(Building startBuiding, Building endBuiding)
    {
        start = startBuiding;
        end = endBuiding;

    }

    private void GoToTagrget(float speed)
    {
        Transform target = path[count].transform;
        float maxDistanceDetail = speed * Time.deltaTime;
        transform.LookAt(target);
        //bool fighting = GetComponent<Units>().isFighting;
        //if (!fighting)
        //{
        transform.position = Vector3.MoveTowards(transform.position, target.position, maxDistanceDetail);
        //}
        //else
        //{

        //}
        if (Vector3.Distance(transform.position, target.position) > 1)
        {
            //print("maxDistanceDetail: " + maxDistanceDetail);
            //print("khoang cach: " + Vector3.Distance(transform.position, target.position));
            unitAnimator.SetInteger("condition", 1);
        }
        else
        {
            if (count < path.Count - 1) count++;
            unitAnimator.SetInteger("condition", 0);
        }
    }
}
