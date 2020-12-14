using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Versioning;

/*
 * Handles the Json Parsing for Beasts moves in Move.json
 */
 
[System.Serializable]
public class StoryManager : MonoBehaviour
{
    string path;
    string jsonString;

    public static StoryList storyList = new StoryList();

    // Start is called before the first frame update
    void Start()
    {
        //Parse through the Json file and put the moves in a list. 
        path = Application.dataPath + "/Scripts/Data/Stories.json";
        jsonString = File.ReadAllText(path);
        storyList = JsonUtility.FromJson<StoryList>(jsonString);
        foreach(Story s in storyList.Stories)
        {
            print(s);
            foreach(Questions q in s.questions)
            {
                print(q);
            }
        }
    }

    public void start()
    {
        Start();
    }
}
