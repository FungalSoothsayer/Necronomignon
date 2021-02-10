using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Versioning;

[System.Serializable]
public class NPCDialogueManager : MonoBehaviour
{
    string path;
    string jsonString;

    public DialogueList diagList = new DialogueList();
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Scripts/Data/NPC_Dialogue/NPCDialogue.json";
        jsonString = File.ReadAllText(path);
        diagList = JsonUtility.FromJson<DialogueList>(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
