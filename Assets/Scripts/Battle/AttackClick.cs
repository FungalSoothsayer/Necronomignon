﻿using System.Collections;
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
                battleManager.selectedEnemy = GetName();
                GameObject slot = GetSlot();
                /*GameObject selector = GameObject.Find("EnemySelected");
                RectTransform slotTransform = slot.GetComponent<RectTransform>();
                RectTransform selectorTransform = selector.GetComponent<RectTransform>();
                selectorTransform.position = slotTransform.position;*/
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

        if (num <= battleManager.enemySlots.Count && battleManager.enemySlots[num-1] != null)
        {
            return battleManager.enemySlots[num - 1];
        }
        else
        { 
            return null; 
        }
    }

    GameObject GetSlot()
    {
        for (int x = 0; x < battleManager.enemySlots.Count; x++)
        {
            if (battleManager.enemySlots[x] != null && battleManager.selectedEnemy.name == battleManager.enemySlots[x].name)
            {
                return battleManager.enemyPadSlots[x];
            }
        }

        return null;
    }
}
