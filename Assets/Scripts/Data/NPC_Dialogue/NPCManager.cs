using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Versioning;

/*
 *  This class handles the creation of a NPC Object from the Json File    
 *  it also links NPCDialogue objects to the NPC object
 */

[System.Serializable]
public class NPCManager : MonoBehaviour
{

    static int givenId = 100;
    string path;
    string jsonString;
    public NPCDialogueManager npcDialogueManager;

    public static NPCList npcList = new NPCList();

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Scripts/Data/NPC_Dialogue/NPC.json";
        jsonString = File.ReadAllText(path);



        if (jsonString != null && npcList.NPCs.Count <= 0)
        {
            npcList = JsonUtility.FromJson<NPCList>(jsonString);
            foreach (NPC npc in npcList.NPCs)
            {
                // gives each npc a unique id
                npc.Npc_id = givenId;
                givenId++;
            }
        }

        // Gets the dialogs for each character
        foreach (NPC npc in npcList.NPCs)
        {
            npc.NpcDialogue = GetNPCDialogue(npc.Npc_diag);
        }
    }

    public bool isLoaded()
    {
        if (npcList.NPCs.Count == 0)
        {
            return false;
        }

        return true;
    }

    //Gets the dialogues from dialogue list
    public NPCDialogue GetNPCDialogue(int id)
    {
        NPCDialogue diag = new NPCDialogue();

        List<NPCDialogue> diagList = npcDialogueManager.diagList.NpcDialogues;

        foreach(NPCDialogue npcdiag in diagList)
        {
            if(npcdiag.Diag_id == id)
                    return npcdiag;
            
        }


        return diag;
    }

    //Methods to get NPC
    public NPC GetNPC(string npcname)
    {
        if (!isLoaded())
            Start();

        NPC npc = new NPC();

        foreach(NPC chr in npcList.NPCs)
        {
            if (npcname.Equals(chr.Name))
            {
                npc = new NPC(chr);
                break;
            }
        }

        return npc;
    }

    public NPC GetNPC(NPC getNPC)
    {
        if (!isLoaded())
            Start();

        NPC npc = new NPC();

        foreach (NPC chr in npcList.NPCs)
        {
            if (getNPC.Equals(chr))
            {
                npc = new NPC(chr);
                break;
            }
        }

        return npc;
    }
}
