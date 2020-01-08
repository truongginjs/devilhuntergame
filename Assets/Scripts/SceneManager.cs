using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    Building[] buildings;
    // Start is called before the first frame update
    void Start()
    {
        buildings = GetComponentsInChildren<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVictoryOrDefeat();      
    }

    private void CheckVictoryOrDefeat()
    {
        bool hasBuildingOfPlayer = false;
        bool hasBuildingOfAI= false;

        foreach (var b in buildings)
        {
            var temp = b.unit;

            if (temp.isNeutral)
            {
                if(temp.isPlayer)
                    hasBuildingOfPlayer = true;
                if (temp.isPlayer)
                    hasBuildingOfAI = true;
            }
        }
        if (hasBuildingOfPlayer)
        {
            //thang
        }
        if (hasBuildingOfAI)
        {
            //thua
        }

    }
}
