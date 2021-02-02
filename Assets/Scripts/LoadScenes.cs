using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    //Load scene passed to method
    public void LoadSelect(string sceneName)
    {
        // Looks for an Audio Manager Component
        AudioManager go = FindObjectOfType<AudioManager>();

        //fixes timescale
        if (Time.timeScale == 0)
            Time.timeScale = 1;

        // Checks for null, otherwise the game would crash on any scene change
        if (go != null)
        {
            //Calls the play method from AudioManager script
            go.Play(sceneName);

            //Calls the method from the UnityEngine package, switches Scene from name
            SceneManager.LoadScene(sceneName);
        }
    }
}
