using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIsetup : MonoBehaviour
{
    public Unitspawner unitSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // todo difficulty
    public void Setup(Transform spawnPos)
    {
       unitSpawner.SpawnBuilder(unitSpawner.AIcolour, spawnPos);
       unitSpawner.SpawnEnemyKing(unitSpawner.AIcolour, spawnPos);
    }
}
