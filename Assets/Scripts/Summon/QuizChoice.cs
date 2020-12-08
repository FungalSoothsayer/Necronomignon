using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This Class Should handle everything Quizz Related, After reading the short stories for individual beasts, a quiz should pop up, and reading comprehension questions 
 * will be asked to the player
 *  WORK IN PROGRESS!!!
 */
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
