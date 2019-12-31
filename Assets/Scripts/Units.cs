using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Units : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] public float attack;
    [SerializeField] public float defend;
    [SerializeField] public float amount;
    [SerializeField] float speed;
    [SerializeField] float cost;

    [SerializeField] Image HPBar;
    [SerializeField] public float CDTime = 0f; //giam het amount trong CDTime
    [SerializeField] float DPSFromEnemy = new float();
    float TimeStop;// dung de dung HPBar giam khi xong chien dau
    GameObject EnemyUnit;
    //player hay computer

    void OnTriggerEnter(Collider Other)
    {
        EnemyUnit=Other.gameObject;
        Units UnitEnemy = Other.gameObject.GetComponent<Units>();
        DPSFromEnemy=CaculateDPS(this, UnitEnemy);
        CDTime = amount / DPSFromEnemy;
       
        print("tiep xuc");
    }
    void OnTriggerExit(Collider Other)
    {
       if(Other.gameObject==EnemyUnit)
        {
            EnemyUnit = null;
        }
    }

    public float CaculateDPS(Units unit1, Units unit2)
    {
        //DPS: amount giam trong 1 giay
       
        float DPS = new float();
        DPS = (unit2.attack * unit2.amount - unit1.defend * unit1.amount);
        if (DPS == 0)
        {
            DPS = 1f;
        }
        return DPS;
    }
    //BUG: ko chay cung luc khi HPBar giam
    //0.031 const de amount va HPbar chay cung luc
    IEnumerator amountDE()
    {
        amount -= DPSFromEnemy*0.031f; 
        yield return new WaitForSeconds(1f);
    }
    void Start()
    {
       
    }
    void Update()
    {
        HPBar.fillAmount -= (1f / CDTime * Time.deltaTime)*TimeStop;
        StartCoroutine(amountDE());
        if (amount <= 0)
        {
            //UnitEnemy.CDTime = 0;
            //UnitEnemy.DPSFromEnemy = 0f;
            Destroy(gameObject);
        }
        if(EnemyUnit==null)
        {
            DPSFromEnemy = 0f;
            TimeStop = 0;
        }
        else
        {
            TimeStop = 1;
        }
    } 
}
