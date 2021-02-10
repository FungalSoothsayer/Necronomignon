using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialClone : AbStraSpecial
{
    public override void special()
    {
        GameObject go = GameObject.Find("GameManager");
        if(go != null)
        {
            battleManager = go.GetComponent<BattleManager>();
        }
        Beast caster = battleManager.currentTurn;
        bool player = battleManager.roundOrderTypes[battleManager.turn] == "player";
        Beast dummy = new Beast();
        List<Beast> potentialSpace = new List<Beast>();
        if (player)
        {
            potentialSpace = battleManager.slots;
        }
        else
        {
            potentialSpace = battleManager.enemySlots;
        }
        List<int> listNumbers = new List<int>();
        int number;
        for (int i = 0; i < 6; i++)
        {
            do
            {
                number = Random.Range(0, 4);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }

        for(int x = 0; x < listNumbers.Count; x++)
        {
            if(potentialSpace[listNumbers[x]] == null || potentialSpace[listNumbers[x]].speed == 0)
            {
                battleManager.playerPadSlots[listNumbers[x]].GetComponent<Image>().sprite = battleManager.getSlot(caster).GetComponent<Image>().sprite;
                potentialSpace[listNumbers[x]] = dummy;
            }
        }
    }
}
