using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingInfo
{
    public int Health { get; set; }
    public float Def { get; set; }
    public float TimeOfHealing { get; set; }
}
public class Building : MonoBehaviour
{
    public static readonly Dictionary<int, BuildingInfo> Builings = new Dictionary<int, BuildingInfo>
    {
        {1, new BuildingInfo { Health = 10, Def = 0.1f, TimeOfHealing = 5f }},
        {2, new BuildingInfo { Health = 20, Def = 0.2f, TimeOfHealing = 4f }},
        {3, new BuildingInfo { Health = 30, Def = 0.3f, TimeOfHealing = 3f }},
        {4, new BuildingInfo { Health = 40, Def = 0.4f, TimeOfHealing = 2f }},
        {5, new BuildingInfo { Health = 50, Def = 0.5f, TimeOfHealing = 0.5f }}
    };
    public Dictionary<Building, float> ways = new Dictionary<Building, float>();
    [SerializeField] List<Building> keys = new List<Building>();
    int currentHealth = 10;
    [SerializeField] int level = 1;
    Text healthText;
    void Awake()
    {
        keysToWays();
        healthText = GetComponentInChildren<Text>();
        healthText.text = currentHealth.ToString();

    }
    private void Start()
    {
        StartCoroutine(Healing());
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString();
        var info = Builings[level];       
    }

    public void UpgradeBuiding()
    {
        level++;
    }
    //hoi mau
    public IEnumerator Healing()
    {
        var info = Builings[level];
        while (true)
        {
            if(currentHealth < info.Health)
            {
                currentHealth+=1;
            }
            yield return new WaitForSeconds(info.TimeOfHealing);
        }
    }

    public void GoAllyGo()
    {
         currentHealth /= 2;

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

