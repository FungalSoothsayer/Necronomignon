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
        ParseJsonData();
    }

    public void ParseJsonData()
    {
        path = Application.dataPath + "/Scripts/Data/NPC_Dialogue/NPCDialogue.json";
        jsonString = File.ReadAllText(path);
        diagList = JsonUtility.FromJson<DialogueList>(jsonString);
    }


    public bool isLoaded()
    {
        if (diagList.NpcDialogues.Count == 0)
        {
            return false;
        }

        return true;
    }

    public NPCDialogue GetNPCDialogue(NPCDialogue getDialogue)
    {
        if (!isLoaded())
            Start();

        NPCDialogue npcdiag = new NPCDialogue();

        foreach (NPCDialogue dia in diagList.NpcDialogues)
        {
            if (getDialogue.Equals(dia))
            {
                npcdiag = new NPCDialogue(dia);
                break;
            }
        }

        return npcdiag;
    }

    public NPCDialogue GetNPCDialogue(string diagName)
    {
        if (!isLoaded())
            Start();

        NPCDialogue npcdiag = new NPCDialogue();

        foreach (NPCDialogue dia in diagList.NpcDialogues)
        {
            if (diagName.Equals(dia.DialogueName))
            {
                npcdiag = new NPCDialogue(dia);
                break;
            }
        }

        return npcdiag;
    }
}
