using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questions
{
    public string question;
    public List<string> options;
    public bool selected;

    override
    public String ToString()
    {        
        return question;
    }
}
