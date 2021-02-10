using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/*
 * Main NPC class for representing NPC objects 
 * Note that this class also represents some playable characters but as NPC were more abundant 
 * the attribute isNPC is used to differentiate between NPC and playable characters.
 */
[System.Serializable]
public class NPC 
{
    //Base attributes for NPC
    private string name = "";
    private string story;
    private factions faction;
    private bool isNPC;
    private string static_img;
    private int npc_id;
    //[XmlElement(Namespace = "NPC")]

    // public enum categories { }; //needs to be populated with the different categories in the game
    public enum factions { }; //needs to be populated with the different worlds in the game

    /*
     * Dialogue attributes such as Response will be determines by the isNpc status
     * since some responses are exclusive for playable characters as they require
     * input from player
     */
    private NPCDialogue npcDialogue;
    private int npc_diag;
    private string mainDialogue;
    private List<string> responses;

    public NPCManager npcMgr;

    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }
    public factions Faction { get => faction; set => faction = value; }
    public bool IsNPC { get => isNPC; set => isNPC = value; }
    public int Npc_diag { get => npc_diag; set => npc_diag = value; }
    public string MainDialogue { get => mainDialogue; set => mainDialogue = value; }
    public NPCDialogue NpcDialogue { get => npcDialogue; set => npcDialogue = value; }
    public List<string> Responses { get => responses; set => responses = value; }
    public string Static_img { get => static_img; set => static_img = value; }
    public int Npc_id { get => npc_id; set => npc_id = value; }

    public NPC() { }

    public NPC(NPC npc)
    {
        Name = npc.Name;
        Story = npc.Story;
        Faction = npc.Faction;
        IsNPC = npc.IsNPC;
        Npc_diag = npc.Npc_diag;
        MainDialogue = npc.MainDialogue;
        Responses = npc.Responses;
        Static_img = npc.Static_img;
        Npc_id = npc.Npc_id;

    }

    
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (obj.GetType() == typeof(NPC))
        {
            NPC npc = (NPC)obj;

            if (npc.name.Equals(this.name) && npc.Npc_id == this.Npc_id)
            {
                return true;
            }
        }
        else
        {
            Debug.Log("The boolean expresion is wrong");
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


}
