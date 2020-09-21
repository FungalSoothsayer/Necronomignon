using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSquad : MonoBehaviour
{
    public CreateManager createManager;
    public SquadData squadData;

    public GameObject cancelButton;
    public GameObject removeButton;
    public GameObject saveButton;
    public GameObject squad1Button;
    public GameObject squad2Button;

    private void Start()
    {
        squad1Button.SetActive(false);
        squad2Button.SetActive(false);
    }

    public void SaveButton()
    {
        if (!createManager.canBePlaced)
        {
            squad1Button.SetActive(true);
            squad2Button.SetActive(true);
            saveButton.SetActive(false);
            if (cancelButton.activeInHierarchy) cancelButton.SetActive(false);
            if (removeButton.activeInHierarchy) removeButton.SetActive(false);
            createManager.saveMode = true;
        }
        else
        {
            Debug.Log("4 beasts must be placed before saving the squad.");
        }
    }

    public void SaveThisSquad(int squadNumber)
    {
        if (squadNumber == 1)
        {
            squadData.ClearList(1);
            squadData.AddToList(1, createManager.slot1);
            squadData.AddToList(1, createManager.slot2);
            squadData.AddToList(1, createManager.slot3);
            squadData.AddToList(1, createManager.slot4);
            squadData.AddToList(1, createManager.slot5);
            squadData.AddToList(1, createManager.slot6);
            squadData.ChangeSquadSavedStatus(1);
        }
        else if (squadNumber == 2)
        {
            squadData.ClearList(2);
            squadData.AddToList(2, createManager.slot1);
            squadData.AddToList(2, createManager.slot2);
            squadData.AddToList(2, createManager.slot3);
            squadData.AddToList(2, createManager.slot4);
            squadData.AddToList(2, createManager.slot5);
            squadData.AddToList(2, createManager.slot6);
            squadData.ChangeSquadSavedStatus(2);
        }
        else
        {
            Debug.Log("Error saving squad.");
        }
        SceneManager.LoadScene("OrganizeMain");
    }
}
