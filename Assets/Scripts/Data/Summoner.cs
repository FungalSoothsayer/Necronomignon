using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner 
{
    public int xp = 0;


    public int getLevel()
    {
        int lvl = this.xp / 100;
        if(lvl <=0)
        {
            lvl = 1;
        }
        else if (lvl > 100)
        {
            lvl = 100;
        }
        return lvl;
    }

    public void addXP(int newxp)
    {
        xp += newxp;
    }
}
