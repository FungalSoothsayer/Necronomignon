﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Kitsune_Script : MonoBehaviour, Parent_Beast
{
    BattleManager battleManager;
    [SerializeField] GameObject backPrefab;

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
        ProjectileAnimation();
    }

    public void front_special() 
    {
        battleManager.PlayDamagedAnimation(battleManager.targets[0]);
    }

    void ProjectileAnimation()
    {
        GameObject player = battleManager.getSlot(battleManager.currentTurn);
        GameObject target = battleManager.getSlot(battleManager.targets[0]);

        GameObject movePrefab = Instantiate(backPrefab);
        movePrefab.transform.SetParent(player.transform);
        movePrefab.transform.localPosition = new Vector3(0, 0);
        movePrefab.transform.localRotation = Quaternion.identity;
        movePrefab.transform.localScale = new Vector3(30, 30);

        Vector3 shootDir = ((target.transform.localPosition) - (player.transform.localPosition)).normalized;

        movePrefab.GetComponent<Projectile>().Setup(shootDir);
    }
}
