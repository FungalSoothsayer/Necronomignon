using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatGradient
{


    public GraDis hpGradient;
    public GraDis defenceGradient;
    public GraDis powerGradient;
    public GraDis speedGradient;
    public GraDis dexGradient;

    public StatGradient()
    {

    }

    override
    public string ToString()
    {
        return "hp " + hpGradient + "\ndefence " + defenceGradient + "\npower " + powerGradient + "\nspeed " + speedGradient + "dex " + dexGradient;
    }
}
