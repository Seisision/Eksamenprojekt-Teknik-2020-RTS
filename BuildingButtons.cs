using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtons : MonoBehaviour
{
    BuildingCreator buildingCreator;

    public Button barracksButton;
    public Button farmButton;
    public Button goldMineButton;
    public Button lumberMillButton;

    public void Initialize()
    {
        buildingCreator = GameObject.Find("Player Builder Template(Clone)").GetComponent<BuildingCreator>();
        barracksButton.onClick.AddListener(buildingCreator.SelectBarracksForBuilding);
        farmButton.onClick.AddListener(buildingCreator.SelectFarmForBuilding);
        goldMineButton.onClick.AddListener(buildingCreator.SelectGoldMineForBuilding);
        lumberMillButton.onClick.AddListener(buildingCreator.SelectLumbermillForBuilding);
    }
}
