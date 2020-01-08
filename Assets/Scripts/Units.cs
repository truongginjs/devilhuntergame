﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class Units : MonoBehaviour
{
    [SerializeField] public float attack;
    [SerializeField] public float defend;
    [SerializeField] public float amount;
    [SerializeField] float speed;

    [SerializeField] Image HPBar;
    [SerializeField] public float CDTime = 0f; //giam het amount trong CDTime
    [SerializeField] float DPSFromEnemy = new float();
    [SerializeField] bool isPlayer = false;

    [SerializeField] bool isNeutral = false;

    Animator unitAnimator;

    [SerializeField] AudioSource runSFX, deadSFX, spawnSFX, hitSFX;

    GameObject EnemyUnit;

    //player hay computer

    //
    bool isAttack = true, isFighting = false;

    void OnTriggerEnter(Collider Other)
    {
        unitAnimator = GetComponent<Animator>();
        EnemyUnit = Other.gameObject;
        Units UnitEnemy = Other.gameObject.GetComponent<Units>();
        if (isPlayer != UnitEnemy.isPlayer || UnitEnemy.isNeutral)
        {
            DPSFromEnemy = CaculateDPS(this, UnitEnemy);
        }
        CDTime = amount / DPSFromEnemy;
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject == EnemyUnit)
        {
            EnemyUnit = null;
        }
    }

    public float CaculateDPS(Units unit1, Units unit2)
    {
        //DPS: amount giam trong 1 giay

        float DPS = new float();
        DPS = (unit2.attack * unit2.amount - unit1.defend * unit1.amount) * 0.01f;
        if (DPS <= 0)
        {
            DPS = 1f;
        }
        return DPS;
    }
    //BUG: ko chay cung luc khi HPBar giam
    //0.031 const de amount va HPbar chay cung luc
    IEnumerator amountDE()
    {
        isFighting = true;
        while (isAttack && amount > 0)
        {
            HPBar.fillAmount -= (1f / CDTime);
            amount -= DPSFromEnemy;
            if (EnemyUnit == null)
                break;
            yield return new WaitForSeconds(1f);
        }
        isAttack = !isAttack;
        isFighting = !isFighting;
        if (amount == 0)
        {
            if (GetComponent<Building>() != null)
            {
                isPlayer = !isPlayer;
            }
            else
            {
                StartCoroutine(DestroyUnit());                
            }
        }
    }

    private IEnumerator DestroyUnit()
    {
        unitAnimator.SetInteger("condition", 5);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void Update()
    {
        if (isAttack && !isFighting)
        {
            unitAnimator.SetInteger("condition", 2);
            StartCoroutine(amountDE());
        }
        else
        {
            StopCoroutine(amountDE());
            unitAnimator.SetInteger("condition", 0);
        }
    }
}