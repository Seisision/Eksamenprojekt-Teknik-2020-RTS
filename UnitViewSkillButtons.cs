using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UnitViewSkillButtons : MonoBehaviour
{
    public Unitspawner unitSpawner;

    public Button SkillButton1;
    public Button SkillButton2;
    public Button SkillButton3;
    public Button SkillButton4;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SkillButton1.onClick.AddListener(() => unitSpawner.SpawnUnit(Player.GetPlayer(unitSpawner.humanColour).selectedBuildings, UnitType.Grunt, unitSpawner.PlayerInfantryTemplate, unitSpawner.humanColour));
        SkillButton2.onClick.AddListener(() => unitSpawner.SpawnUnit(Player.GetPlayer(unitSpawner.humanColour).selectedBuildings, UnitType.Headhunter, unitSpawner.PlayerHeadhunterTemplate, unitSpawner.humanColour));
        SkillButton3.onClick.AddListener(() => unitSpawner.SpawnUnit(Player.GetPlayer(unitSpawner.humanColour).selectedBuildings, UnitType.Hogrider, unitSpawner.PlayerCavalryTemplate, unitSpawner.humanColour));
        //SkillButton4.onClick.AddListener(() => unitSpawner.SpawnUnit(Player.GetPlayer(unitSpawner.humanColour).selectedBuildings, UnitType.Chieftain, unitSpawner.PlayerKingTemplate, unitSpawner.humanColour));
    }
}
