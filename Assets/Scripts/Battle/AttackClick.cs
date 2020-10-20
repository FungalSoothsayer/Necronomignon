using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BattleManager battleManager;
    //public BeastManager beastManager;

   bool mouse_over;
    
    void Start() 
    {
    }

    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*
                if (battleManager.roundOrderTypes[battleManager.turn] == "Player")
                {
                    battleManager.Attack(GetName());
                }
                */
                battleManager.selectedEnemy = GetName();
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
        print(gameObject.name);
        if (gameObject.name == "Slot1") return battleManager.enemySlot1;
        else if (gameObject.name == "Slot2") return battleManager.enemySlot2;
        else if (gameObject.name == "Slot3") return battleManager.enemySlot3;
        else if (gameObject.name == "Slot4") return battleManager.enemySlot4;
        else if (gameObject.name == "Slot5") return battleManager.enemySlot5;
        else if (gameObject.name == "Slot6") return battleManager.enemySlot6;
        else return null;
    }
}
