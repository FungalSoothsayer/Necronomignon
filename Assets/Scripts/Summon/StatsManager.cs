using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StatsManager : MonoBehaviour
{
    //sliders for each stat are stored in the following order: HP, Def, Power, Speed, Dex and XP
    public List<StatBar> statsList;
    public List<Text> statsValues;

    public static Beast currentBeast;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentBeast = BeastManager.beastsList.Beasts.Find(x => x.name == SummonManager.name);


        setBeastStats();
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
        statsList[0].setValue(currentBeast.maxHP);
        statsValues[0].text = currentBeast.maxHP.ToString();
        
        statsList[1].setValue(currentBeast.defence);
        statsValues[1].text = currentBeast.defence.ToString();
        
        statsList[2].setValue(currentBeast.power);
        statsValues[2].text = currentBeast.power.ToString();
        
        statsList[3].setValue(currentBeast.speed);
        statsValues[3].text = currentBeast.speed.ToString();
        
        statsList[4].setValue(currentBeast.dexterity);
        statsValues[4].text = currentBeast.dexterity.ToString();
    }
}
