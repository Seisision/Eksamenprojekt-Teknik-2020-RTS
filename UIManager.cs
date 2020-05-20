using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject PauseMenu;
    public GameObject ResourceView;
    public GameObject MiniMap;
    public GameObject BuildingView;
    public GameObject EndScreen;
    public GameState GameState;
    public GameObject WinText;
    public GameObject LoseText;

    

    bool Paused = false;

    // Start is called before the first frame update
    void Start()
    {
        MiniMap.SetActive(false);
        BuildingView.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseResumeButton();
        }

        if (GameState.GameRunning)
        {
            if (WinCondition() && Time.time > 30)
            {
                Debug.Log("You win");
                EndScreen.SetActive(true);
                WinText.SetActive(true);
            }
           if (LoseCondition() && Time.time > 30)
            {
                Debug.Log("You lose");
                EndScreen.SetActive(true);
                LoseText.SetActive(true);
            }
        }
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Application Terminated");
    }

    public void PauseResumeButton()
    {
        if (GameState.GameRunning)
            {

            if (Paused)
            {
                Paused = false;
                PauseMenu.SetActive(false);
            }
            else
            {
                Paused = true;
                PauseMenu.SetActive(true);
            }
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    public void StartButton()
    {
        if (!GameState.isReady)
        {
            return;
        }
        StartMenu.SetActive(false);
        Time.timeScale = 1;
        ResourceView.SetActive(true);
        MiniMap.SetActive(true);
        BuildingView.SetActive(true);
        GameState.StartGame();
    }

    public bool WinCondition()
    {
        print(GameState.AiPlayer.buildings.Count);
        return GameState.AiPlayer.units.Count(u => u.UnitType != UnitType.Builder) == 0 && GameState.AiPlayer.buildings.Count == 0;
    }

    public bool LoseCondition()
    {
        return GameState.HumanPlayer.units.Count(u => u.UnitType != UnitType.Builder) == 0 && GameState.HumanPlayer.buildings.Count == 0;
    }
}
