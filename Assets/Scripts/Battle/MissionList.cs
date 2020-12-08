
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionList : MonoBehaviour
{
    public string mission; 

    public BeastManager beastManager;

    public List<Beast> enemies = new List<Beast>();

    void Start()
    {
        mission = LevelChecker.lastClick;
    }
    //gets the beasts coresponding to the mission
    private void Awake()
    {
        
        mission = LevelChecker.lastClick;
        //the default and static mission
        if (mission == "sample")
        {
            enemies.Add(beastManager.getFromName("Dryad")); //A1
            enemies.Add(beastManager.getFromName("Conglomerate")); //B1
            enemies.Add(beastManager.getFromName("Wyvern")); //A2
            enemies.Add(null); //B2
            enemies.Add(null); //A3
            enemies.Add(beastManager.getFromName("Kitsune")); //B3
        }
        //the mission with 4 randomly placed random beast
        if (mission == "random")
        {
            List<int> beast = new List<int>();

            int ran = -1;

            while (beast.Count < Values.SQUADMAX)
            {
                //randomly picks a beast based on it's number in the list
                while (beast.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, BeastManager.beastsList.Beasts.Count);
                }
                beast.Add(ran);
            }
            beast.Add(-1);
            beast.Add(-1);

            ran = -1;

            List<int> position = new List<int>();

            while (enemies.Count < 6)
            {
                //loops random numbers that will go on to assigne to each beast a slot
                while (position.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, 6);
                }
                position.Add(ran);
                if (beast[ran] < 0)
                {
                    enemies.Add(null);
                }
                else
                {
                    enemies.Add(beastManager.getFromName(BeastManager.beastsList.Beasts[beast[ran]].name));
                }
            }
        }
        //the mission with 1-4 random beast with random placement 
        if (mission == "randomer")
        {
            List<int> beast = new List<int>();

            int ran = -1;

            int godhimself = Random.Range(1, Values.SQUADMAX+1);

            while (beast.Count < godhimself)
            {
                while (beast.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, BeastManager.beastsList.Beasts.Count);
                }
                beast.Add(ran);
            }
            while (beast.Count < 6)
            {
                beast.Add(-1);
            }

            ran = -1;

            List<int> position = new List<int>();

            while (enemies.Count < 6)
            {
                while (position.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, 6);
                }
                position.Add(ran);
                if (beast[ran] < 0)
                {
                    enemies.Add(null);
                }
                else
                {
                    enemies.Add(beastManager.getFromName(BeastManager.beastsList.Beasts[beast[ran]].name));
                }
            }
        }

    }
}
