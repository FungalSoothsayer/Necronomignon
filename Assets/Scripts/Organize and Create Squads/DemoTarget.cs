using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemoTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DemoBattleManager demoBattleManager;

    private bool mouse_over = false;

    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                demoBattleManager.TakeTurn();
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
}
