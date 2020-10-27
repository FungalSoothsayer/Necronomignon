using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Move 
{
    public int move_id;
    public string name;
    public int power;
    public float condition_chance;
    public string description;

    override
    public String ToString()
    {
        String str = "";
        str += "Name = " + name + "\n";
        str += "ID = " + move_id + "\n";
        str += "Condition Chance = " + condition_chance + "\n";
        str += "Power = " + power + "\n";
        str += "Description = " + description + "\n";
        return str;
    }
}
