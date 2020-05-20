using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsScript : MonoBehaviour
{
    public Sprite InfantryIcon;
    public Sprite ScoutIcon;
    public Sprite LongbowmanIcon;
    public Sprite KingIcon;
    public Sprite BarrackIcon;
    public Sprite FarmIcon;
    public Sprite GoldMineIcon;
    public Sprite LumbermillIcon;


    // Start is called before the first frame update
    void Start()
    {
        UnitType.Create(this);
        BuildingType.Create(this);
    }
}
