using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionList : MonoBehaviour
{
    public string mission; //Set in inspector

    public List<string> enemies = new List<string>();

    private void Awake()
    {
        if(mission == "sample")
        {
            enemies.Add("Sunbather"); //A1
            enemies.Add("Gaia"); //B1
            enemies.Add("Cthulhu"); //A2
            enemies.Add(""); //B2
            enemies.Add(""); //A3
            enemies.Add("Trogdor"); //B3
        }
    }

}
