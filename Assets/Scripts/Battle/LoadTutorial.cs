using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTutorial : MonoBehaviour
{
    public static bool tutorialCompleted = false;

    //Check if the tutorial has been completed and load the corresponding scene
    public void LoadNextScene()
    {
        if (tutorialCompleted)
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            tutorialCompleted = true;
            SceneManager.LoadScene("TutorialMain");
        }
    }
}
