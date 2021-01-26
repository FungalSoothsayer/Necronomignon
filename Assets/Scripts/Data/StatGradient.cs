using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatGradient
{


    public int hpGradient;
    public int defenceGradient;
    public int powerGradient;
    public int speedGradient;
    public int dexGradient;

    public StatGradient()
    {

    }

    override
    public string ToString()
    {
        return "hp " + hpGradient + "\ndefence " + defenceGradient + "\npower " + powerGradient + "\nspeed " + speedGradient + "dex " + dexGradient;
    }
}
