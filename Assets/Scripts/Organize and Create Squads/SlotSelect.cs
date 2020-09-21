using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CreateManager createManager;
    public CreatePoolLoader createPoolLoader;

    private bool mouse_over = false;

    public int slotID; //Set in inspector

    string thisBeast;
    int thisBeastIndex;

    void Update()
    {
        if (mouse_over)
        {
            //When mouse is clicked and cursor is over this image, set the beast to this slot
            if (Input.GetMouseButtonDown(0) && !createManager.saveMode)
            {
                if (createManager.canBePlaced && createManager.placing) SetImage();
                else if(createManager.moving && !createManager.placing) MoveImage();
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
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(createPoolLoader.summonedImages[createManager.selectedIndex]);
        gameObject.GetComponent<Image>().color = Color.white;
        //gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 125);
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
        switch (createManager.selectedIndex)
        {
            case 0:
                createPoolLoader.slot1.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 1:
                createPoolLoader.slot2.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 2:
                createPoolLoader.slot3.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 3:
                createPoolLoader.slot4.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 4:
                createPoolLoader.slot5.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 5:
                createPoolLoader.slot6.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 6:
                createPoolLoader.slot7.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 7:
                createPoolLoader.slot8.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
            case 8:
                createPoolLoader.slot9.sprite = Resources.Load<Sprite>(GetFadedImage());
                break;
        }
        SetSlot();
    }

    //Set te CreateManager's variables to reflect the selected beast
    void SetSlot()
    {
        if (gameObject.name == "Slot1") createManager.slot1 = createManager.selected;
        else if (gameObject.name == "Slot2") createManager.slot2 = createManager.selected;
        else if (gameObject.name == "Slot3") createManager.slot3 = createManager.selected;
        else if (gameObject.name == "Slot4") createManager.slot4 = createManager.selected;
        else if (gameObject.name == "Slot5") createManager.slot5 = createManager.selected;
        else if (gameObject.name == "Slot6") createManager.slot6 = createManager.selected;

        thisBeast = createManager.selected;
        thisBeastIndex = createManager.selectedIndex;

        createManager.placed += 1;
        createManager.placing = false;
        createManager.CheckPlaceable();
        createManager.selected = "";
        createManager.selectedIndex = -1;
        createManager.TurnOffSlots();
    }

    //Get the faded image of the placed beast
    string GetFadedImage()
    {
        if(createManager.selected == "Gaia")
        {
            return "EmptyRectangle";
        }
        else if(createManager.selected == "Cthulhu")
        {
            return "EmptyRectangle";
        }
        else if(createManager.selected == "Trogdor")
        {
            return "EmptyRectangle";
        }
        else if(createManager.selected == "Naglfar")
        {
            return "EmptyRectangle";
        }
        else if(createManager.selected == "Behemoth")
        {
            return "EmptyRectangle";
        }
        else if(createManager.selected == "Sunbather")
        {
            return "EmptyRectangle";
        }
        else
        {
            return "";
        }
    }

    //Select this beast and give options to move to another slot or remove from the grid
    void EditPlace()
    {
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
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(createPoolLoader.summonedImages[createManager.selectedIndex]);
        gameObject.GetComponent<Image>().color = Color.white;
        SetSlot();
        createManager.RemoveSlotImage();
    }
}
