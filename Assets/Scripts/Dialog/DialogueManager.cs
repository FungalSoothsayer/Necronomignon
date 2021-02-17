using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

namespace DialogueEditor
{
    public class DialogueManager : MonoBehaviour
    {

        //main conversation scene variables
        public NPCConversation conversation;
        public string sceneName;

        //Character dialogue system 
        public List<NPC> characterList;
        public List<NPCDialogue> storyDialogues;

        // Start is called before the first frame update
        void Start()
        {
            
            //Start conversation
            BeginConversation();
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
        //Temporary Method for testing
        public SpeechNode SetRootDialogue()
        {
            EditableSpeechNode editableNode = new EditableSpeechNode();

            editableNode.Name = "Sister";
            editableNode.Text = "...Don't worry Dio, you've got this!";
            editableNode.AdvanceDialogueAutomatically = true;
            editableNode.AutoAdvanceShouldDisplayOption = false;
            editableNode.TimeUntilAdvance = 0.5f;

            SpeechNode node = conversation.CreateSpeechNode(editableNode);

            return node;
           

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
            
        }
        //Conversation begins on scene
        public void BeginConversation()
        {
            sceneName = SceneManager.GetActiveScene().name;

            Debug.Log(sceneName);

            if (sceneName == "DialogScene")
                ConversationManager.Instance.StartConversation(conversation);
        }
    }
}
