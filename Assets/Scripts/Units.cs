using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class Units : MonoBehaviour
{
    public float attack;
    public float defend;
    public float amount;
    [SerializeField] float speed;

    [SerializeField] Image HPBar;
    [SerializeField] public float CDTime = 0f; //giam het amount trong CDTime
    [SerializeField] float DPSFromEnemy = new float();
    public bool isPlayer;
    public bool isNeutral = false;
    Building building;


    GameObject EnemyUnit;


    //
    public bool isAttack = false;
    public bool isFighting = false;

    void OnTriggerEnter(Collider Other)
    {
        EnemyUnit = Other.gameObject;
        Units UnitEnemy = Other.gameObject.GetComponent<Units>();
        if (isPlayer != UnitEnemy.isPlayer || UnitEnemy.isNeutral)
        {
            isAttack = true;
            DPSFromEnemy = CaculateDPS(this, UnitEnemy);
        CDTime = amount / DPSFromEnemy;
        }
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
            DPS = 0.01f;
        }
        return DPS;
    }
    //BUG: ko chay cung luc khi HPBar giam
    //0.031 const de amount va HPbar chay cung luc
    IEnumerator amountDE()
    {
        isFighting = true;
        if (building != null)
            building.stopHealing();
        while (isAttack && amount > 0)
        {
            amount -= DPSFromEnemy;
            if(building == null)
                HPBar.fillAmount -= (1f / CDTime);
            else
                building.currentAmount = (int)amount;
            
            if (EnemyUnit == null)
                break;
            yield return new WaitForSeconds(1f);
        }
        isAttack = !isAttack;
        isFighting = !isFighting;
        if (building != null)
            building.startHealing();
        if (amount < 1)
        {
            if (building != null)
            {
                isPlayer = !isPlayer;
                isNeutral = false;
            }
            else
            {
                StartCoroutine(DestroyUnit());                
            }
        }
    }

    private IEnumerator DestroyUnit()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Start()
    {
        building = GetComponent<Building>();
    }
    void Update()
    {
        if (isAttack && !isFighting)
        {
            StartCoroutine(amountDE());
        }
        else
        {
            StopCoroutine(amountDE());
        }
    }
}