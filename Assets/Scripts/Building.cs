using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo
{
    public int Amount { get; set; }
    public float Def { get; set; }
    public float TimeOfHealing { get; set; }
}
public class Building : MonoBehaviour
{
    public static readonly Dictionary<int, BuildingInfo> Buildings = new Dictionary<int, BuildingInfo>
    {
        {1, new BuildingInfo { Amount = 10, Def = 0.1f, TimeOfHealing = 1.25f }},
        {2, new BuildingInfo { Amount = 20, Def = 0.2f, TimeOfHealing = 1f }},
        {3, new BuildingInfo { Amount = 30, Def = 0.3f, TimeOfHealing = 0.85f }},
        {4, new BuildingInfo { Amount = 40, Def = 0.4f, TimeOfHealing = 0.65f }},
        {5, new BuildingInfo { Amount = 50, Def = 0.5f, TimeOfHealing = 0.5f }}
    };

    [SerializeField] ParticleSystem upgradeBuilding;

    public Dictionary<Building, float> ways = new Dictionary<Building, float>();
    [SerializeField] List<Building> keys = new List<Building>();
    int currentHealth = 1;
    [SerializeField] int level = 1;
    [SerializeField] float delayUpgrade = 2f;

    Text healthText;
    private bool isHealing;

    //----
    [SerializeField] Image flag;
    int buildingOf = 0;
    //----
    void Awake()
    {
        keysToWays();
        healthText = GetComponentInChildren<Text>();
        healthText.text = currentHealth.ToString();
        isHealing = false;

    }

    void setFlag()
    {
        switch (buildingOf)
        {
            case 0:
                flag.color = Color.red;
                break;
            case 1:
                flag.color = Color.blue;
                break;
            default:
                flag.color = Color.white;
                break;
        }
    }

    private void Update()
    {
        if (currentHealth < Buildings[level].Amount && !isHealing)
        {
            StartCoroutine(Healing());
        }
        healthText.text = currentHealth.ToString() + " / " + Buildings[level].Amount;
        var info = Buildings[level];
    }

    public void Occupied()
    {

    }

    public void Upgrade()
    {
        Upgrading(true);
        StartCoroutine(StartUpgrade());
    }

    private IEnumerator StartUpgrade()
    {

        yield return new WaitForSeconds(delayUpgrade);
        int index = Buildings[level].Amount / 2;
        if (currentHealth >= index)
        {
            level++;
            currentHealth -= index;
        }
        Upgrading(false);
    }

    private void Upgrading(bool isUpgrading)
    {
        if (isUpgrading)
            upgradeBuilding.Play();
        else
            upgradeBuilding.Stop();
    }

    //hoi mau
    public IEnumerator Healing()
    {
        var info = Buildings[level];
        while (currentHealth < info.Amount)
        {
            isHealing = true;
            currentHealth += 1;
            yield return new WaitForSeconds(info.TimeOfHealing);
        }
        isHealing = false;
    }

    public int GoAllyGo()
    {
        int result = (int)(currentHealth * ManagerGame.percentOfUnit);
        currentHealth -= result;
        return result;
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

}

