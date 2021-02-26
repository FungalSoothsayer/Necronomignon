using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSlime_Script : MonoBehaviour, Parent_Beast
{
    BattleManager battleManager;

    void Start()
    {
        GameObject g = GameObject.Find("GameManager");

        if (g != null)
        {
            battleManager = g.GetComponent<BattleManager>();
        }
    }

    public void back_special()
    {
        int slot = -1;
        int ran;
        if (battleManager.roundOrderTypes[battleManager.turn] == "Player" && !battleManager.isSquadFull("Player"))
        {
            while (slot == -1)
            {
                ran = Random.Range(0, Values.SMALLSLOT);
                if(battleManager.slots[ran] == null || battleManager.slots[ran].speed == 0 || battleManager.slots[ran].hitPoints <= 0)
                {
                    battleManager.playerPadSlots[ran].transform.gameObject.SetActive(true);
                    GameObject beastPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Beasts/" + "DreamSlime"));
                    beastPrefab.transform.SetParent(battleManager.playerPadSlots[ran].transform);
                    beastPrefab.transform.localPosition = new Vector3(0, 0);
                    beastPrefab.transform.localRotation = Quaternion.identity;
                    slot = ran;
                }
            }
            battleManager.slots[slot] = BeastManager.getFromNameS("DreamDummy");
            battleManager.attackPool.Add(battleManager.slots[slot]);
        }
        else if (battleManager.roundOrderTypes[battleManager.turn] == "Enemy" && !battleManager.isSquadFull("Enemy"))
        {
            while (slot == -1)
            {
                ran = Random.Range(0, Values.SMALLSLOT);
                if (battleManager.slots[ran] == null || battleManager.slots[ran].speed == 0 || battleManager.slots[ran].hitPoints <= 0)
                {
                    battleManager.enemyPadSlots[ran].transform.gameObject.SetActive(true);
                    GameObject beastPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Beasts/" + "DreamSlime"));
                    beastPrefab.transform.SetParent(battleManager.enemyPadSlots[ran].transform);
                    beastPrefab.transform.localPosition = new Vector3(0, 0);
                    beastPrefab.transform.localRotation = Quaternion.identity;
                    slot = ran;
                }
            }
            battleManager.enemySlots[slot] = BeastManager.getFromNameS("DreamDummy");
            battleManager.enemyAttackPool.Add(battleManager.enemySlots[slot]);
        }
    }

    public void front_special()
    {
        
    }
}
