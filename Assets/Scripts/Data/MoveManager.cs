using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Versioning;

[System.Serializable]
public class MoveManager : MonoBehaviour
{

    string path;
    string jsonString;

    public MoveList movesList = new MoveList();

    // Start is called before the first frame update
    void Start()
    {
        print("Something that we could easily find");
        path = Application.dataPath + "/Scripts/Data/Move.json";
        jsonString = File.ReadAllText(path);
        print(jsonString);
        movesList = JsonUtility.FromJson<MoveList>(jsonString);
        //movesList = JsonConvert.DeserializeObject(jsonString); 
        print(movesList.Moves.Count);
        foreach (Move moves in movesList.Moves)
        {
            print(moves);
        }
    }

    public void start()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
