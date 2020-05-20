using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceScript : MonoBehaviour
{
    public GameState GameState;
    public Text GoldText;
    public Text FoodText;
    public Text LumberText;
    public Player humanPlayer;
    public Player AIplayer;
    
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
            GoldText.text = humanPlayer.PlayerGold.ToString();
            LumberText.text = humanPlayer.PlayerLumber.ToString();
            FoodText.text = string.Format("{0}/{1}", humanPlayer.PlayerCurrentFood.ToString(), humanPlayer.PlayerMaxFood.ToString());

            if (humanPlayer.IncomeCooldown > 0)
            {
                humanPlayer.IncomeCooldown -= Time.deltaTime;
            }
            if (humanPlayer.IncomeCooldown <= 0) income(humanPlayer);

            if (AIplayer.IncomeCooldown > 0)
            {
                AIplayer.IncomeCooldown -= Time.deltaTime;
            }
            if (AIplayer.IncomeCooldown <= 0) income(AIplayer);
    }

    public void income(Player player)
    {
        foreach (Building building in player.buildings)
        {
            player.PlayerGold += building.Income;
            player.PlayerLumber += building.LumberProduction;
        }
        player.IncomeCooldown = 1 / player.IncomeRate;
    }
}
