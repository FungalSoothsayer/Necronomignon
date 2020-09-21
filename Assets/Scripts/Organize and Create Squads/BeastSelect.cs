using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeastSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CreateManager createManager;
    public CreatePoolLoader createPoolLoader;

    private bool mouse_over = false;
    void Update()
    {
        if (mouse_over)
        {
            //When mouse is clicked and cursor is over this image, light up the slots
            if (Input.GetMouseButtonDown(0))
            {
                if (NotSummoned() && !createManager.saveMode)
                {
                    if (createManager.canBePlaced)
                    {
                        createManager.LightUpSlots();
                        createManager.placing = true;
                    }
                    createManager.selectedIndex = GetThisBeast();
                    createManager.selected = createPoolLoader.summoned[createManager.selectedIndex];

                }
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

    //Get the index of the beast that is selected
    private int GetThisBeast()
    {
        if (gameObject.name == "Pool1") return 0;
        else if (gameObject.name == "Pool2") return 1;
        else if (gameObject.name == "Pool3") return 2;
        else if (gameObject.name == "Pool4") return 3;
        else if (gameObject.name == "Pool5") return 4;
        else if (gameObject.name == "Pool6") return 5;
        else if (gameObject.name == "Pool7") return 6;
        else if (gameObject.name == "Pool8") return 7;
        else if (gameObject.name == "Pool9") return 8;
        else return 0;
    }

    //Check to make sure that this beast is not already in the grid
    bool NotSummoned()
    {
        string beast = createPoolLoader.summoned[GetThisBeast()];
        if (createManager.slot1 != beast && createManager.slot2 != beast && createManager.slot3 != beast
        && createManager.slot4 != beast && createManager.slot5 != beast && createManager.slot6 != beast)
        {
            return true;
        }
        else return false;
    }
}
