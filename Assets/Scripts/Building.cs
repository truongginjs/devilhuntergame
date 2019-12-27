using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Dictionary<Building, float> ways = new Dictionary<Building, float>();
    [SerializeField] List<Building> keys = new List<Building>();
    int maxUnit = 50;
    int currentUnit = 10;
    int level = 5;
    int UnitsPerSecond = 5;
    float def = 0.5f;
    void Awake()
    {
        keysToWays();
    }

    private void keysToWays()
    {
        foreach (var key in keys)
        {
            var dictance = Mathf.Abs(Vector3.Distance(key.transform.position, gameObject.transform.position));
            ways.Add(key, dictance);
        }
    }
    public void SetTopColor(Color color)
    {
        MeshRenderer topMesh = transform.Find("Top").GetComponent<MeshRenderer>();
        topMesh.material.color = color;
    }
    private void OnMouseOver()
    {

    }
}

