using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FriendlyClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BattleManager battleManager;

    bool mouse_over;

    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                battleManager.selectedFriend = GetName();
                //GameObject slot = GetSlot();
                //print(slot.transform.GetChild(2));

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    //When cursor leaves this image, make mouse_over false
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    //Get the name of the beast that this slot is holding
    Beast GetName()
    {
        string str = gameObject.name;
        int num = int.Parse((str).ToCharArray()[str.Length - 1].ToString());

        if (num <= battleManager.slots.Count && battleManager.slots[num - 1] != null)
        {
            return battleManager.slots[num - 1];
        }
        else
        {
            return null;
        }
    }

    GameObject GetSlot()
    {
        for (int x = 0; x < battleManager.slots.Count; x++)
        {
            if (battleManager.slots[x] != null && battleManager.selectedFriend.name == battleManager.slots[x].name)
            {
                return battleManager.playerPadSlots[x];
            }
        }

        return null;
    }
}
