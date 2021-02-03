using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Summoner summoner = new Summoner();
    public static bool RedRoach = true;
    // Start is called before the first frame update
    void Start()
    {
        print(summoner.getLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addXp(int xp)
    {
        if (xp < 1)
        {
            summoner.addXP(1);
            return;
        }
        summoner.addXP(xp);
        print(summoner.getLevel());
    }

    public static void activeRedRoach()
    {
        RedRoach = !RedRoach;
    }
}
