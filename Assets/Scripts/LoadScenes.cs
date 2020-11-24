using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    //Load scene passed to method
    public void LoadSelect(string sceneName)
    {
        AudioManager go = FindObjectOfType<AudioManager>();
        
        if (go != null)
        {
            go.Play(sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
