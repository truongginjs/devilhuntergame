using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] public string name;
    public SerializationCallbackScript ways = new SerializationCallbackScript();
  

    //[SerializeField] int health;
    //[SerializeField] int defend;
    //[SerializeField] int buildingTime;
    //[SerializeField] int level;
    //[SerializeField] int cost;
}
