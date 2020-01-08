using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Building : MonoBehaviour
{
    [SerializeField] ParticleSystem upgradeBuilding;

    public Dictionary<Building, float> ways = new Dictionary<Building, float>();
    [SerializeField] List<Building> keys = new List<Building>();
    public int currentAmount = 1;
    public int level = 1;
    [SerializeField] float delayUpgrade = 2f;

    Text healthText;
    private bool isHealing;
    public Units unit;
    Dictionary<int, BuildingInfo> Buildings;
    //----
    [SerializeField] Image flag;
    //----
    void Awake()
    {
        Buildings = ManagerGame.Buildings;
        keysToWays();
        healthText = GetComponentInChildren<Text>();
        healthText.text = currentAmount.ToString();
        isHealing = false;
        unit = GetComponent<Units>();
    }

    void setFlag()
    {
        if (unit.isNeutral)
        {
            flag.color = Color.white;
        }
        else
        {
            if (unit.isPlayer)
                flag.color = Color.red;
            else
                flag.color = Color.blue;
        }
    }

    private void Update()
    {
        unit.amount = currentAmount;
        unit.defend = Buildings[level].Def;
        if (currentAmount < Buildings[level].Amount && !isHealing)
        {
            StartCoroutine(Healing());
        }
        if (currentAmount > Buildings[level].Amount)
            currentAmount = Buildings[level].Amount;

        setFlag();
        healthText.text = currentAmount.ToString() + " / " + Buildings[level].Amount;
        var info = Buildings[level];
    }

    public void stopHealing()
    {
        StopCoroutine(Healing());
    }
    public void startHealing()
    {
        StartCoroutine(Healing());
    }

    public void Upgrade()
    {
        if (level <= 5)
        {

            Upgrading(true);
            StartCoroutine(StartUpgrade());
        }
    }

    private IEnumerator StartUpgrade()
    {

        yield return new WaitForSeconds(delayUpgrade);
        int index = Buildings[level].Amount / 2;
        if (currentAmount >= index)
        {
            level++;
            currentAmount -= index;
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
        while (currentAmount < info.Amount)
        {
            isHealing = true;
            currentAmount += 1;
            yield return new WaitForSeconds(info.TimeOfHealing);
        }
        isHealing = false;
    }

    public int GoAllyGo()
    {
        int result = (int)(currentAmount * ManagerGame.percentOfUnit);
        currentAmount -= result;
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

