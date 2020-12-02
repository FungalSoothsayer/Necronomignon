using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 */
public class SquadData : MonoBehaviour
{
    //Holds the names of the beasts in each position of the grid of saved squad 1
    public static List<Beast> squad1 = new List<Beast>();
    public static bool squad1Saved = false;

    //Holds the names of the beasts in each position of the grid of saved squad 2
    public static List<Beast> squad2 = new List<Beast>();
    public static bool squad2Saved = false;

    //Saves the selected beasts into the correct squad
    public void AddToList(int squad, Beast beast)
    {
        //Makes sure all unused spaces are set to null for future use
        if(beast != null && beast.speed == 0)
        {
            beast = null;
        }
        
        if(squad == 1)
        {
            squad1.Add(beast);
        }
        else if(squad == 2)
        {
            squad2.Add(beast);
        }
        else
        {
            Debug.Log("Error saving squad.");
        }
    }

    //Clears the squad that was previously saved
    public void ClearList(int squad)
    {
        if (squad == 1)
        {
            squad1.Clear();
        }
        else if (squad == 2)
        {
            squad2.Clear();
        }
        else
        {
            Debug.Log("Error clearing squad.");
        }
    }

    //Returns the selected squad
    public List<Beast> GetSquadList(int squad)
    {
        if (squad == 1)
        {
            return squad1;
        }
        else if (squad == 2)
        {
            return squad2;
        }
        else
        {
            Debug.Log("Error retrieving squad.");
            return squad2;
        }
    }

    //Marks that there is a squad saved in squad 1 or 2 to load in other scenes
    public void ChangeSquadSavedStatus(int squad)
    {
        if (squad == 1)
        {
            if (!squad1Saved)
            {
                squad1Saved = true;
            }
        }
        else if (squad == 2)
        {
            if (!squad2Saved)
            {
                squad2Saved = true;
            }
        }
        else
        {
            Debug.Log("Error saving squad.");
        }
    }

    //Returns true if squad 1 has been saved to
    public bool GetSquad1Status()
    {
        return squad1Saved;
    }

    //Returns true if squad 2 has been saved to
    public bool GetSquad2Status()
    {
        return squad2Saved;
    }
}
