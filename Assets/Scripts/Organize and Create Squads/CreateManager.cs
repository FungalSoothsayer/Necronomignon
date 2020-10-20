using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    public CreatePoolLoader createPoolLoader;

   /* public GameObject slot1Obj;
    public GameObject slot2Obj;
    public GameObject slot3Obj;
    public GameObject slot4Obj;
    public GameObject slot5Obj;
    public GameObject slot6Obj;*/
    public List<GameObject> slotObjs;
    public GameObject cancelButton;
    public GameObject removeButton;

   /* public Beast slot1;
    public Beast slot2;
    public Beast slot3;
    public Beast slot4;
    public Beast slot5;
    public Beast slot6;*/
    public List<Beast> slots;


    public Beast selected;
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

        foreach(GameObject go in slotObjs)
        {
            go.SetActive(false);
        }

        /*slot1Obj.SetActive(false);
        slot2Obj.SetActive(false);
        slot3Obj.SetActive(false);
        slot4Obj.SetActive(false);
        slot5Obj.SetActive(false);
        slot6Obj.SetActive(false);*/
        cancelButton.SetActive(false);
        removeButton.SetActive(false);
    }

    //Set all slot lights and cancel button active 
    public void LightUpSlots()
    {

        foreach (GameObject go in slotObjs)
        {
            go.SetActive(true);
        }
        /*slot1Obj.SetActive(true);
        slot2Obj.SetActive(true);
        slot3Obj.SetActive(true);
        slot4Obj.SetActive(true);
        slot5Obj.SetActive(true);
        slot6Obj.SetActive(true);*/
        cancelButton.SetActive(true);
    }

    //Set all slots that do not have a beast placed in it to inactive
    public void TurnOffSlots()
    {
        for(int x = 0; x < slots.Count; x++)
        {
            if (slots[x] == null || slots[x].speed == 0)
            {
                slotObjs[x].SetActive(false);
            }
        }
        /*if (slot1 == null || slot1.speed == 0)
        {
            slot1Obj.SetActive(false);
        }
        if (slot2 == null || slot2.speed == 0)
        {
            slot2Obj.SetActive(false);
        }
        if (slot3 == null || slot3.speed == 0)
        {
            slot3Obj.SetActive(false);
        }
        if (slot4 == null || slot4.speed == 0)
        {
            slot4Obj.SetActive(false);
        }
        if (slot5 == null || slot5.speed == 0)
        {
            slot5Obj.SetActive(false);
        }
        if (slot6 == null || slot6.speed == 0)
        {
            slot6Obj.SetActive(false);
        }*/
        cancelButton.SetActive(false);
    }

    //Connected to cancel button
    public void Cancel()
    {
        TurnOffSlots();
        selected = null;
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

        GameObject.Find("Slot"+selectedSlotID).GetComponent<SlotSelect>().RemoveImage();
        slots[selectedSlotID-1] = null;
        /*switch (selectedSlotID)
        {
            case 1:
                GameObject.Find("Slot1").GetComponent<SlotSelect>().RemoveImage();
                slot1 = null;
                break;
            case 2:
                GameObject.Find("Slot2").GetComponent<SlotSelect>().RemoveImage();
                slot2 = null;
                break;
            case 3:
                GameObject.Find("Slot3").GetComponent<SlotSelect>().RemoveImage();
                slot3 = null;
                break;
            case 4:
                GameObject.Find("Slot4").GetComponent<SlotSelect>().RemoveImage();
                slot4 = null;
                break;
            case 5:
                GameObject.Find("Slot5").GetComponent<SlotSelect>().RemoveImage();
                slot5 = null;
                break;
            case 6:
                GameObject.Find("Slot6").GetComponent<SlotSelect>().RemoveImage();
                slot6 = null;
                break;
        }*/
        selected = null;
        selectedIndex = -1;
        placed -= 1;
        CheckPlaceable();
        moving = false;
        TurnOffSlots();
        removeButton.SetActive(false);
    }

    public bool isAvailable(int x)
    {
        if (slots[x - 1] != null && slots[x-1].speed ==0)
        {
            return true;
        }
        /*switch (x)
        {
            case 1: 
                if(slot1 != null && slot1.speed == 0)
                {
                    return true;
                }
                break;
            case 2:
                if (slot2 != null && slot2.speed == 0)
                {
                    return true;
                }
                break;
            case 3:
                if (slot3 != null && slot3.speed == 0)
                {
                    return true;
                }
                break;
            case 4:
                if (slot4 != null && slot4.speed == 0)
                {
                    return true;
                }
                break;
            case 5:
                if (slot5 != null && slot5.speed == 0)
                {
                    return true;
                }
                break;
            case 6:
                if (slot6 != null && slot6.speed == 0)
                {
                    return true;
                }
                break;
        }*/
        return false;
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
