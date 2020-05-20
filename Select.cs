using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
//using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.EventSystems;


public class Select : MonoBehaviour
{
    private bool isSelecting;
    private Vector3 startMousePos;
    public RectTransform SelectionBoxImage;
    public UnitViewScript UnitViewScript;

    public playerColour humanColour;
    public playerColour AIcolour;

    private Player humanPlayer;
    private Player AIplayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            TargetEnemyUnit();
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            SelectLeftClickDown();
        }
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            SelectLeftClickUp();
        }
        if (isSelecting)
        {
            DrawSelectionBox();
        }
        else
        {
            SelectionBoxImage.gameObject.SetActive(false);
        }
    }

    public void SelectUnit()
    {
        print("select unit");
        Unit unitTarget = Finder.GetObject<Unit>("Unit");
        if (unitTarget != null && unitTarget.GetColour() == humanPlayer.colour)
        {
            // if ctrl down add the selected unit to selectedPlayerUnits
            if (Input.GetKey(KeyCode.LeftControl))
            {
                unitTarget.isSelected = true;
                humanPlayer.selectedUnits.Add(unitTarget);
                UnitViewScript.gameObject.SetActive(true);
                if (humanPlayer.selectedUnits.Count == 1)
                {
                    UnitViewScript.ActivateUnitStatView();
                }
                if (humanPlayer.selectedUnits.Count > 1)
                {
                    UnitViewScript.ActivateMultiView();
                }
                return;
            }
            else // if ctrl not down ,then unselect all and select selected unit
            {
                UnselectUnitsBuilding();
                unitTarget.isSelected = true;
                humanPlayer.selectedUnits.Add(unitTarget);
                UnitViewScript.gameObject.SetActive(true);
                UnitViewScript.ActivateUnitStatView();
                return;
            }
        }

        Building buildingTarget = Finder.GetObject<Building>("Building");
        if(buildingTarget != null && buildingTarget.GetColour() == humanPlayer.colour)
        {
            // if ctrl down add the selected building to selectedPlayerBuildings
            if (Input.GetKey(KeyCode.LeftControl))
            {
                buildingTarget.isSelected = true;
                humanPlayer.selectedBuildings.Add(buildingTarget);
                return;
            }
            else //if ctrl not down, then unselect all  and select intended target
            {
                UnselectUnitsBuilding();
                buildingTarget.isSelected = true;
                humanPlayer.selectedBuildings.Add(buildingTarget);
                UnitViewScript.gameObject.SetActive(true);
                UnitViewScript.MultiView.SetActive(false);
                return;
            }
        }
        else
        {
            UnselectUnitsBuilding();
        }
    }

    public void SelectLeftClickDown()
    {
        if (isSelecting)
        {
            return;
        }
        isSelecting = true;
        startMousePos = Input.mousePosition;
    }

    public void SelectLeftClickUp()
    {
        if (!isSelecting)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isSelecting = false;
            return;
        }

        if(startMousePos == Input.mousePosition)
        {
            SelectUnit();
            SelectEnemyBuilding();
            SelectEnemyUnit();
            CheckDeSelect();
            isSelecting = false;
            return;
        }
        var left = Mathf.Min(startMousePos.x, Input.mousePosition.x);
        var right = Mathf.Max(startMousePos.x, Input.mousePosition.x);
        var bottom = Mathf.Max(startMousePos.y, Input.mousePosition.y);
        var top = Mathf.Min(startMousePos.y, Input.mousePosition.y);

        SelectUnits(left, right, bottom, top);

        isSelecting = false;
    }


    public void SelectUnits(float left, float right, float bottom, float top)
    {
        Camera camera = Camera.main;

        UnselectUnits();

        foreach (Unit unit in humanPlayer.units)
        {
            var viewPos = camera.WorldToScreenPoint(unit.transform.position);
            if (viewPos.x >= left && viewPos.x <= right && viewPos.y >= top && viewPos.y <= bottom)
            {
                unit.isSelected = true;
                humanPlayer.selectedUnits.Add(unit);
            }
        }

        foreach (Building building in humanPlayer.buildings)
        {
            var viewPos = camera.WorldToScreenPoint(building.transform.position);
            if (viewPos.x >= left && viewPos.x <= right && viewPos.y >= top && viewPos.y <= bottom)
            {
                building.isSelected = true;
                humanPlayer.selectedBuildings.Add(building);
            }
        }

        if(humanPlayer.selectedUnits.Count > 0 && humanPlayer.selectedUnits.Count >= humanPlayer.selectedBuildings.Count)
        {
            UnselectBuildings();
            UnitViewScript.gameObject.SetActive(true);
            if (humanPlayer.selectedUnits.Count == 1)
            {
                UnitViewScript.ActivateUnitStatView();
            }
            if (humanPlayer.selectedUnits.Count > 1)
            {
                UnitViewScript.ActivateMultiView();
            }
            
        }
        else
        {
            print("else what??");
            UnselectUnits();
            UnitViewScript.gameObject.SetActive(true);
            UnitViewScript.ActivateMultiView();
        }
    }

    public void TargetEnemyUnit()
    { 
        Unit enemyTarget = Finder.GetObject<Unit>("Unit");
        if (enemyTarget != null && enemyTarget.GetColour() == AIcolour)
        {
            UnselectEnemyUnitsBuilding();
            foreach (Unit playerUnit in humanPlayer.selectedUnits)
            {         
                playerUnit.PlayerTarget = enemyTarget;
                enemyTarget.isSelected = true;
            }
            return;
        }

        Building enemyTargetBuilding = Finder.GetObject<Building>("Building");
        if (enemyTargetBuilding != null && enemyTargetBuilding.GetColour() == AIcolour)
        {
            enemyTargetBuilding.isSelected = true;
            foreach(Unit playerUnit in humanPlayer.selectedUnits)
            {
                playerUnit.PlayerTarget = enemyTargetBuilding;
                enemyTargetBuilding.isSelected = true;
            }
        }
    }

    public void SelectEnemyUnit()
    {
        Unit selectedEnemy = Finder.GetObject<Unit>("Unit");
        if (selectedEnemy != null && selectedEnemy.GetColour() == AIplayer.colour)
        {
            UnselectEnemyUnitsBuilding();
            UnselectUnitsBuilding();
            selectedEnemy.isSelected = true;
            humanPlayer.selectedEnemyUnits.Add(selectedEnemy);
            UnitViewScript.gameObject.SetActive(true);
            UnitViewScript.ActivateUnitStatView();
        }
    }

    public void SelectEnemyBuilding()
    {
        Building selectedBuilding = Finder.GetObject<Building>("Building");
        if (selectedBuilding != null && selectedBuilding.GetColour() == AIplayer.colour)
        {
            UnselectEnemyUnitsBuilding();
            UnselectUnitsBuilding();
            selectedBuilding.isSelected = true;
            humanPlayer.selectedEnemyBuildings.Add(selectedBuilding);
            UnitViewScript.gameObject.SetActive(true);
            UnitViewScript.MultiView.SetActive(false);

        }
    }


    public void CheckDeSelect()
    {
        if (Finder.GetMousePosAtLayerOld("Terrain").Item2)
        {
            UnselectEnemyUnitsBuilding();
            UnselectUnitsBuilding();
        }
    }


    public void UnselectUnits()
    {
        print("unselecting units");
        foreach(Unit unit in humanPlayer.selectedUnits)
        {
            unit.isSelected = false;
        }
        humanPlayer.selectedUnits.Clear();
        UnitViewScript.gameObject.SetActive(false);
    }

    public void UnselectBuildings()
    {
        foreach (Building building in humanPlayer.selectedBuildings)
        {
            building.isSelected = false;
        }
        humanPlayer.selectedBuildings.Clear();
    }

    public void UnselectTargetedEnemyUnits()
    {
        foreach(Unit enemyUnit in humanPlayer.selectedEnemyUnits)
        {
            enemyUnit.isSelected = false;
        }
        humanPlayer.selectedEnemyUnits.Clear();

        foreach (Unit unit in humanPlayer.selectedUnits)
        {
            unit.AutoTarget = null;
        }
    }

    public void UnselectTargetedEnemyBuildings()
    {
        foreach (Building enemyBuilding in humanPlayer.selectedEnemyBuildings)
        {
            enemyBuilding.isSelected = false;
        }
        humanPlayer.selectedEnemyBuildings.Clear();
    }


    public void UnselectUnitsBuilding()
    {
        UnselectUnits();
        UnselectBuildings();
    }

    public void UnselectEnemyUnitsBuilding()
    {
        UnselectTargetedEnemyUnits();
        UnselectTargetedEnemyBuildings();
    }

    public void DrawSelectionBox()
    {
        SelectionBoxImage.gameObject.SetActive(true);
        var startPos = startMousePos;
        var currentPos = Input.mousePosition;

        var width = Mathf.Abs(startPos.x - currentPos.x);
        var height = Mathf.Abs(startPos.y - currentPos.y);

        SelectionBoxImage.anchoredPosition = new Vector2(Mathf.Min(startPos.x, currentPos.x), Mathf.Max(startPos.y, currentPos.y));
        SelectionBoxImage.sizeDelta = new Vector2(width, height);
    }

    public void SetHumanColour(playerColour colour)
    {
        if(colour == playerColour.red)
        {
            humanColour = playerColour.red;
            AIcolour = playerColour.blue;      
        }                                                  
        else if (colour == playerColour.blue)
        {
            humanColour = playerColour.blue;
            AIcolour = playerColour.red;
        }
        humanPlayer = Player.GetPlayer(humanColour);
        AIplayer = Player.GetOtherPlayer(humanColour);
    }
}
      
