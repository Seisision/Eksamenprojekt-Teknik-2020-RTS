using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class BuildingDescriptionBox : DescriptionBox
{
    BuildingType buildingType;
    public Text GoldCost;
    public GameObject goldSprite;
    public Text LumberCost;
    public GameObject lumberSprite;

    private void Start()
    {
        setBuildingType();

        if (buildingType.GoldCost != 0)
        {
            GoldCost.text = buildingType.GoldCost.ToString();
        }
        else
        {
            GoldCost.text = "";
            goldSprite.SetActive(false);
        }

        if (buildingType.LumberCost != 0)
        {
            print(buildingType.LumberCost.ToString());
            LumberCost.text = buildingType.LumberCost.ToString();
        }
        else
        {
            LumberCost.text = "";
            lumberSprite.SetActive(false);
        }

    }

    void setBuildingType()
    {
        List<BuildingType> buildingTypes = new List<BuildingType> { BuildingType.Barracks, BuildingType.Farm, BuildingType.GoldMine, BuildingType.LumberMill };
        foreach (BuildingType bt in buildingTypes)
        {
            if (nameText.text == bt.Name)
            {
                buildingType = bt;
            }
        } 
    }
}
