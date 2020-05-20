using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, ICombatObject
{
    //type that determines the attributes of the building
    public BuildingType BuildingType;
    public GameObject selectedIndicator;

    public bool IsAI;

    // the unit is red team
    public bool redTeam;

    // the unit is blue team
    public bool blueTeam;

    public bool isSelected { get; set; }

    // damageTaken used to calc hp
    public int damageTaken;

    public string Name
    {
        get
        {
            return BuildingType.Name;
        }
    }

    public int HitPointsMax
    {
        get
        {
            return BuildingType.HitPointsMax;
        }
    }

    public int HitPoints
    {
        get
        {
            return BuildingType.HitPointsMax - damageTaken;
        }
    }

    public int GoldCost
    {
        get
        {
            return BuildingType.GoldCost;
        }
    }

    public int LumberCost
    {
        get
        {
            return BuildingType.LumberCost;
        }
    }

    public int Income
    {
        get
        {
            return BuildingType.Income;
        }
    }
    public int LumberProduction

    {
        get
        {
            return BuildingType.LumberProduction;
        }
    }

    public playerColour GetColour()
    {
        if (this.blueTeam)
        {
            return playerColour.blue;
        }
        if (this.redTeam)
        {
            return playerColour.red;
        }
        return playerColour.undefined;
    }

    public void TakeDamage(int damage, Unit attackingUnit)
    {
        this.damageTaken += damage;
        if (CheckDeath())
        {
            Die();
            return;
        }
    }

    void Die()
    {
        Player.GetPlayer(this.GetColour()).buildings.Remove(this);
        Player.GetPlayer(this.GetColour()).selectedBuildings.Remove(this);

        Player.GetOtherPlayer(this.GetColour()).selectedEnemyBuildings.Remove(this);

        foreach (Unit unit in Player.GetOtherPlayer(this.GetColour()).units)
        {
            if (unit.AutoTarget == this || unit.PlayerTarget == this)
            {
                unit.PlayerTarget = null;
                unit.AutoTarget = null;
            }
        }
        Destroy(this.gameObject);
    }

    bool CheckDeath()
    {
        return (HitPoints <= 0);
    }

    public void SetColour(playerColour colour)
    {
        if (colour == playerColour.blue)
        {
            this.blueTeam = true;
            this.redTeam = false;
        }
        else if (colour == playerColour.red)
        {
            this.redTeam = true;
            this.blueTeam = false;
        }
    }

    public void spawnUnit(UnitType unitType, Unit UnitTemplate, playerColour colour, Material material)
    {
        if (BuildingType == BuildingType.Barracks)
        {
            var SpawnedUnit = Instantiate(UnitTemplate, UnitTemplate.transform.parent);
            SpawnedUnit.UnitType = unitType;
            SpawnedUnit.transform.position = this.transform.position + new Vector3(0, 0, -10);
            if (material != null)
            {
                SpawnedUnit.GetComponent<MeshRenderer>().material = material;
                SpawnedUnit.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
            }
            SpawnedUnit.SetColour(colour);
            SpawnedUnit.gameObject.SetActive(true);
            Player.GetPlayer(colour).UpdatePlayerGoldAndFood(unitType);
            Player.GetPlayer(colour).units.Add(SpawnedUnit.GetComponent<Unit>());
            SpawnedUnit.transform.Translate(0, 1f, 0);
        }
    }

    void Update()
    {
        if (selectedIndicator != null)
        {
            if (this.isSelected)
            {
                selectedIndicator.SetActive(true);
            }
            else
            {
                selectedIndicator.SetActive(false);
            }
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
