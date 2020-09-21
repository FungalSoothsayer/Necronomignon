using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    //Load scene passed to method
    public void LoadSelect(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
