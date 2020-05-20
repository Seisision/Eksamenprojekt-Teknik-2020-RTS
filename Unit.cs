using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatObject
{
    bool isSelected { get; set; }

    Transform GetTransform();
    void TakeDamage(int damage, Unit attackingUnit);
    playerColour GetColour();
}

public class Unit : MonoBehaviour, ICombatObject
{
    // type that determines stats and abilities
    public UnitType UnitType;

    public Animator UnitAnimator;

    //the unit that current is targeting
    public ICombatObject AutoTarget;
//    public Building AutoTargetBuilding;

    public ICombatObject PlayerTarget;
 //   public Building PlayerBuildingTarget;

    public GameObject selectedIndicator;

    public int LastAttacked;

    // the unit is red team
    public bool redTeam;

    // the unit is blue team
    public bool blueTeam;

    public bool isSelected { get; set; }

    // start position of the unit
    public Vector3 startPos;

    private Animator animator;


    //set icon from type
    public Sprite Icon
    {
        get
        {
            return UnitType.Icon;
        }
    }
    
    // set name from type
    public string Name
    {
        get
        {
            return UnitType.Name;
        }
    }

    // set goldcost from type
    public int GoldCost
    {
        get
        {
            return UnitType.GoldCost;
        }
    }

    // set food cost form type
    public int FoodCost
    {
        get
        {
            return UnitType.FoodCost;
        }
    }

    // set hp from type 
    public int HitPoints
    {
        get
        {
            return UnitType.HitPointsMax - damageTaken;
        }
    }

    public int armor
    {
        get
        {
            return UnitType.Armor;
        }
    }

    // set attack damage from type
    public int damage
    {
        get
        {
            return UnitType.Damage;
        }
    }

    // set attack speed from type
    public float attackSpeed
    {
        get
        {
            return UnitType.AttackSpeed;
        }
    }

    // set attack cooldown based on attack speed
    public float attackCooldown;


    // set speed from type
    public float movementSpeed
    {
        get
        {
            return UnitType.MovementSpeed;
        }
    }

    public void SetColour(playerColour colour)
    {
        if(colour == playerColour.blue)
        {
            this.blueTeam = true;
            this.redTeam = false;
        }
        else if(colour == playerColour.red)
        {
            this.redTeam = true;
            this.blueTeam = false;
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



    // attackrange set from type
    public float attackRange
    {
        get
        {
            return UnitType.AttackRange;
        }
    }

    // aggrorange set from type
    public float aggroRange
    {
        get
        {
            return UnitType.AggroRange;
        }
    }

    // use ability, checks cd
    public void UseAbility(Ability ability)
    {
        if (abilityLastUse.ContainsKey(ability))
        {
            // check cooldown
        }
    }

    // damageTaken used to calc hp
    public int damageTaken;

    // Set of values, key is abillity and value is the units cooldown for ability
    public Dictionary<Ability, float> abilityLastUse = new Dictionary<Ability, float>();

    public HashSet<Unit> GetEnemies()
    {
        return Player.GetOtherPlayer(GetColour()).units;
    }

    public HashSet<Building> GetEnemyBuildings()
    {
        return Player.GetOtherPlayer(GetColour()).buildings;
    }

    public ICombatObject FindNearbyTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Unit targetUnit = null;

        foreach (Unit enemyUnit in this.GetEnemies())
        {
            if(enemyUnit.UnitType == UnitType.Builder)
            {
                continue;
            }
            float dist = Vector3.Distance(this.transform.position, enemyUnit.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                targetUnit = enemyUnit;
            }
        }
        if (targetUnit != null && Vector3.Distance(targetUnit.transform.position, this.transform.position) < Mathf.Infinity)
        {
            return targetUnit;
        }
        else
        {
            return FindNearbyTargetBuilding();
        }
    }

    public Building FindNearbyTargetBuilding()
    {
        float shortestDistance = Mathf.Infinity;
        Building targetBuilding = null;

        foreach (Building enemyBuilding in this.GetEnemyBuildings())
        {
            float dist = Vector3.Distance(this.transform.position, enemyBuilding.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                targetBuilding = enemyBuilding;
            }
        }
        if (targetBuilding != null && Vector3.Distance(targetBuilding.transform.position, this.transform.position) < Mathf.Infinity)
        {
            targetBuilding.isSelected = true;
            return targetBuilding;
        }
        else
        {
            return null;
        }
    }

    public HashSet<Unit> GetAllies()
    {
        return Player.GetPlayer(GetColour()).units;
    }

    // Start is called before the first frame update
    void Start()
    {
        UnitAnimator = GetComponent<Animator>();
    }

    // Attack is called whenever the distance between unitPosition and targetUnit_position is less than attackRange
    void Attack(int damage, ICombatObject target, Unit thisUnit)
    {
        UnitAnimator.SetTrigger("Attack");

        if(thisUnit.GetColour() == target.GetColour())
        {
            return;
        }
        target.TakeDamage(damage, thisUnit);
        attackCooldown = 1 / attackSpeed;
    }

    // TakeDamage is called whenever a unit is attacked
    public void TakeDamage(int damage, Unit attackingUnit)
    {
        this.damageTaken += damage;
        if (CheckDeath())
        {
            Die();
            return;
        }
        if ((AutoTarget == null && !IsMoving()) || (AutoTarget is Building))
        {
            AutoTarget = attackingUnit;
        }
    }
    void Die()
    {
        UnitAnimator.SetTrigger("Die");
        Player.GetPlayer(this.GetColour()).units.Remove(this);
        Player.GetPlayer(this.GetColour()).selectedUnits.Remove(this);

        Player.GetOtherPlayer(this.GetColour()).selectedEnemyUnits.Remove(this);

        foreach (Unit unit in Player.GetOtherPlayer(this.GetColour()).units)
        {
            if (unit.PlayerTarget == this || unit.AutoTarget == this)
            {
                unit.PlayerTarget = null;
                unit.AutoTarget = null;
            }
        }

        Player.GetPlayer(this.GetColour()).PlayerCurrentFood -= this.FoodCost;
        Destroy(this.gameObject);
    }

    bool CheckDeath()
    {
        return (HitPoints <= 0);
    }

    bool CheckAttackRange()
    {
        return (PlayerTarget ?? AutoTarget) != null && attackRange >= Vector3.Distance((PlayerTarget ?? AutoTarget).GetTransform().position, transform.position);
    }

    bool CanAttack()
    {
        if (attackCooldown <= 0) return true;
        else return false;
        // check attack conditions
    }

    public bool IsMoving()
    {

        var pos = GetComponent<Movement>().targetPos;
        var dist = Vector3.Distance(pos, this.transform.position);
        if(dist < 0.1f)
        {
            return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving())
        {
            UnitAnimator.SetBool("isMoving", true);
            UnitAnimator.SetBool("isIdle", false);
        }
        else
        {
            UnitAnimator.SetBool("isMoving", false);
            UnitAnimator.SetBool("isIdle", true);
        }
        if (selectedIndicator != null)
        {
            if (isSelected)
            {
                selectedIndicator.SetActive(true);
            }
            else
            {
                selectedIndicator.SetActive(false);
            }
        }
       

        if (attackCooldown > 0) attackCooldown-=Time.deltaTime;

        if (PlayerTarget != null)
        {
            if (CheckAttackRange() && CanAttack())
            {
               Attack(damage, PlayerTarget, this);
            }
        }
        else if (AutoTarget != null)
        {
            if(CheckAttackRange() && CanAttack())
            {
                Attack(damage, AutoTarget, this);
            }
        }
        else if(!IsMoving())
        {
           AutoTarget = FindNearbyTarget();
           
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
