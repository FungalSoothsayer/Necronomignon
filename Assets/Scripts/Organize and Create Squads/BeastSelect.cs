using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
                    
                    createManager.selectedIndex = GetThisBeast();
                    if (createManager.selectedIndex < createPoolLoader.summoned.Count)
                    {
                        if (createManager.canBePlaced)
                        {
                            createManager.LightUpSlots();
                            createManager.placing = true;
                        }
                        createManager.selected = createPoolLoader.summoned[createManager.selectedIndex];
                    }
                    

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
        string str = gameObject.name;
        char chr = str.ToCharArray()[str.Length - 1];
        int num = int.Parse(chr.ToString());
        if (num.GetType() == 1.GetType())
        {
            return (num - 1);
        }
        else return 0;
    }

    //Check to make sure that this beast is not already in the grid
    bool NotSummoned()
    {
        if (GetThisBeast() >= createPoolLoader.summoned.Count)
        {
            return true;
        }
        Beast beast = createPoolLoader.summoned[GetThisBeast()];

        if(beast == null)
        {
            return false;
        }
        for(int x = 0; x < createManager.slots.Count; x++)
        {
            if (beast.Equals(createManager.slots[x]))
            {
                return false;
            }
        }
        return true;
        /*if (beast != null && !beast.Equals(createManager.slots[0]) && !beast.Equals(createManager.slots[1]) && !beast.Equals(createManager.slots[2])
        && !beast.Equals(createManager.slots[3]) && !beast.Equals(createManager.slots[4]) && !beast.Equals(createManager.slots[5]))
        {
            return true;
        }
        else return false;*/
    }
}
