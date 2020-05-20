using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    public Image[] icons;
    public UnitDescriptionbox[] unitDescriptionBoxes;

    public void updateSkills(Building building)
    {
        print("update skills");
        if (building != null && building.BuildingType == BuildingType.Barracks)
        {
            print("barracks");
            List<UnitType> possibleUnitTypes = UnitType.GetUnitTypes();
            for (int i = 0; i < unitDescriptionBoxes.Length; i++)
            {
                icons[i].gameObject.SetActive(true);
                unitDescriptionBoxes[i].setUnitType(possibleUnitTypes[i]);
                unitDescriptionBoxes[i].setText();
                icons[i].sprite = possibleUnitTypes[i].Icon;
            }
        }
        else
        {
            foreach (Image icon in icons)
            {
                icon.gameObject.SetActive(false);
            }
        }
    }
}
