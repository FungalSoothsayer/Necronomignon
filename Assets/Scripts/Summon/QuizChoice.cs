using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizChoice : MonoBehaviour
{

    Beast currentBeast;

    public void GetBeast(Beast beast)
    {
        currentBeast = beast;
    }

    public void ChoiceClick(int addRate)
    {
        currentBeast.tier += 1;
    }
}
