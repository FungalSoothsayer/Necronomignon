using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main Dialogue Class to Represent Dialogues and Responses from NPC objects
 */
public class NPCDialogue
{
    //Main dialog acts as the sum of all dialog components in dialog
    private int diag_id;
    private string speaker;
    private string mainDialogue;
    private DiagType diag_type;
    private int diag_level;

    private List<string> responses;

    public int Diag_id { get => diag_id; set => diag_id = value; }
    public string Speaker { get => speaker; set => speaker = value; }
    public string MainDialogue { get => mainDialogue; set => mainDialogue = value; }
    public DiagType Diag_type { get => diag_type; set => diag_type = value; }
    public int Diag_level { get => diag_level; set => diag_level = value; }
    public List<string> Responses { get => responses; set => responses = value; }

    public enum DiagType { }; //needs to be populated with the different dialog types ie story, battle, introduction, narrative+

    public NPCDialogue() { }

    public NPCDialogue(NPCDialogue diag)
    {
        Diag_id = diag.Diag_id;
        Speaker = diag.Speaker;
        MainDialogue = diag.MainDialogue;
        Diag_type = diag.Diag_type;
        Diag_level = diag.diag_level;
        Responses = diag.Responses;
    }
}
