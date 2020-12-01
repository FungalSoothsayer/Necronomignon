using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    static public int levels = 0;

    static public string lastClick;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void setLastClick(string levelName)
    {
        lastClick = levelName;
        print(lastClick);
    }

    public void Progess(string levelName)
    {
        if (lastClick == "sample" && levels <= 0)
        {
            levels++;
        }
        else if (lastClick == "random" && levels <= 1)
        {
            levels++;
        }
        else if (lastClick == "randomer" && levels <= 2)
        {
            levels++;
        }
    }
}
