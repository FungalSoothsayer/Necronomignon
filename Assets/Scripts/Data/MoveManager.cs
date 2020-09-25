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
        path = Application.dataPath + "/Scripts/Data/Move.json";
        jsonString = File.ReadAllText(path);

        movesList = JsonUtility.FromJson<MoveList>(jsonString);
        // print(JsonConvert.DeserializeObject(jsonString)); 

        if (jsonString != null)
        {
            foreach (Move move in movesList.Moves)
            {
                print(move);
            }
        }
        else
        {
            print("Asset is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
