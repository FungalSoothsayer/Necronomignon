using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionList : MonoBehaviour
{
    public string mission; //Set in inspector

    public BeastManager beastManager;

    public List<Beast> enemies = new List<Beast>();

    private void Awake()
    {
        if(mission == "sample")
        {
            enemies.Add(beastManager.getFromName("Sunbather")); //A1
            enemies.Add(beastManager.getFromName("Gaia")); //B1
            enemies.Add(beastManager.getFromName("Cthulhu")); //A2
            enemies.Add(null); //B2
            enemies.Add(null); //A3
            enemies.Add(beastManager.getFromName("Trogdor")); //B3
        }
    }

}
