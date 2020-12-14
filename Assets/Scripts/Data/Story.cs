using System;
using System.Collections;
using System.Collections.Generic;
/*
 * This is the object class for Moves 
 */
[System.Serializable]
public class Story
{
    //Values of a move object, these gets filled from the Move.Json
    public string beast_name;
    public string title;
    public List<string> pages;
    public List<Questions> questions;
    override
    public String ToString()
    {
        String str = beast_name+" "+title;
        return str;
    }


}

