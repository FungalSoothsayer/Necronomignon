using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizChoice : MonoBehaviour
{
    public BeastDatabase beastDatabase;

    Beast currentBeast;

    public void GetBeast(Beast beast)
    {
        currentBeast = beast;
    }

    public void ChoiceClick(int addRate)
    {
        if(currentBeast.tier > 5)
        {
            currentBeast.tier += addRate;
        }
        while(currentBeast.tier > 5)
        {
            currentBeast.tier -= 1;
        }
    }
}
