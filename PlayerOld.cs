using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

 
public static class PlayerOld
{
    // all player units
    public static HashSet<Unit> playerUnits = new HashSet<Unit>();
    public static HashSet<Unit> blueUnits = new HashSet<Unit>();
    public static HashSet<Unit> redUnits = new HashSet<Unit>();

    public static HashSet<Building> buildings = new HashSet<Building>();
    public static HashSet<Building> blueBuildings = new HashSet<Building>();
    public static HashSet<Building> redBuildings = new HashSet<Building>();

    public static HashSet<Unit> selectedPlayerUnits = new HashSet<Unit>();
    public static HashSet<Building> selectedPlayerBuildings= new HashSet<Building>();
    public static int PlayerGold = 0;
    public static int PlayerCurrentFood = 0;
    public static int PlayerMaxFood = 0;
    public static int PlayerLumber = 0;
    public static float IncomeRate = 0.5f;
    public static float IncomeCooldown = 0;

    public static bool CheckPlayerGoldAndFood(UnitType unitType)
    {
        if(PlayerOld.PlayerGold - unitType.GoldCost >= 0 && PlayerOld.PlayerCurrentFood + unitType.FoodCost <= PlayerMaxFood)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckPlayerGoldAndLumber(BuildingType buildingType)
    {
        if (PlayerOld.PlayerGold - buildingType.GoldCost >= 0 && PlayerOld.PlayerLumber - buildingType.LumberCost >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void UpdatePlayerGoldAndFood(UnitType unitType)
    {
        PlayerOld.PlayerGold -= unitType.GoldCost;
        PlayerOld.PlayerCurrentFood += unitType.FoodCost;
    }

    public static void UpdatePlayerGoldAndLumber(BuildingType buildingType)
    {
        PlayerOld.PlayerGold -= buildingType.GoldCost;
        PlayerOld.PlayerLumber -= buildingType.LumberCost;
    }

    public static void CheckFarm(BuildingType buildingType)
    {
        if(buildingType == BuildingType.Farm)
        {
            PlayerOld.PlayerMaxFood += buildingType.FoodProduction;
        }
        else
        {
            return;
        }
    }

    public static bool IsPlayer(playerColour colour)
    {
        return (colour == playerColour.blue);
    }

    // returns true if the 2 units are on the same team
    public static bool CompareTeam(Unit thisUnit, Unit otherUnit)
    {
       if(thisUnit.blueTeam && otherUnit.blueTeam)
        {
            return true;
        }
       if(thisUnit.redTeam && otherUnit.redTeam)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
