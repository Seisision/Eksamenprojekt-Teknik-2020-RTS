using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{
    public Select select;
    public UnitViewScript unitView;
    public BuildingButtons buildingView;
    public ResourceScript humanResource;
    public Unitspawner unitSpawner;
    public AIsetup setup;
    public EnemyAI enemyAi;

    public Transform BlueStart;
    public Transform RedStart;

    public Player AiPlayer;
    public Player HumanPlayer;    

    public bool isReady;

    public bool GameRunning;

    public void StartGame()
    {
        select.gameObject.SetActive(true);
        unitSpawner.SpawnBuilder(select.humanColour, GetStartPosFromColour(select.humanColour));
        unitSpawner.SpawnPlayerKing(select.humanColour, GetStartPosFromColour(select.humanColour));
        buildingView.Initialize();
        setup.Setup(GetStartPosFromColour(select.AIcolour));
        GameRunning = true;
    }

    public void SetHumanColourBlue()
    {
        SetHumanColour(playerColour.blue);
    }

    public void SetHumanColourRed()
    {
        SetHumanColour(playerColour.red);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SetHumanColour(playerColour colour)
    {
        select.SetHumanColour(colour);
        HumanPlayer = Player.GetPlayer(colour);
        AiPlayer = Player.GetOtherPlayer(colour);
        humanResource.humanPlayer = Player.GetPlayer(colour);
        humanResource.AIplayer = Player.GetOtherPlayer(colour);
        unitView.humanPlayer = Player.GetPlayer(colour);
        unitSpawner.humanColour = colour;
        unitSpawner.AIcolour = Player.GetOtherPlayer(colour).colour;
        enemyAi.humanPlayer = Player.GetPlayer(colour);
        enemyAi.AIplayer = Player.GetOtherPlayer(colour);
        Player.GetPlayer(colour).isHuman = true;
        Player.GetPlayer(colour).isAi = false;
        Player.GetOtherPlayer(colour).isAi = true;
        Player.GetOtherPlayer(colour).isHuman = false;
        SetResources();
        isReady = true;
    }

    public Transform  GetStartPosFromColour(playerColour colour)
    {
        if(colour == playerColour.blue)
        {
            return BlueStart;
        }
        if(colour == playerColour.red)
        {
            return RedStart;
        }
        return null;
    }


    public void SetResources()
    {
        humanResource.humanPlayer.PlayerGold = 100;
        humanResource.humanPlayer.PlayerMaxFood = 50;
        humanResource.humanPlayer.PlayerLumber = 75;

        humanResource.AIplayer.PlayerGold = 100;
        humanResource.AIplayer.PlayerMaxFood = 50;
        humanResource.AIplayer.PlayerLumber = 75;
    }

    // Start is called before the first frame update
    void Start()
    {
        setup = GetComponent<AIsetup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
