using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizChoice : MonoBehaviour
{
    public BeastDatabase beastDatabase;

    string currentBeast;

    public void GetBeast(string beast)
    {
        currentBeast = beast;
    }

    public void ChoiceClick(int addRate)
    {
        beastDatabase.ChangeRating(currentBeast, beastDatabase.GetRating(currentBeast) + addRate);
    }
}
