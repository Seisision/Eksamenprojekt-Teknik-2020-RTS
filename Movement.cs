using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public Unit unit;
    public EventSystem eventSystem;

    public float speed
    {
        get
        {
            return unit.movementSpeed;
        }
    }

    public Unit targetedUnit;
    

    public Vector3 targetPos;
    public Transform followUnit;

    void Start()
    {
        targetPos = this.transform.position;
        if (unit == null)
        {
            unit = GetComponent<Unit>();
        }
    }

    void Update()
    {
        if (CheckToMove())
        {
            MoveToPosition();
        }

        MoveToTarget();

        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, step);
        if(this.transform.position != targetPos)
        {
            Quaternion lookAtAngle = Quaternion.LookRotation(targetPos - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtAngle, 1f);
        }
        

        CheckIfTargetReached();
        CheckIfPositionReached();
    }

    bool CheckToMove()
    {
        if (!Input.GetMouseButtonUp(1)) return false;
        if (!unit.isSelected) return false;
        if (!Player.GetPlayer(unit.GetColour()).isHuman) return false;
        //If mouse is not over terrain or buildable terrain
        if (!(Finder.GetMousePosAtLayer("Terrain").Item2 || Finder.GetMousePosAtLayer("BuildableTerrain").Item2))
        {
            return false;
        }
        if (eventSystem.IsPointerOverGameObject()) return false;
        Unit unitUnderPointer = Finder.GetObject<Unit>("Unit");
        if (!(unitUnderPointer == null || Player.GetPlayer(unitUnderPointer.GetColour()).isHuman)) return false;
        return true;
    }

    public void MoveToTarget()
    {
        // target is the playtarget if possible else its autotarget
        var target = unit.PlayerTarget ?? unit.AutoTarget;
        if (target == null)
        {
            return;
        } 
        targetPos = target.GetTransform().position;
        if (Vector3.Distance(targetPos, this.transform.position) < unit.attackRange)
        {
            targetPos = this.transform.position;
        }
    }

    public void MoveToPosition()
    {
        // when you move to somewhere that isnt a target (fx right clicking on the ground) the target should be reset, as the unit is no longer fighting
        if(unit.PlayerTarget != null)
        {
            //unit.PlayerTarget.isSelected = false;
            unit.PlayerTarget = null;
        }
        if(unit.AutoTarget != null)
        {
            //unit.AutoTarget.isSelected = false;
            unit.AutoTarget = null;
        }

        // set targetPosition to mouse position if terrain is clicked
        targetPos = Finder.GetMousePosAtLayer("Terrain").Item2 ? Finder.GetMousePosAtLayer("Terrain").Item1 : Finder.GetMousePosAtLayer("BuildableTerrain").Item1;
        targetPos += new Vector3(0, 1.25f, 0);
    }

    public void CheckIfPositionReached()
    {
        // the unit should stop when reaching its destination
        if (Vector3.Distance(targetPos, this.transform.position) < 0.001f)
        {
            targetPos = this.transform.position;
        }
    }

    public void CheckIfTargetReached()
    {
        // the unit should stop when reaching its target
        if (unit.PlayerTarget != null)
        {
            if (Vector3.Distance(targetPos, this.transform.position) < unit.attackRange)
            {
                targetPos = this.transform.position;
            }
        }
        else if (unit.AutoTarget != null)
        {
            if (Vector3.Distance(targetPos, this.transform.position) < unit.attackRange)
            {
                targetPos = this.transform.position;
            }
        }
    }
}
