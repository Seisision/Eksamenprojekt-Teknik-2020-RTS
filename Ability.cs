using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ability
{
    public string name;

    public string description;

    public Action<Unit> useHandler;

    public float cooldown;

    public float currentCooldown;

    public Sprite icon;

    public static Ability FireballAbility = new Ability { useHandler = FireballHandler, cooldown = 5f };

    public static Ability LightningBoltAbility = new Ability { useHandler = LightningBoltHandler, cooldown = 3f };

    public static void FireballHandler(Unit target)
    {

    }

    public static void LightningBoltHandler(Unit taget)
    {

    }

    

   
}