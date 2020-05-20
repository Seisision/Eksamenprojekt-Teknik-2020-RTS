using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitType
{
    public string Name;
    public string Description;
    public float MovementSpeed;
    public int Damage;
    public float AttackSpeed;
    public int HitPointsMax;
    public int Armor;
    public int GoldCost;
    public int FoodCost;
    public float AttackRange;
    public float AggroRange;
    public Sprite Icon;

    public List<Ability> abilities;

    public static UnitType Hogrider;
    public static UnitType Headhunter;
    public static UnitType Grunt;
    public static UnitType Builder;
    public static UnitType Chieftain;

    public static void Create(IconsScript icons)
    {
        Grunt = new UnitType()
        {
            Name = "Grunt",
            Description = "Grunts are sturdy melee units.",
            MovementSpeed = 15,
            Damage = 25,
            AttackSpeed = 1,
            HitPointsMax = 250,
            Armor = 10,
            GoldCost = 5,
            FoodCost = 5,
            AttackRange = 10,
            AggroRange = 20,
            Icon = icons.InfantryIcon
        };

        Chieftain = new UnitType()
        {
            Name = "Chieftain",
            Description = "The chieftain is strong but he's not for sale.",
            MovementSpeed = 0,
            Damage = 200,
            AttackSpeed = 1.5f,
            HitPointsMax = 4000,
            Armor = 30,
            GoldCost = 0,
            FoodCost = 0,
            AttackRange = 30,
            AggroRange = 10,
            Icon = icons.KingIcon
        };
        Headhunter = new UnitType()
        {
            Name = "Headhunter",
            Description = "The headhunter is ranged but slow unit.",
            MovementSpeed = 7,
            Damage = 15,
            AttackSpeed = 0.75f,
            HitPointsMax = 75,
            Armor = 5,
            GoldCost = 5,
            FoodCost = 10,
            AttackRange = 150,
            AggroRange = 300,
            Icon = icons.LongbowmanIcon
        };
        Hogrider = new UnitType()
        {
            Name = "Hogrider",
            Description = "Hogriders units are fast.",
            MovementSpeed = 25,
            Damage = 70,
            AttackSpeed = 0.25f,
            HitPointsMax = 300,
            Armor = 3,
            GoldCost = 5,
            FoodCost = 15,
            AttackRange = 15,
            AggroRange = 30,
        Icon = icons.ScoutIcon,
        };

        Builder = new UnitType()
        {
            Name = "Builder",
            MovementSpeed = 0,
            HitPointsMax = 20,
            Armor = 5,
            GoldCost = 5,
            FoodCost = 2
        };
    }

    public static List<UnitType> GetUnitTypes()
    {
        List<UnitType> unitTypes = new List<UnitType> { UnitType.Grunt, UnitType.Headhunter, UnitType.Hogrider, UnitType.Chieftain };
        return unitTypes;
    }
}

