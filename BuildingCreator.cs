using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour
{
    public Material blueMaterial;
    public Material redMaterial;

    public Unit builder;
    public Building BarracksTemplate;
    public Building EnemyBarracksTemplate;
    public Building FarmTemplate;
    public Building GoldMineTemplate;
    public Building LumbermillTemplate;

    public Blueprint BarracksBlueprint;
    public Blueprint FarmBlueprint;
    public Blueprint GoldMineBlueprint;
    public Blueprint LumbermillBlueprint;

    public Building CurrentBuildingTemplate;
    public Blueprint CurrentBuildingBlueprint;
    public BuildingType CurrentBuildingType;

    // Update is called once per frame
    void Update()
    {
        if (builder == null || Player.GetPlayer(builder.GetColour()).isAi)
        {
            return;
        }
        if (Input.GetKeyDown("b"))
        {
            SelectBarracksForBuilding();
        }
        if (Input.GetKeyDown("f"))
        {
            SelectFarmForBuilding();
        }
        if (Input.GetKeyDown("g"))
        {
            SelectGoldMineForBuilding();
        }
        if (Input.GetKeyDown("l"))
        {
            SelectLumbermillForBuilding();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(CurrentBuildingBlueprint != null && CurrentBuildingBlueprint.canBuild)
            {
                CurrentBuildingBlueprint.gameObject.SetActive(false);
                CurrentBuildingBlueprint = null;
                BuildBuilding(CurrentBuildingType, CurrentBuildingTemplate, builder.GetColour());
            }
        }

        if (Input.GetKeyUp("e"))
        {
            //BuildEnemyBarracks();
        }

        if (CurrentBuildingBlueprint != null)
        {
            (Vector3 newPosTerrain, bool hitTerrain) = Finder.GetMousePosAtLayer("Terrain");
            (Vector3 newPosBuildableTerrain, bool hitBuildableTerrain) = Finder.GetMousePosAtLayer("BuildableTerrain");
            if (hitTerrain || hitBuildableTerrain)
            {
                //Move the blueprint to the hit position
                CurrentBuildingBlueprint.transform.position = newPosTerrain != new Vector3() ? newPosTerrain : newPosBuildableTerrain;
                CurrentBuildingBlueprint.transform.rotation = Quaternion.identity;
            } 
            if (hitBuildableTerrain && CurrentBuildingBlueprint.canBuild)
            {
                CurrentBuildingBlueprint.rend.material = CurrentBuildingBlueprint.canBuildMaterial;
            }
            else
            {
                CurrentBuildingBlueprint.rend.material = CurrentBuildingBlueprint.canNotBuildMaterial;
            }


        }

    }

    public void SelectBarracksForBuilding()
    {
        CurrentBuildingTemplate = BarracksTemplate;
        CurrentBuildingBlueprint = BarracksBlueprint;
        CurrentBuildingBlueprint.gameObject.SetActive(true);
        CurrentBuildingType = BuildingType.Barracks;
    }

    public void SelectFarmForBuilding()
    {
        CurrentBuildingTemplate = FarmTemplate;
        CurrentBuildingBlueprint = FarmBlueprint;
        CurrentBuildingBlueprint.gameObject.SetActive(true);
        CurrentBuildingType = BuildingType.Farm;
    }

    public void SelectGoldMineForBuilding()
    {
        CurrentBuildingTemplate = GoldMineTemplate;
        CurrentBuildingBlueprint = GoldMineBlueprint;
        CurrentBuildingBlueprint.gameObject.SetActive(true);
        CurrentBuildingType = BuildingType.GoldMine;
    }

    public void SelectLumbermillForBuilding()
    {
        CurrentBuildingTemplate = LumbermillTemplate;
        CurrentBuildingBlueprint = LumbermillBlueprint;
        CurrentBuildingBlueprint.gameObject.SetActive(true);
        CurrentBuildingType = BuildingType.LumberMill;
    }


    public void BuildBuilding(BuildingType buildingType, Building buildingTemplate, playerColour colour)
    {
        if (Player.GetPlayer(colour).CheckPlayerGoldAndLumber(buildingType))
        {
            var building = Instantiate(buildingTemplate, buildingTemplate.transform.parent);
            building.BuildingType = buildingType;
            (Vector3 pos, bool hit) = Finder.GetMousePosAtLayer("BuildableTerrain");
            if (hit && Player.GetPlayer(colour).isHuman)
            {
               building.transform.position = pos;
               building.transform.Translate(0, 1.25f, 0);
            }
            else if(!hit && Player.GetPlayer(colour).isHuman)
            {
                Debug.Log("You no buildings can here fam");
                return;
            }
            else if(Player.GetPlayer(colour).isAi)
            {
                // set building transform to some designated area in the bots base.
            }
            building.GetComponent<MeshRenderer>().material = GetMaterialFromColour(colour);
            building.gameObject.SetActive(true);
            building.SetColour(colour);
            Player.GetPlayer(colour).UpdatePlayerGoldAndLumber(buildingType);
            Player.GetPlayer(colour).CheckFarm(buildingType);
            Player.GetPlayer(colour).buildings.Add(building);
        }
        else
        {
            return;
        }
    }

    // build barrack
    public void BuildBarracks(playerColour colour)
    {
        BuildBuilding(BuildingType.Barracks, EnemyBarracksTemplate, colour);
    }

    public Material GetMaterialFromColour(playerColour colour)
    {
        if (colour == playerColour.blue)
        {
            return blueMaterial;
        }
        if (colour == playerColour.red)
        {
            return redMaterial;
        }
        return null;
    }
}
