using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UnitViewScript : MonoBehaviour
{
    public Text HPText;
    public Text ManaText;
    public Text DamageText;
    public Text AttackSpeedText;
    public Text ArmorText;
    public Image UnitImage;
    public GameObject UnitStatView;
    public GameObject MultiView;
    public SkillView skillView;

    public GameObject[] Icons;
    public HealthBar[] HealthBars;
    public Player humanPlayer;

    

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        skillView.updateSkills(humanPlayer.selectedBuildings.FirstOrDefault());

        if (humanPlayer.selectedUnits.Count == 1)
        {
            Debug.Log("kappa");
            if (!UnitStatView.activeSelf)
            {
                Debug.Log("boi");
                ActivateUnitStatView();
            }
            setMainUnitView(humanPlayer.selectedUnits.FirstOrDefault());
            setUnitStatView(humanPlayer.selectedUnits.First());
            HPText.color = Color.green;
        }
        else if (humanPlayer.selectedEnemyUnits.Count == 1)
        {
            setMainUnitView(humanPlayer.selectedEnemyUnits.FirstOrDefault());
            setUnitStatView(humanPlayer.selectedEnemyUnits.First());
            HPText.color = Color.red;
        }
        else if (humanPlayer.selectedEnemyBuildings.Count == 1)
        {
            setMainBuildingView(humanPlayer.selectedEnemyBuildings.FirstOrDefault());
            HPText.color = Color.red;
        }
        else if (humanPlayer.selectedBuildings.Count == 1)
        {
            setMainBuildingView(humanPlayer.selectedBuildings.FirstOrDefault());
            HPText.color = Color.green;
        }
        if (humanPlayer.selectedUnits.Count > 1)
        {
            setMainUnitView(humanPlayer.selectedUnits.FirstOrDefault());
            setMultiUnitView();
        }
        
        if (humanPlayer.selectedBuildings.Count > 1)
        {
            setMainBuildingView(humanPlayer.selectedBuildings.FirstOrDefault());
            setMultiBuildingView();
        }

    }
    void setMainUnitView(Unit unit)
    {
        if (unit != null)
        {
            string currentHitPointsText = unit.HitPoints > 0 ? unit.HitPoints.ToString() : "0";
            HPText.text = string.Format("{0}/{1}", currentHitPointsText, unit.UnitType.HitPointsMax.ToString());
            //ManaText.text = string.Format("{0}/{1}", unit.HitPoints.ToString(), unit.UnitType.HitPointsMax.ToString());
            UnitImage.sprite = unit.Icon;
        }
    }

    void setMainBuildingView(Building building)
    {
        if(building != null)
        {
            string currentHitPointsText = building.HitPoints > 0 ? building.HitPoints.ToString() : "0";
            HPText.text = string.Format("{0}/{1}", currentHitPointsText, building.BuildingType.HitPointsMax.ToString());
            UnitImage.sprite = building.BuildingType.Icon;
        }
    }

    public void setUnitStatView(Unit unit)
    {
        DamageText.text = string.Format("DMG:{0}", unit.damage.ToString());
        AttackSpeedText.text = string.Format("AS:{0}", unit.attackSpeed.ToString());
        ArmorText.text = string.Format("AR:{0}", unit.armor.ToString());
    }

    void setMultiUnitView()
    {

        for (int j = 0; j < Icons.Length; j++)
        {
            Icons[j].SetActive(j < humanPlayer.selectedUnits.Count);
            HealthBars[j].gameObject.SetActive(j < humanPlayer.selectedUnits.Count);
        }

        int i = 0;
        foreach (Unit unit in humanPlayer.selectedUnits)
        {
            Icons[i].GetComponent<Image>().sprite = unit.Icon;
            HealthBars[i].SetMaxHitPoints(unit.UnitType.HitPointsMax);
            HealthBars[i].SetHitPoints(unit.HitPoints);
            HealthBars[i].UpdateColor((float)unit.HitPoints / unit.UnitType.HitPointsMax);
            i++;
        }        
    }

    void setMultiBuildingView()
    {
        for (int j = 0; j < Icons.Length; j++)
        {
            Icons[j].SetActive(j < humanPlayer.selectedBuildings.Count);
            HealthBars[j].gameObject.SetActive(j < humanPlayer.selectedBuildings.Count);
        }

        int i = 0;
        foreach (Building building in humanPlayer.selectedBuildings)
        {
            Icons[i].GetComponent<Image>().sprite = building.BuildingType.Icon;
            HealthBars[i].SetMaxHitPoints(building.BuildingType.HitPointsMax);
            HealthBars[i].SetHitPoints(building.HitPoints);
            HealthBars[i].UpdateColor((float)building.HitPoints / building.BuildingType.HitPointsMax);
            i++;
        }
    }

    public void ActivateUnitStatView()
    {
        MultiView.SetActive(false);
        UnitStatView.SetActive(true);
    }

    public void ActivateMultiView()
    {
        UnitStatView.SetActive(false);
        MultiView.SetActive(true);
    }

    
}
