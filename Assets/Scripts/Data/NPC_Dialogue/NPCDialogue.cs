using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main Dialogue Class to Represent Dialogues and Responses from NPC objects
 */
[System.Serializable]
public class NPCDialogue
{
    //Main dialog acts as the sum of all dialog components in dialog
    private string dialogueName;
    private string conversationName;
    private string scene;
    private int numberSpeakers;
    private string[] speakersNames;

    public string DialogueName { get => dialogueName; set => dialogueName = value; }
    public string ConversationName { get => conversationName; set => conversationName = value; }
    public int NumberSpeakers { get => numberSpeakers; set => numberSpeakers = value; }
    public string[] SpeakersNames { get => speakersNames; set => speakersNames = value; }
    public string Scene { get => scene; set => scene = value; }

    public enum DiagType { }; //needs to be populated with the different dialog types ie story, battle, introduction, narrative+

    public NPCDialogue() { }

    public NPCDialogue(NPCDialogue diag)
    {
        DialogueName = diag.DialogueName;
        ConversationName = diag.ConversationName;
        NumberSpeakers = diag.NumberSpeakers;
        SpeakersNames = diag.SpeakersNames;
        Scene = diag.Scene;
    }
}
