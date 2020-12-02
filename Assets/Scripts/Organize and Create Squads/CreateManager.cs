using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles all of the processes when creating a squad
 */
public class CreateManager : MonoBehaviour
{
    public CreatePoolLoader createPoolLoader;

    public List<GameObject> slotObjs; // Squad gameobjects
    public List<Beast> slots; // Beasts that are in the squad slots

    public GameObject cancelButton;
    public GameObject removeButton;
    public GameObject moveDescription;
    public Text description;
    public Beast selected;
    public int selectedIndex;
    public bool canBePlaced = true;
    public bool placing = false;
    public bool moving = false;
    public int placed = 0;
    public int selectedSlotID;
    public bool saveMode = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in slotObjs)
        {
            go.SetActive(false);
        }

        cancelButton.SetActive(false);
        removeButton.SetActive(false);
    }

    // Sets all slot lights and cancel button active for when a beast is being selected
    public void LightUpSlots()
    {
        moveDescription.SetActive(true);
        selected = createPoolLoader.summoned[selectedIndex + (createPoolLoader.counter * 9)];

        // Sets the description of selected beast
        for (int x = 0 + (createPoolLoader.counter * 9); x < createPoolLoader.summoned.Count; x++)
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

    // Sets all slots that do not have a beast placed in them to inactive
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

    // Cancels all selections when cancel button is pressed
    public void Cancel()
    {
        TurnOffSlots();
        selected = null;
        selectedIndex = -1;
        if (removeButton.activeInHierarchy) removeButton.SetActive(false);
        if (moving) moving = false;
        if (placing) placing = false;
    }

    // Removes selected beast when remove button is pressed
    public void Remove()
    {
        RemoveImage();
        createPoolLoader.PutImageBack();
        RemoveSlotImage();
    }

    // Removes image from a slot
    public void RemoveImage()
    {
        GameObject.Find("Slot" + selectedSlotID).GetComponent<SlotSelect>().RemoveImage();
        slots[selectedSlotID - 1] = null;
    }

    // Removes the image in a slot and remove it from selected variables
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

    // Checks to see if any more beasts can be placed
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
