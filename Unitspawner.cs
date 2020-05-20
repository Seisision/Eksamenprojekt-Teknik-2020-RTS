using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unitspawner : MonoBehaviour
{
    public Material blueMaterial;
    public Material redMaterial;
    public Unit PlayerInfantryTemplate;
    public Unit PlayerHeadhunterTemplate;
    public Unit PlayerKingTemplate;
    public Unit EnemyKingTemplate;
    public Unit PlayerCavalryTemplate;
    public Unit BuilderTemplate;
    public Unit EnemyInfantryTemplate;
    public Unit EnemyHeadhunterTemplate;
    public Unit EnemyCavalryTemplate;
    public playerColour humanColour;
    public playerColour AIcolour;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("a"))
        {
            SpawnUnit(Player.GetPlayer(humanColour).selectedBuildings, UnitType.Grunt, PlayerInfantryTemplate, humanColour);
        }
        if (Input.GetKeyUp("s"))
        {
            SpawnUnit(Player.GetPlayer(humanColour).selectedBuildings, UnitType.Hogrider, PlayerCavalryTemplate, humanColour);
        }
        if (Input.GetKeyUp("d"))
        {
            SpawnUnit(Player.GetPlayer(humanColour).selectedBuildings, UnitType.Headhunter, PlayerHeadhunterTemplate, humanColour);
        }
        if (Input.GetKeyUp("h"))
        {
            SpawnUnit(Player.GetPlayer(AIcolour).selectedBuildings, UnitType.Grunt, EnemyInfantryTemplate, AIcolour);
        }
        if (Input.GetKeyUp("j"))
        {
            SpawnUnit(Player.GetPlayer(AIcolour).selectedBuildings, UnitType.Hogrider, EnemyCavalryTemplate, AIcolour);
        }
        if (Input.GetKeyUp("k"))
        {
            SpawnUnit(Player.GetPlayer(AIcolour).selectedBuildings, UnitType.Headhunter, EnemyHeadhunterTemplate, AIcolour);
        }
    }

    public void SpawnUnit(HashSet<Building> buildings, UnitType unitType, Unit UnitTemplate, playerColour colour)
    {
        print("spawn unit called");
        foreach (Building building in buildings)
        {
            if (Player.GetPlayer(colour).CheckPlayerGoldAndFood(unitType))
            {
                building.spawnUnit(unitType, UnitTemplate, colour, GetMaterialFromColour(colour));
            }
        }
    }

    public void SpawnInfantry(Player player, playerColour colour)
    {
        SpawnUnit(player.buildings, UnitType.Grunt, EnemyInfantryTemplate, colour);
    }

    public void SpawnPlayerKing(playerColour colour, Transform position)
    {
        Unit king = Instantiate(PlayerKingTemplate, PlayerKingTemplate.transform.parent);
        king.UnitType = UnitType.Chieftain;
        king.GetComponentInChildren<SkinnedMeshRenderer>().material = GetMaterialFromColour(colour);
        king.gameObject.SetActive(true);
        Player.GetPlayer(colour).units.Add(king);
        king.SetColour(colour);
        king.transform.position = position.position;
        king.transform.Translate(0f, 2.5f, 0f);
    }

    public void SpawnEnemyKing(playerColour colour, Transform position)
    {
        Unit king = Instantiate(EnemyKingTemplate, EnemyKingTemplate.transform.parent);
        king.UnitType = UnitType.Chieftain;
        king.GetComponentInChildren<SkinnedMeshRenderer>().material = GetMaterialFromColour(colour);
        king.gameObject.SetActive(true);
        Player.GetPlayer(colour).units.Add(king);
        king.SetColour(colour);
        king.transform.position = position.position;
        king.transform.Translate(0f, 2.5f, 0f);
    }

    public void SpawnCavalry(Player player, playerColour colour)
    {
        SpawnUnit(player.buildings, UnitType.Hogrider, EnemyCavalryTemplate, colour);
    }

    public void SpawnHeadhunter(Player player, playerColour colour)
    {
        SpawnUnit(player.buildings, UnitType.Headhunter, EnemyHeadhunterTemplate, colour);
    }

    public void SpawnBuilder(playerColour colour, Transform position)
    {
        Unit builderunit = Instantiate(BuilderTemplate, BuilderTemplate.transform.parent);
        builderunit.UnitType = UnitType.Builder;
        builderunit.GetComponent<MeshRenderer>().material = GetMaterialFromColour(colour);
        builderunit.gameObject.SetActive(true);
        Player.GetPlayer(colour).units.Add(builderunit);
        builderunit.SetColour(colour);
        builderunit.transform.position = position.position;
    }

    public Material GetMaterialFromColour(playerColour colour)
    {
        if(colour == playerColour.blue)
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
