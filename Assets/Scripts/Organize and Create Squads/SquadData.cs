using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadData : MonoBehaviour
{
    //Holds the names of the beasts in each position of the grid of saved squad 1
    public static List<string> squad1 = new List<string>();
    public static bool squad1Saved = false;

    //Holds the names of the beasts in each position of the grid of saved squad 2
    public static List<string> squad2 = new List<string>();
    public static bool squad2Saved = false;

    public void AddToList(int squad, string beast)
    {
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
            Debug.Log("Error saving squad.");
        }
    }

    public List<string> GetSquadList(int squad)
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
            Debug.Log("Error saving squad.");
            return squad2;
        }
    }

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

    public bool GetSquad1Status()
    {
        return squad1Saved;
    }

    public bool GetSquad2Status()
    {
        return squad2Saved;
    }
}
