using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDemoBattle : MonoBehaviour
{
    public SquadData squadData;
    public static int squadNo;

    public void SetSquadNumber(int number)
    {
        squadNo = number;
        if(squadNo == 1)
        {
            if (squadData.GetSquad1Status())
            {
                SceneManager.LoadScene("DemoBattle");
            }
        }
        else
        {
            if (squadData.GetSquad2Status())
            {
                SceneManager.LoadScene("DemoBattle");
            }
        }
    }

    public int GetSquadNumber()
    {
        return squadNo;
    }
}
