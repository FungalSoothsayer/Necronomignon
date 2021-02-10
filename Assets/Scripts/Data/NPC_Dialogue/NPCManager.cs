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

        // Gets the Moves and sets them in the right object. 
        /*foreach (Beast beast in beastsList.Beasts)
        {
            beast.Move_A = getMove(beast.moveA);
            beast.Move_B = getMove(beast.moveB);
        }*/
    }

    public bool isLoaded()
    {
        if (npcList.NPCs.Count == 0)
        {
            return false;
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
