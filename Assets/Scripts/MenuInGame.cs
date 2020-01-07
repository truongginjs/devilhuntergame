using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuInGame : MonoBehaviour
{
    [SerializeField] Button bt25, bt50, bt100;
    Button select;
    float num;

    void Awake()
    {
        select = bt50;
        num = 0.5f;
        setPercentOfUnit();
    }

    private void setPercentOfUnit()
    {
        ManagerGame.percentOfUnit = num;
    }

    public void SelectedButton25()
    {
        UnselectBtn();
        bt25.GetComponent<Image>().sprite=Resources.Load<Sprite>("Sprites/selected");
        select = bt25;
        num = 0.25f;
        setPercentOfUnit();
    }

    public void SelectedButton50()
    {
        UnselectBtn();
        bt50.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/selected");
        select = bt50;

        num = 0.5f;
        setPercentOfUnit();
    }
    public void SelectedButton100()
    {
        UnselectBtn();
        bt100.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/selected");
        select = bt100;

        num = 1;
        setPercentOfUnit();
    }
    private void UnselectBtn()
    {
        select.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/unselect");
    }

}
