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
    }

    public void start()
    {
        Start();
    }
}
