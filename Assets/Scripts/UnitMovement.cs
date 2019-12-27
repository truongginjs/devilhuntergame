using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float speed;
    private Animator unitAnimator;
    [SerializeField] Building start, end;
    [SerializeField] List<Building> path;
    private int count = 1;
    void Start()
    {
        transform.position = start.transform.position;
        unitAnimator = GetComponent<Animator>();
        path = GetComponent<FindWay>().getWays(start, end);


        //foreach(var t in path)
        //{
        //    print(t.name+"\n");
        //}



    }
    void Update()
    {
        if (count < path.Count)
            GoToTagrget();
    }

    private void GoToTagrget()
    {
        Transform target = path[count].transform;
        float maxDistanceDetail = speed * Time.deltaTime;
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, maxDistanceDetail);
        if (Vector3.Distance(transform.position, target.position) > 1)
        {
            //print("maxDistanceDetail: " + maxDistanceDetail);
            //print("khoang cach: " + Vector3.Distance(transform.position, target.position));
            unitAnimator.SetFloat("speed", maxDistanceDetail);
        }
        else
        {
            if (count < path.Count - 1) count++;

            unitAnimator.SetFloat("speed", 0);
        }
    }
}
