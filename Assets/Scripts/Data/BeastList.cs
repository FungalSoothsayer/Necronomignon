using System.Collections;
using System.Collections.Generic;

/*
 *  This helps the creation of a BeastManager object, allows us to parse Beast.json into a list of Beast Objects.
 */
[System.Serializable]
public class BeastList 
{
   public List<Beast> Beasts = new List<Beast>();

    public BeastList()
    {

    }
}