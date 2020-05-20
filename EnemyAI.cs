using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    public Player humanPlayer;
    public Player AIplayer;
    public Unitspawner unitSpawner;
    System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
     //   unitSpawner.SpawnKing(AIplayer, AIplayer.colour);
    }

    // Update is called once per frame
    void Update()
    {
        Think();
    }

    public void Think()
    {
        bool result = random.Next(0, 2) != 0;
        if (AIplayer == null)
        {
            return;
        }
        if (!AIplayer.buildings.Any(u => u.BuildingType == BuildingType.Barracks))
        {
            var builder = AIplayer.units.FirstOrDefault(u => u.UnitType == UnitType.Builder);
            if(builder != null)
            {
                var buildingCreator = builder.GetComponent<BuildingCreator>();
                if(buildingCreator != null)
                {
                    buildingCreator.BuildBarracks(AIplayer.colour);
                }
            }
        }
        else
        {
            if (result)
            {
                unitSpawner.SpawnInfantry(AIplayer, AIplayer.colour);
            }
            else
            {
                unitSpawner.SpawnCavalry(AIplayer, AIplayer.colour);
            }
        }
    }
}
