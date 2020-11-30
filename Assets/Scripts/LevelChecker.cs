using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    static public int levels = 0;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Progess(string levelName)
    {
        if (levelName == "Manoli" && levels <= 0)
        {
            levels++;
        }
        else if (levelName == "RandomFight" && levels <= 1)
        {
            levels++;
        }
        else if (levelName == "RandomerFight" && levels <= 2)
        {
            levels++;
        }
    }
}
