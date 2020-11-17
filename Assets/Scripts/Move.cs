using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Move 
{
    public int move_id;
    public string name;
    public int power;
    public int number_of_moves;
    public float condition_chance;
    public string description;
    public bool healing;
    public bool rowAttack;
    public bool columnAttack;
    public types type;

    public enum types { Water, Fire, Earth, Air, Dark, Light };
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
