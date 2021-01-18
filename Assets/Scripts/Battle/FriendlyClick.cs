using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//allows the player to click on a friendly target
public class FriendlyClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BattleManager battleManager;

    bool mouse_over;

    void Start()
    {
        disableSelected();
    }

    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Trying to make it work when they are child gameobjects
                //disableSelected();
                //battleManager.selectedFriend = GetName();
                //GameObject slot = GetSlot();
                //slot.transform.GetChild(2).gameObject.SetActive(true);

                /*battleManager.selectedFriend = GetName();
                GameObject slot = GetSlot();
                GameObject selector = GameObject.Find("FriendlySelected");
                Transform slotTransform = slot.GetComponent<RectTransform>();
                Transform selectorTransform = selector.GetComponent<RectTransform>();
                selectorTransform.position = slotTransform.position;*/
                //selectorTransform.position = new Vector3(slotTransform.position.x, slotTransform.position.y, slotTransform.position.z);
                //selectorTransform.position = new Vector3(0, 0, 0);
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
    //gets the slot that represents the beast
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

    void disableSelected()
    {
        foreach (GameObject slot in battleManager.playerPadSlots)
        {
            slot.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
