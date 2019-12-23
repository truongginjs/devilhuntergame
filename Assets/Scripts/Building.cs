using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] public string name;
    //public SerializationCallbackScript ways = new SerializationCallbackScript();
    public Dictionary<Building, float> ways = new Dictionary<Building, float>();
    [SerializeField] List<Building> _key = new List<Building>();
    void keyToWays()
    {
        foreach(var i in _key)
        {
            ways.Add(i, DistanceFromKey(i));
        }
    }
    float DistanceFromKey(Building b)
    {
        return Mathf.Abs(Vector3.Distance(transform.position, b.transform.position));

    }




    [SerializeField] int health;
    [SerializeField] int defend;
    [SerializeField] int buildingTime;
    [SerializeField] int level;
    [SerializeField] int maxUnit;
    [SerializeField] int unitPerSecond;

    void Awake()
    {
        keyToWays();
        foreach(var z in ways)
        {
            print(z);
        }
    }
}
