using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    public CreatePoolLoader createPoolLoader;
    //public BeastManager beastManager;

    public List<GameObject> slotObjs;
    //List<Beast> beasts = new List<Beast>();

    public GameObject cancelButton;
    public GameObject removeButton;
    public GameObject moveDescription;
    public Text description;

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

        cancelButton.SetActive(false);
        removeButton.SetActive(false);
    }

    //Set all slot lights and cancel button active 
    public void LightUpSlots()
    {
        moveDescription.SetActive(true);
        selected = createPoolLoader.summoned[selectedIndex + (createPoolLoader.counter * 9)];
        for (int x = 0+ (createPoolLoader.counter*9); x < createPoolLoader.summoned.Count; x++)
        {
            if (selected.name.Equals(createPoolLoader.summoned[x].name))
            {
                description.GetComponent<Text>().text = "\n\nIn the front row \n" + selected.Move_A.description + 
                    "\n\nIn the back row \n" + selected.Move_B.description;
                break;
            }
        }
        foreach (GameObject go in slotObjs)
        {
            go.SetActive(true);
        }

        cancelButton.SetActive(true);
    }

    //Set all slots that do not have a beast placed in it to inactive
    public void TurnOffSlots()
    {
        moveDescription.SetActive(false);
        for (int x = 0; x < slots.Count; x++)
        {
            if (slots[x] == null || slots[x].speed == 0)
            {
                slotObjs[x].SetActive(false);
            }
        }

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
        RemoveBeast();
        createPoolLoader.PutImageBack();
        RemoveSlotImage();
        
    }
    public void RemoveBeast()
    {
        GameObject.Find("Slot" + selectedSlotID).GetComponent<SlotSelect>().RemoveImage();
        slots[selectedSlotID - 1] = null;
    }
    //Remove the image in a slot and remove it from selected variables
    public void RemoveSlotImage()
    {

        GameObject.Find("Slot" + selectedSlotID).GetComponent<SlotSelect>().RemoveImage();
        slots[selectedSlotID - 1] = null;

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
