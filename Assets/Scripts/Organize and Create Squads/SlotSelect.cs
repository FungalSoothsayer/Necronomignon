using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CreateManager createManager;
    public CreatePoolLoader createPoolLoader;
    public BeastManager beastManager;

    private bool mouse_over = false;

    public int slotID; //Set in inspector

    Beast thisBeast;
    int thisBeastIndex;

    void Update()
    {
        if (mouse_over)
        {
            //When mouse is clicked and cursor is over this image, set the beast to this slot
            if (Input.GetMouseButtonDown(0) && !createManager.saveMode)
            {
                if (createManager.canBePlaced && createManager.placing) SetImage();
                else if (createManager.moving && !createManager.placing) MoveImage();
                else EditPlace();
            }
        }
    }

    //When the cursor is over this image, make mouse_over true
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    //When cursor leaves this image, make mouse_over false
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    //Set the placed beast image to this slot's image
    void SetImage()
    {
        print("setimage");
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(createPoolLoader.summonedImages[createManager.selectedIndex]);
        gameObject.GetComponent<Image>().color = Color.white;
        ChangePoolImage();
    }

    //Remove the beast's image from this slot's image
    public void RemoveImage()
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ellipse 1");
        gameObject.GetComponent<Image>().color = Color.green;
    }

    //Change the image of the beast in the pool to a faded image
    void ChangePoolImage()
    {
        int index = createManager.selectedIndex;

        createPoolLoader.slots[index].gameObject.SetActive(false);
        createPoolLoader.slots[index].sprite = Resources.Load<Sprite>(GetFadedImage());
        SetSlot();
    }

    //Set the CreateManager's variables to reflect the selected beast
    void SetSlot()
    {
        print("set slot");
        print(createManager.selectedIndex);
        print(createManager.selectedSlotID);

        char chr = (gameObject.name).ToCharArray()[gameObject.name.Length - 1];
        int num = int.Parse(chr.ToString());
        print(num);

        createManager.slots[num-1] = createManager.selected;

        thisBeast = createManager.selected;
        thisBeastIndex = createManager.selectedIndex;

        createManager.placed += 1;
        createManager.placing = false;
        createManager.CheckPlaceable();
        createManager.selected = null;
        createManager.selectedIndex = -1;
        createManager.TurnOffSlots();
    }

    //Get the faded image of the placed beast
    string GetFadedImage()
    {
        if (!beastManager.isLoaded())
        {
            beastManager.Awake();
        }
        BeastList bl = BeastManager.beastsList; 
        for (int x = 0; x< bl.Beasts.Count; x++)
        {
            if (bl.Beasts[x].Equals(createManager.selected))
            {
                return "EmptyRectangle";
            }
        }
        return "";
    }

    //Select this beast and give options to move to another slot or remove from the grid
    void EditPlace()
    {
        print("editplace");
        createManager.selected = thisBeast;
        createManager.selectedIndex = thisBeastIndex;
        createManager.selectedSlotID = slotID;
        createManager.LightUpSlots();
        createManager.removeButton.SetActive(true);
        createManager.moving = true;
    }

    //Move this beast to another slot
    void MoveImage()
    {
        print("moveimage");
        if (slotID != createManager.selectedSlotID)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(createPoolLoader.summonedImages[createManager.selectedIndex]);
            gameObject.GetComponent<Image>().color = Color.white;
            SetSlot();
            createManager.RemoveSlotImage();
        }
    }
}
