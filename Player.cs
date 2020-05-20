using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    public class Player
    {
        public bool isAi;
        public bool isHuman;

        public static Player bluePlayer = new Player(playerColour.blue);
        public static Player redPlayer = new Player(playerColour.red);

        public HashSet<Unit> units = new HashSet<Unit>();
        public HashSet<Unit> selectedUnits = new HashSet<Unit>();
        public HashSet<Unit> selectedEnemyUnits = new HashSet<Unit>();


        public HashSet<Building> buildings = new HashSet<Building>();
        public HashSet<Building> selectedBuildings = new HashSet<Building>();
        public HashSet<Building> selectedEnemyBuildings = new HashSet<Building>();


    public playerColour colour;

        public int PlayerGold = 0;
        public int PlayerCurrentFood = 0;
        public int PlayerMaxFood = 0;
        public int PlayerLumber = 0;
        public float IncomeRate = 0.5f;
        public float IncomeCooldown = 0;


    public static Player GetPlayer(playerColour colour)
    {
        if(colour == playerColour.blue)
        {
            return bluePlayer;
        }
        if(colour == playerColour.red)
        {
            return redPlayer;
        }
        return null;
    }

    public static Player GetOtherPlayer(Player player)
    {
        if (player == redPlayer)
        {
            return bluePlayer;
        }
        if (player == bluePlayer)
        {
            return redPlayer;
        }
        return null;
    }

    public static Player GetOtherPlayer(playerColour colour)
    {
        if (colour == playerColour.red)
        {
            return bluePlayer;
        }
        if (colour == playerColour.blue)
        {
            return redPlayer;
        }
        return null;
    }



    public Player(playerColour colour)
        {
            this.colour = colour;
        }

    public bool CheckPlayerGoldAndFood(UnitType unitType)
    {
        if (PlayerGold - unitType.GoldCost >= 0 && PlayerCurrentFood + unitType.FoodCost <= PlayerMaxFood)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckPlayerGoldAndLumber(BuildingType buildingType)
    {
        if (PlayerGold - buildingType.GoldCost >= 0 && PlayerLumber - buildingType.LumberCost >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public  void UpdatePlayerGoldAndFood(UnitType unitType)
    {
        PlayerGold -= unitType.GoldCost;
        PlayerCurrentFood += unitType.FoodCost;
    }

    public  void UpdatePlayerGoldAndLumber(BuildingType buildingType)
    {
        PlayerGold -= buildingType.GoldCost;
        PlayerLumber -= buildingType.LumberCost;
    }

    public  void CheckFarm(BuildingType buildingType)
    {
        if (buildingType == BuildingType.Farm)
        {
            PlayerMaxFood += buildingType.FoodProduction;
        }
        else
        {
            return;
        }
    }
}

