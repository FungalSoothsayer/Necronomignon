using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conglomerate_Script : MonoBehaviour, Parent_Beast
{
    BattleManager battleManager;
    [SerializeField] GameObject backPrefab;
    int currentSlot;
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
        ProjectileAnimation(false);

        currentSlot = battleManager.getCurrentBeastSlot();
        battleManager.targets.Clear();
        bool front = false;
        Beast enemy = battleManager.GetPlayerTarget();

        print(enemy.name);
        battleManager.targets.Add(enemy);

        if (battleManager.roundOrderTypes[battleManager.turn] == "Player")
        {
            battleManager.attack.InitiateAttack(battleManager.currentTurn, battleManager.targets, front, Player.summoner);
        }
        else
        {
            battleManager.attack.InitiateAttack(battleManager.currentTurn, battleManager.targets, front, battleManager.enemySummoner);
        }

    }

    public void front_special()
    {
        
    }

    void ProjectileAnimation(bool front)
    {
        GameObject target = battleManager.getSlot(battleManager.targets[0]);

        GameObject movePrefab = Instantiate(backPrefab);
        movePrefab.transform.SetParent(target.transform);
        movePrefab.transform.localPosition = new Vector3(0, 0);
        movePrefab.transform.localRotation = Quaternion.identity;
        movePrefab.transform.localScale = new Vector3(50, 50);
    }
}
