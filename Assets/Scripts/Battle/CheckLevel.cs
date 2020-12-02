using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevel : MonoBehaviour
{
    public LevelChecker levelChecker;

    // Start is called before the first frame update
    void Start()
    {
        GameObject name = GameObject.Find("LevelData");

        if(name != null)
        {
            levelChecker = name.GetComponent<LevelChecker>();
        }

       if(LevelChecker.levels < 1)
        {
            GameObject.Find("btnLevel2").SetActive(false);
        }
       if(LevelChecker.levels < 2)
        {
            GameObject.Find("btnLevel3").SetActive(false);
        }
    }
    public void sendString(string str)
    {
        GameObject name = GameObject.Find("LevelData");

        if (name != null)
        {
            levelChecker = name.GetComponent<LevelChecker>();
            levelChecker.setLastClick(str);
        }
    }
}
