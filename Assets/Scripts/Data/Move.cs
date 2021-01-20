using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
/*
* This is the object class for Moves 
*/
[System.Serializable]
public class Move 
{
    //Values of a move object, these gets filled from the Move.Json
    public int move_id;
    public string name;
    public int power;
    public int number_of_moves;
    public float condition_chance;
    public string description;
    public bool healing;
    public bool rowAttack;
    public bool columnAttack;
    public bool multiAttack;

    //Buff Object from the buff class
    public Buff buff;
    // Type object (Elements: Wind,Wate, Earth, Dark....) 
    [XmlElement(Namespace = "Move")]
    public types type;

    //To string
    public enum types {[XmlEnum(Name = "Sleep")] Sleep, [XmlEnum(Name = "Burn")] Burn, [XmlEnum(Name = "Poison")] Poison, [XmlEnum(Name = "Paralyze")] Paralyze, [XmlEnum(Name = "Doom")] Doom, [XmlEnum(Name = "Blind")] Blind, [XmlEnum(Name = "Corrupt")] Corrupt, [XmlEnum(Name = "Confusion")] Confusion };
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
