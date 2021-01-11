using Runemark.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//checks which levels are availible for the player
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
        GameObject go;
       if(LevelChecker.levels < 1)
        {
            go = GameObject.Find("btnLevel2");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
       if(LevelChecker.levels < 2)
        {
            go = GameObject.Find("btnLevel3");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        if (LevelChecker.levels < 3)
        {
            go = GameObject.Find("btnLevel4");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        if (LevelChecker.levels < 4)
        {
            go = GameObject.Find("btnLevel5");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        if (LevelChecker.levels < 5)
        {
            go = GameObject.Find("btnLevel6");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        if (LevelChecker.levels < 6)
        {
            go = GameObject.Find("btnLevel7");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        if (LevelChecker.levels < 7)
        {
            go = GameObject.Find("btnLevel8");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }
    //sends a string that represents which beasts should be sent as the enemy squad in mission list
    public void sendString(string str)
    {
        GameObject name = GameObject.Find("LevelData");

        int lvl = int.Parse(this.name.ToCharArray()[this.name.Length-1].ToString());
        DialogueSystem.SetGlobalVariable<int>("Level", lvl);

        if (name != null)
        {
            levelChecker = name.GetComponent<LevelChecker>();
            levelChecker.setLastClick(str);
        }
    }
}
