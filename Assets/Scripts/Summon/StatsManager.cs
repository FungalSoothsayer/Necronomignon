using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class StatsManager : MonoBehaviour
{
    //sliders for each stat are stored in the following order: HP, Def, Power, Speed, Dex and XP
    public List<StatBar> statsList;
    public List<Text> statsValues;

    public static Beast currentBeast;

    private void Awake()
    {
        currentBeast = BeastManager.beastsList.Beasts.Find(x => x.name == SummonManager.name);
        setBeastStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        currentBeast = BeastManager.beastsList.Beasts.Find(x => x.name == SummonManager.name);
        setBeastStats();
        //setBeastStats();
        //statsList[1].incrementProgress(20);
        //Scene scene = SceneManager.GetActiveScene();
        //while (BeastSummon.beastName == null) { Debug.Log("no beast"); }
        Debug.Log("current beast = " + currentBeast.name);
    }

    // Update is called once per frame
    void Update(){}

    /*
     * takes the stats from currentBeast and sets the stat bars to their corresponding values
       to change or update the stat bar values, change the stats of the currentBeast object, then call setBeastStats()
     */
    
    public void setBeastStats() {
        //float temp = (float)currentBeast.maxHP + ((float)currentBeast.statGradients.hpGradient * (currentBeast.tier == 1? 1 : (currentBeast.tier - 1) * (float)Values.TEIRBOOST)) * Player.summoner.getLevel();

        int beastHealth = Convert.ToInt32(currentBeast.maxHP + (currentBeast.statGradients.hpGradient.getGradient(currentBeast.tier)) * (Player.summoner.getLevel() - 1));
        int beastdefence = Convert.ToInt32(currentBeast.defence + (currentBeast.statGradients.defenceGradient.getGradient(currentBeast.tier)) * (Player.summoner.getLevel() - 1) );
        int beastPower = Convert.ToInt32(currentBeast.power + (currentBeast.statGradients.powerGradient.getGradient(currentBeast.tier)) * (Player.summoner.getLevel() - 1));
        int beastSpeed = Convert.ToInt32(currentBeast.speed + (currentBeast.statGradients.speedGradient.getGradient(currentBeast.tier)) * (Player.summoner.getLevel() - 1));
        int beastDex = Convert.ToInt32(currentBeast.dexterity + (currentBeast.statGradients.dexGradient.getGradient(currentBeast.tier)) * (Player.summoner.getLevel() - 1));

        statsList[0].Value = beastHealth;
        statsValues[0].text = (((int)beastHealth).ToString());

        statsList[1].Value = beastdefence;
        statsValues[1].text = beastdefence.ToString();

        statsList[2].Value = beastPower;
        statsValues[2].text = beastPower.ToString();

        statsList[3].Value = beastSpeed;
        statsValues[3].text = beastSpeed.ToString();

        statsList[4].Value = beastDex;
        statsValues[4].text = beastDex.ToString();

        statsList[5].Value = Player.summoner.xp;
        statsValues[5].text = Player.summoner.getLevel().ToString();
    }
}
