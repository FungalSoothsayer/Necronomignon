using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    public CreatePoolLoader createPoolLoader;

    public GameObject slot1Obj;
    public GameObject slot2Obj;
    public GameObject slot3Obj;
    public GameObject slot4Obj;
    public GameObject slot5Obj;
    public GameObject slot6Obj;
    public GameObject cancelButton;
    public GameObject removeButton;

    public string slot1;
    public string slot2;
    public string slot3;
    public string slot4;
    public string slot5;
    public string slot6;

    public string selected;
    public int selectedIndex;

    public bool canBePlaced = true;
    public bool placing = false;
    public bool moving = false;
    public int placed = 0;

    public int selectedSlotID;

    public bool saveMode = false;

    void Start()
    {
        //Set objects inactive to start
        slot1Obj.SetActive(false);
        slot2Obj.SetActive(false);
        slot3Obj.SetActive(false);
        slot4Obj.SetActive(false);
        slot5Obj.SetActive(false);
        slot6Obj.SetActive(false);
        cancelButton.SetActive(false);
        removeButton.SetActive(false);
    }

    //Set all slot lights and cancel button active 
    public void LightUpSlots()
    {
        slot1Obj.SetActive(true);
        slot2Obj.SetActive(true);
        slot3Obj.SetActive(true);
        slot4Obj.SetActive(true);
        slot5Obj.SetActive(true);
        slot6Obj.SetActive(true);
        cancelButton.SetActive(true);
    }

    //Set all slots that do not have a beast placed in it to inactive
    public void TurnOffSlots()
    {
        if(slot1 == "")
        {
            slot1Obj.SetActive(false);
        }
        if(slot2 == "")
        {
            slot2Obj.SetActive(false);
        }
        if (slot3 == "")
        {
            slot3Obj.SetActive(false);
        }
        if (slot4 == "")
        {
            slot4Obj.SetActive(false);
        }
        if (slot5 == "")
        {
            slot5Obj.SetActive(false);
        }
        if (slot6 == "")
        {
            slot6Obj.SetActive(false);
        }
        cancelButton.SetActive(false);
    }

    //Connected to cancel button
    public void Cancel()
    {
        TurnOffSlots();
        selected = "";
        selectedIndex = -1;
        if (removeButton.activeInHierarchy) removeButton.SetActive(false);
        if (moving) moving = false;
        if (placing) placing = false;
    }

    //Connected to remove button
    public void Remove()
    {
        createPoolLoader.PutImageBack();
        RemoveSlotImage();
    }

    //Remove the image in a slot and remove it from selected variables
    public void RemoveSlotImage()
    {
        switch (selectedSlotID)
        {
            case 1:
                GameObject.Find("Slot1").GetComponent<SlotSelect>().RemoveImage();
                slot1 = "";
                break;
            case 2:
                GameObject.Find("Slot2").GetComponent<SlotSelect>().RemoveImage();
                slot2 = "";
                break;
            case 3:
                GameObject.Find("Slot3").GetComponent<SlotSelect>().RemoveImage();
                slot3 = "";
                break;
            case 4:
                GameObject.Find("Slot4").GetComponent<SlotSelect>().RemoveImage();
                slot4 = "";
                break;
            case 5:
                GameObject.Find("Slot5").GetComponent<SlotSelect>().RemoveImage();
                slot5 = "";
                break;
            case 6:
                GameObject.Find("Slot6").GetComponent<SlotSelect>().RemoveImage();
                slot6 = "";
                break;
        }
        selected = "";
        selectedIndex = -1;
        placed -= 1;
        CheckPlaceable();
        moving = false;
        TurnOffSlots();
        removeButton.SetActive(false);
    }

    //Check to see if any more beasts can be placed
    public void CheckPlaceable()
    {
        if(placed >= 4)
        {
            canBePlaced = false;
        }
        else
        {
            canBePlaced = true;
        }
    }
}
