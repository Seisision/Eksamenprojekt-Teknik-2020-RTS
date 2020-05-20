using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitDescriptionbox : DescriptionBox
{
    UnitType unitType;
    public Text GoldCost;
    public GameObject goldSprite;
    public Text FoodCost;
    public GameObject foodSprite;

    public void setUnitType(UnitType passedUnitType)
    {
        //print("set unittype");

        foreach (UnitType ut in UnitType.GetUnitTypes())
        {
            if (passedUnitType == ut)
            {
                unitType = ut;
            }
        }
    }

    public void setText()
    {
        //print("set text");
        nameText.text = unitType.Name;
        description.text = unitType.Description;
        GoldCost.text = unitType.GoldCost.ToString();
        FoodCost.text = unitType.FoodCost.ToString();
    }
}
