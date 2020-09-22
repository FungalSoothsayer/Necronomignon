using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Versioning;

[System.Serializable]
public class BeastManager : MonoBehaviour
{

    string path;
    string jsonString;

    public BeastList beastsList = new BeastList();

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Scripts/Data/Beast.json";
        jsonString = File.ReadAllText(path);


       beastsList = JsonUtility.FromJson<BeastList>(jsonString);
     // print(JsonConvert.DeserializeObject(jsonString)); 



        if (jsonString != null)
        {          
            foreach (Beast beast in beastsList.Beasts)
            {
                print(beast);

            }
        }
        else
        {
            print("Asset is null");
        }
       // get();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Beast getFromName(String str)
    {
        if(beastsList.Beasts.Count <= 0)
        {
            Start();
        }

        Beast b = null;

        for (int x = 0; x < beastsList.Beasts.Count; x++)
        {
            if (str.Equals(beastsList.Beasts[x].name))
            {
                b = beastsList.Beasts[x];
                break;
            }
            
        }
        return b;
    }

    public Beast[] get()
    {
        return null;
/*
        path = Application.dataPath + "/Scripts/Data/Beast.json";
        jsonString = File.ReadAllText(path);
        //Beast[] listaRecords = JsonUtility.FromJson<Beast[]>(jsonString);l
        
        //var listaRecords = JsonConvert.DeserializeObject(jsonString);
       var listaRecords =
        //  print(listaRecords[0].name);
        Beast[] ba = (Beast[])listaRecords.Beast;
        print(jsonString);
        foreach (Beast record in ba)
        {

            print("nombre: " + record.name+" a");
        }
        return ba;
*/
    
    }
}
