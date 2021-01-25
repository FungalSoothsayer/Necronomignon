using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStart : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {

        ConversationManager.Instance.StartConversation(conversation);
        if (ConversationManager.Instance != null)
        {
            print(CheckLevel.lvl);
            ConversationManager.Instance.SetInt("Levels", CheckLevel.lvl);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
