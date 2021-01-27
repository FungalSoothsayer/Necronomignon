using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner 
{
    public int xp = 0;
    public int level = 1;


    public int getLevel()
    {
        updateLevel();
        return level;
    }

    public void addXP(int newxp)
    {
        xp += newxp;
        updateLevel();
    }

    public void updateLevel()
    {
        int xpneeded = (int)Math.Round((4 * (Math.Pow((double)level , (double)3))) / 5);
        while(this.xp > xpneeded)
        {
            level++;
            xpneeded = (int)Math.Round((4 * (Math.Pow((double)level, (double)3))) / 5);
        }
    }
}
