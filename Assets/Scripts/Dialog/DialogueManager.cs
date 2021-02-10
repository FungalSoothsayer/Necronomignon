using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    //main conversation scene variables
    public NPCConversation conversation;
    public string sceneName;

    //Character dialogue system -- Using generic List since no character or dialogue class has been specifically created
    //public List<> characterList;
    //public List<> storyDialogues;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        print(sceneName);

        if (sceneName == "DialogScene")
            ConversationManager.Instance.StartConversation(conversation);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Populates the list of characters for which dialogs will be displayed
    public void GetCharacters()
    {

    }
    //Sets the dialog progression in scene 
    public void SetDialogue(string character1, string character2)
    {

    }
    //Gets the dialog for said speaker
    public void SpeakerDialogue(string speaker)
    {

    }
    //Sets assets of current dialogue scene based on story 
    public void SceneInterface()
    {

    }
    //On dialog end move to following scene or go back to prev scene --Requires implementation depending on the story
    public void ConversationEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
