using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingType
{
    public string Name;
    public int GoldCost;
    public int LumberCost;
    public int Income;
    public int FoodProduction;
    public int LumberProduction;
    public int HitPointsMax;
    public Sprite Icon;

    public static void Create(IconsScript iconscript)
    {
        Barracks = new BuildingType()
        {
            Name = "Barracks",
            GoldCost = 25,
            LumberCost = 1,
            HitPointsMax = 25,
            Icon = iconscript.BarrackIcon,
        };

        GoldMine = new BuildingType()
        {
            Name = "Gold Mine",
            GoldCost = 25,
            LumberCost = 20,
            Income = 1,
            HitPointsMax = 25,
            Icon = iconscript.GoldMineIcon,
         };

        Farm = new BuildingType()
        {
            Name = "Farm",
            GoldCost = 25,
            LumberCost = 20,
            FoodProduction = 5,
            HitPointsMax = 25,
            Icon = iconscript.FarmIcon,
        };

        LumberMill = new BuildingType()
        {
            Name = "Lumber Mill",
            GoldCost = 10,
            LumberCost = 15,
            LumberProduction = 1,
            HitPointsMax = 25,
            Icon = iconscript.LumbermillIcon,
        };
    }
    public static BuildingType Barracks;

    public static BuildingType GoldMine;

    public static BuildingType Farm;

    public static BuildingType LumberMill;
}
