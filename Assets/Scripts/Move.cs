using System;
using System.Collections;
using System.Collections.Generic;

public class Move 
{
    public int id;
    public string name;
    public int power;
    public float condition_chance;

    override
    public String ToString()
    {
        String str = "";
        str += "Name = " + name + "\n";
        str += "ID = " + id + "\n";
        str += "Condition Chance = " + condition_chance + "\n";
        str += "Power = " + power + "\n";
        return str;
    }
}
