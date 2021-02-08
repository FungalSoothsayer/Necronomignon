using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{

    public NPCConversation conversation;
    public string current;
    // Start is called before the first frame update
    void Start()
    {
        current = SceneManager.GetActiveScene().name;
        conversation = gameObject.GetComponent(typeof(NPCConversation)) as NPCConversation;

        if (current == "DialogScene")
            ConversationManager.Instance.StartConversation(conversation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
