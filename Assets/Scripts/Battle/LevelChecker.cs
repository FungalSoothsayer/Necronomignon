using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//checks which levels are availible and sends the string to mission list
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
    //sets the lastclick string to whichever value was saved in the button clicked
    public void setLastClick(string levelName)
    {
        lastClick = levelName;
        print(lastClick);
    }
    //unlocks the following level
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
