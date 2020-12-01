//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public bool eRunning = false;
    public bool pRunning = false;

    public List<GameObject> playerPadSlots = new List<GameObject>(6);
    public List<GameObject> enemyPadSlots = new List<GameObject>(6);

    public HealthManager healthManager;
    public Attack attack;
    public LoadMission loadMission;

    public Text txtTurn;
    Animator anim;

    public List<Beast> players = new List<Beast>();
    public List<Beast> enemies = new List<Beast>();
    public List<Beast> roundOrder = new List<Beast>();
    public List<string> roundOrderTypes = new List<string>();
    List<Beast> attackPool = new List<Beast>();
    List<Beast> enemyAttackPool = new List<Beast>();

    public int turn = 0;
    int totalMoves;
    int totalBeasts = 8;

    public Beast currentTurn;
    public Beast selectedEnemy;
    public Beast selectedFriend;

    public List<Image> orderBar = new List<Image>();

    public List<Beast> slots = new List<Beast>();
    public List<Beast> enemySlots;

    public bool[] playersActive = { true, true, true, true };
    public bool[] enemiesActive= {true, true, true, true};

    List<int> playersTurnsTaken = new List<int>();
    List<int> enemiesTurnsTaken = new List<int>();

    //Get lists from LoadMission and add the players to the attack pool
    public void SendLists(List<Beast> thisSquad, List<Beast> enemySquad, List<HealthBar> activePlayersHealth, List<HealthBar> activeEnemiesHealth, List<DamageOutput> activePlayerDamage, List<DamageOutput> activeEnemyDamage)
    {
        selectedEnemy = enemySquad[0];
        for(int x = 0; x < enemySquad.Count; x++)
        {
            if(enemySquad[x].hitPoints < selectedEnemy.hitPoints)
            {
                selectedEnemy = enemySquad[x];
            }
        }

        enemies = enemySquad;
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);

        enemyAttackPool.Add(enemies[0]);
        if (enemies[1] != null)
        {
            enemyAttackPool.Add(enemies[1]);
        }
        else
        {
            enemiesActive[1] = false;
            totalBeasts--;
        }
        if (enemies[2] != null)
        {
            enemyAttackPool.Add(enemies[2]);
        }
        else
        {
            enemiesActive[2] = false;
            totalBeasts--;
        }
        if (enemies[3] != null)
        {
            enemyAttackPool.Add(enemies[3]);
        }
        else
        {
            enemiesActive[3] = false;
            totalBeasts--;
        }

        players = thisSquad;
        players.Add(null);
        players.Add(null);
        players.Add(null);

        attackPool.Add(players[0]);
        if (players[1] != null)
        {
            
            attackPool.Add(players[1]);
        }
        else
        {
            playersActive[1] = false;
            totalBeasts--;
        }
        if (players[2] != null)
        {
            
            attackPool.Add(players[2]);
        }
        else
        {
            playersActive[2] = false;
            totalBeasts--;
        }
        if (players[3] != null)
        {
            
            attackPool.Add(players[3]);
        }
        else
        {
            playersActive[3] = false;
            totalBeasts--;
        }

        for(int x = 0; x < 4; x++)
        {
            if(players[x] != null)
            {
                players[x].hitPoints = players[x].maxHP;
            }
            if (enemies[x] != null)
            {
                enemies[x].hitPoints = enemies[x].maxHP;
            }
        }

        healthManager.GetHealth(players, enemies, activePlayersHealth, activeEnemiesHealth, activePlayerDamage, activeEnemyDamage);
        LoadOrder();
    }

    //Get the slot info for each beast from LoadMission
    public void GetSlots(List<Beast> s, List<Beast> e)
    {
        slots.Clear();
        enemySlots.Clear();
        for (int x = 0; x < 6; x++)
        {
            slots.Add(s[x]);
            enemySlots.Add(e[x]);
        }
    }
    public void GetSlots(Beast s1, Beast s2, Beast s3, Beast s4, Beast s5, Beast s6, Beast e1, Beast e2, Beast e3, Beast e4, Beast e5, Beast e6)
    {
        slots.Clear();
        enemySlots.Clear();
        slots.Add(s1);
        slots.Add(s2);
        slots.Add(s3);
        slots.Add(s4);
        slots.Add(s5);
        slots.Add(s6);

        enemySlots.Add(e1);
        enemySlots.Add(e2);
        enemySlots.Add(e3);
        enemySlots.Add(e4);
        enemySlots.Add(e5);
        enemySlots.Add(e6);
    }

    //Create attack order
    void LoadOrder()
    {
        int[] moves = new int[8];

        for(int x = 0; x < 4; x++)
        {
            if (playersActive[x] && players[x] != null)
            {
                moves[x] = players[x].number_MOVES;
                if (inFront(players[x]))
                {
                    moves[x] += players[x].Move_A.number_of_moves;
                }
                else
                {
                    moves[x] += players[x].Move_B.number_of_moves;
                }
            }
            if (enemiesActive[x] && enemies[x] != null)
            {
                moves[x+4] = enemies[x].number_MOVES;
                enemies[x].setAttacks();
                if (inFront(enemies[x]))
                {
                    moves[x + 4] += enemies[x].Move_A.number_of_moves;
                }
                else
                {
                    moves[x + 4] += enemies[x].Move_B.number_of_moves;
                }

            }
        }

        totalMoves = moves.Sum();

        List<int> Speed = new List<int>();

        /*List<float> playZap = new List<float>();
        List<float> enemZap = new List<float>();
        playZap.Clear();
        enemZap.Clear();*/
        for (int x = 0; x < 8; x++)
        {
            /*if (x < 4 && players[x] != null)
            {
                if (x < 4 && playersActive[x] && players[x].statusTurns[(int)Beast.types.Air] > 0)
                {
                    playZap.Add(0.5f);
                }
                else
                {
                    playZap.Add(1);
                }
            }
            else
            {
                playZap.Add(0);
            }
            if (x > 3 && enemies[x % 4] != null)
            {
                if (x >= 4 && enemiesActive[x % 4] && enemies[x % 4].statusTurns[(int)Beast.types.Air] > 0)
                {
                    enemZap.Add(0.5f);
                }
                else if (x >= 4)
                {
                    enemZap.Add(1);
                }
            }
            else
            {
                enemZap.Add(0);
            }*/
            if (x < 4 && players[x] != null)
            {
                Speed.Add((int)Mathf.Floor((float)players[x].speed/* * playZap[x]*/));
            }
            else if (x > 3 && enemies[x % 4] != null)
            {
                Speed.Add((int)Mathf.Floor((float)enemies[x % 4].speed /** enemZap[x % 4]*/));
            }
            else
            {
                Speed.Add(0);
            }
        }

        bool[] beastActive = { playersActive[0], playersActive[1], playersActive[2], playersActive[3], enemiesActive[0], enemiesActive[1], enemiesActive[2], enemiesActive[3] };
        int i = 0;

        List<string> wave = new List<string>();

        //Create an array with each speed and sort it highest to lowest
        Speed.Sort();
        Speed.Reverse();

        //Clear the previous round order
        roundOrder.Clear();
        roundOrderTypes.Clear();
        //Loop through the speed array and find the corresponding beast and add them to the round order
        while (i < totalMoves)
        {
            for(int y = 0; y < 8; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    if (x<4 && beastActive[x] && Speed[y] == players[x].speed/**playZap[x]*/ && moves[x] > 0 && !InWave("Player " + players[x].name, wave))
                    {
                        roundOrder.Add(players[x]);
                        roundOrderTypes.Add("Player");
                        wave.Add("Player " + players[x].name);
                        moves[x]--;
                        i++;
                        break;
                    }
                    else if(x>=4 && beastActive[x] && Speed[y] == enemies[x%4].speed/**enemZap[x%4]*/ && moves[x] > 0 && !InWave("Enemy " + enemies[x % 4].name, wave))
                    {
                        roundOrder.Add(enemies[x % 4]);
                        roundOrderTypes.Add("Enemy");
                        wave.Add("Enemy " + enemies[x % 4].name);
                        moves[x]--;
                        i++;
                        break;
                    }
                }
            }
            wave.Clear();
        }
        if (turn < roundOrder.Count)
        {
            currentTurn = roundOrder[turn];
            txtTurn.text = roundOrderTypes[0] + " " + currentTurn.name + "'s turn \n HP left: " + currentTurn.hitPoints;
        }
        if (roundOrderTypes[turn] == "Enemy" && attackPool.Count > 0)
        {
            if (!eRunning && !pRunning)
            {
                StartCoroutine(EnemyAttack());
            }
        }
        else if (roundOrderTypes[turn] == "Player" && enemyAttackPool.Count > 0)
        {
            if (!eRunning && !pRunning)
            {
                StartCoroutine(PlayerAttack());
            }
        }
        UpdateOrderBar();
    }

    //Check to see of beast is used in the loop yet
    bool InWave(string beast, List<string> wave)
    {
        for (int i = 0; i < wave.Count; i++)
        {
            if (beast == wave[i])
            {
                return true;
            }
        }
        return false;
    }

    void UpdateOrderBar()
    {
        currentTurn = roundOrder[turn];
        for (int x = 0; x < orderBar.Count; x++)
        {
            try
            {
                orderBar[x].sprite = Resources.Load<Sprite>(GetImage(roundOrder[x + turn]));
            }
            catch
            {
                orderBar[x].sprite = Resources.Load<Sprite>("EmptyRectangle");
            }
        }
    }

    void TakeTurn()
    {
        turn++;
        currentTurn = roundOrder[turn]; 
        txtTurn.text = roundOrderTypes[turn] + " " + currentTurn + " 's turn";

        //If it is enemy turn, start their attack
        if (roundOrderTypes[turn] == "Enemy")
        {
            if (!eRunning && !pRunning)
            {
                StartCoroutine(EnemyAttack());
            }
            UpdateOrderBar();
        }
        else if (roundOrderTypes[turn] == "Player")
        {
            if (!eRunning && !pRunning)
            {
                StartCoroutine(PlayerAttack());
            }
            UpdateOrderBar();
        }
        bool justNow = false;
        for(int x =0;x< currentTurn.statusTurns.Length;x++)
        {
            justNow = false;
            if (currentTurn.statusTurns[x] > 0 && (int)Move.types.Corrupt != x)
            {
                currentTurn.statusTurns[x]--;
                justNow = true;
            }
            if(currentTurn.statusTurns[x] > 0 && x == (int)Move.types.Burn)
            {
                healthManager.UpdateHealth(currentTurn, 5);
            }
            if(currentTurn.statusTurns[x] > 0 && x == (int)Move.types.Poison)
            {
                healthManager.UpdateHealth(currentTurn,(int) Mathf.Ceil((float)currentTurn.hitPoints*.05f));
            }
        }
        for(int x =0; x< currentTurn.buffs.Count; x++)
        {

            if(currentTurn.buffs[x].turnsLeft <= 0)
            {
                currentTurn.buffs.RemoveAt(x);
                x--;
            }
            else
            {
                currentTurn.buffs[x].turnsLeft--;
            }
        }
    }

    List<Beast> findRowTargets()
    {
        List<Beast> targets = new List<Beast>();
        int slot = getCurrentBeastSlot();
        if (roundOrderTypes[turn] == "Player")
        {
            for(int x = 0; x < enemySlots.Count; x++)
            {
                if (x < 3 && enemySlots[x] != null && enemySlots[x].hitPoints > 0)
                {
                    if(slot+1 == x || slot == x || slot-1 == x)
                    {
                        targets.Add(enemySlots[x]);
                    }
                }
                else if(x>=3 && enemySlots[x] != null && enemySlots[x].hitPoints > 0)
                {
                    if (targets.Count - (x - 3) >= 1)
                    {
                        break;
                    }
                    if (slot + 1 == x || slot == x || slot - 1 == x)
                    {
                        targets.Add(enemySlots[x]);
                    }
                }
            }
            if (targets.Count <= 0)
            {
                for (int x = 0; x < enemySlots.Count; x++)
                {
                    if (x < 3 && enemySlots[x] != null && enemySlots[x].hitPoints > 0)
                    {
                            targets.Add(enemySlots[x]);                        
                    }
                    else if (x >= 3 && enemySlots[x] != null && enemySlots[x].hitPoints > 0)
                    {
                        if (targets.Count>0)
                        {
                            break;
                        }
                        targets.Add(enemySlots[x]);
                        
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (x < 3 && slots[x] != null && slots[x].hitPoints >0)
                {
                    if (slot + 1 == x || slot == x || slot - 1 == x)
                    {
                        targets.Add(slots[x]);
                    }
                }
                else if (x >= 3 && slots[x] != null && slots[x].hitPoints > 0)
                {
                    if (targets.Count - (x - 3) >= 1)
                    {
                        break;
                    }
                    if (slot + 1 == x || slot == x || slot - 1 == x)
                    {
                        targets.Add(slots[x]);
                    }
                }
            }
            if (targets.Count <= 0)
            {
                for (int x = 0; x < slots.Count; x++)
                {
                    if (x < 3 && slots[x] != null && slots[x].hitPoints > 0)
                    {
                        targets.Add(slots[x]);
                    }
                    else if (x >= 3 && slots[x] != null && slots[x].hitPoints > 0)
                    {
                        if (targets.Count >0)
                        {
                            break;
                        }
                        targets.Add(slots[x]);
                    }
                }
            }
        }
        return targets;
    }

    int getCurrentBeastSlot()
    {
        int slot = -1;
        for (int x = 0; x < slots.Count; x++)
        {
            if (roundOrderTypes[turn] == "Player" && currentTurn.Equals(slots[x]))
            {
                slot = x;
                break;
            }
            else if (roundOrderTypes[turn] == "Enemy" && currentTurn.Equals(enemySlots[x]))
            {
                slot = x;
                break;
            }
        }
        return slot;
    }
    int getCurrentBeastSlot(Beast b)
    {
        int slot = -1;
        for (int x = 0; x < slots.Count; x++)
        {
            if (b.Equals(enemySlots[x]) || b.Equals(slots[x]))
            {
                slot = x;
                break;
            }
        }
        return slot;
    }

    List<Beast> findColumnTargets()
    {
        List<Beast> targets = new List<Beast>();
        int slot = getCurrentBeastSlot();
        if (roundOrderTypes[turn] == "Enemy")
        {
            switch (slot % 3)
            {
                case 0:
                    do
                    {
                        if (slots[slot % 3] != null && slots[slot % 3].hitPoints > 0)
                        {
                            targets.Add(slots[slot % 3]);
                        }
                        if (slots[(slot % 3) + 3] != null && slots[(slot % 3) +3].hitPoints > 0)
                        {
                            targets.Add(slots[(slot % 3) + 3]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 1:
                    do
                    {
                        if (slots[slot % 3] != null && slots[slot % 3].hitPoints > 0)
                        {
                            targets.Add(slots[slot % 3]);
                        }
                        if (slots[(slot % 3) + 3] != null && slots[(slot % 3) + 3].hitPoints > 0)
                        {
                            targets.Add(slots[(slot % 3) + 3]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 2:
                    do
                    {
                        if (slots[slot % 3] != null && slots[slot % 3].hitPoints > 0)
                        {
                            targets.Add(slots[slot % 3]);
                        }
                        if (slots[(slot % 3) + 3] != null && slots[(slot % 3) +3].hitPoints > 0)
                        {
                            targets.Add(slots[(slot % 3) + 3]);
                        }
                        slot--;
                    } while (targets.Count < 1);
                    break;

            }
        }
        else
        {
            switch (slot % 3)
            {
                case 0:
                    do
                    {
                        if (enemySlots[slot % 3] != null && enemySlots[slot % 3].hitPoints >0)
                        {
                            targets.Add(enemySlots[slot % 3]);
                        }
                        if (enemySlots[(slot % 3) + 3] != null && enemySlots[(slot % 3)  +  3].hitPoints > 0)
                        {
                            targets.Add(enemySlots[slot % 3 + 3]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 1:
                    do
                    {
                        if (enemySlots[slot % 3] != null && enemySlots[slot % 3].hitPoints > 0)
                        {
                            targets.Add(enemySlots[slot % 3]);
                        }
                        if (enemySlots[(slot % 3) + 3] != null && enemySlots[(slot % 3) + 3].hitPoints > 0)
                        {
                            targets.Add(enemySlots[(slot % 3) + 3]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 2:
                    do
                    {
                        if (enemySlots[slot % 3] != null && enemySlots[slot % 3].hitPoints > 0)
                        {
                            targets.Add(enemySlots[slot % 3]);
                        }
                        if (enemySlots[(slot % 3) + 3] != null && enemySlots[(slot % 3) + 3].hitPoints > 0)
                        {
                            targets.Add(enemySlots[(slot % 3) + 3]);
                        }
                        slot--;
                    } while (targets.Count < 1);
                    break;

            }
        }
        return targets;
    }

    public void Attack(Beast target)
    {
        bool inFront = this.inFront();
        bool guarded = this.guarded(target);

        bool cancelGuard = false;

        List<Beast> targets = new List<Beast>();

        targets.Add(target);



        if (inFront)
        {
            if (currentTurn.Move_A.healing)
            {
                targets.Clear();
                targets.Add(this.getWeakestFriend());
                cancelGuard = true;
            }
            else if (currentTurn.Move_A.rowAttack)
            {
                targets.Clear();
                targets = findRowTargets();
                cancelGuard = true;
            }
            else if (currentTurn.Move_A.columnAttack)
            {
                targets.Clear();
                targets = findColumnTargets();
                cancelGuard = true;
            }
            else if (currentTurn.Move_A.multiAttack)
            {
                targets.Clear();

                int ran = Random.Range(2, 6);
                for (; ran > 0; ran--)
                {
                    targets.Add(target);
                }
            }
        }
        else if (!inFront)
        {
            if (currentTurn.Move_B.healing)
            {
                targets.Clear();
                targets.Add(this.getWeakestFriend());
                cancelGuard = true;
            }
            else if (currentTurn.Move_B.rowAttack)
            {
                targets.Clear();
                targets = findRowTargets();
                cancelGuard = true;
            }
            else if (currentTurn.Move_B.columnAttack)
            {
                targets.Clear();
                targets = findColumnTargets();
                cancelGuard = true;
            }
            else if (currentTurn.Move_B.multiAttack)
            {
                targets.Clear();

                int ran = Random.Range(2, 6);
                for (; ran > 0; ran--)
                {
                    targets.Add(target);
                }
            }
        }
        if (guarded && !cancelGuard)
        {
            int slot = getCurrentBeastSlot(targets[targets.Count-1]);
            targets.Clear();
            Beast b = new Beast();
            if (roundOrderTypes[turn] == "Player")
            {
                for (int x = 3; x < enemySlots.Count; x++)
                {
                    if (x == slot && enemySlots[x % 3] != null && enemySlots[x % 3].hitPoints > 0)
                    {
                        b = enemySlots[x % 3];
                    }
                }
            }
            else
            {
                for (int x = 3; x < slots.Count; x++)
                {
                    if (x == slot && slots[x % 3] != null && slots[x % 3].hitPoints > 0)
                    {
                        b = slots[x % 3];
                    }
                }
            }
            targets.Add(b);
            if (inFront && currentTurn.Move_A.multiAttack)
            {
                int ran = Random.Range(1, 5);
                for (; ran > 0; ran--)
                {
                    targets.Add(targets[0]);
                }
            }
            else if (currentTurn.Move_B.multiAttack)
            {
                int ran = Random.Range(1, 5);
                for (; ran > 0; ran--)
                {
                    targets.Add(targets[0]);
                }
            }
        }

        if (currentTurn.statusTurns[(int)Move.types.Confusion] > 0)
        {
            targets.Clear();
            if (roundOrderTypes[turn] == "Player")
            {
                targets.Add(GetEnemyTarget());
            }
            else
            {
                targets.Add(GetPlayerTarget());
            }
        }

        if (currentTurn.cursed != null)
        {
            if (currentTurn.cursed.hitPoints <= 0)
            {
                currentTurn.cursed = null;
                currentTurn.curseCharge = 0;

            }
            else
            {
                targets.Clear();
                targets.Add(currentTurn.cursed);
            }
        }

        //Check to see if the round is still going and then run an attack
        if (turn >= totalMoves - 1)
        {
            attack.InitiateAttack(currentTurn, targets, inFront);
            GameObject slot = getSlot();
            if (!slot.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Front") &&
                !slot.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Back"))
            {
                if (pRunning) pRunning = false;
                if (eRunning) eRunning = false;
            }
            PlayAttackAnimation(inFront);
            PlayDamagedAnimation(targets);
            Debug.Log("Round Ended");
            ClearTurns();
            currentTurn = roundOrder[0];
            txtTurn.text = roundOrderTypes[0] + " " + currentTurn + "'s turn";
            turn = 0;
            if (healthManager.playersLeft > 0 && healthManager.enemiesLeft > 0 && roundOrderTypes[turn] == "Enemy")
            {
                StartCoroutine(EnemyAttack());
                /*
                if (!eRunning && !pRunning)
                {
                    StartCoroutine(EnemyAttack());
                }
                */
            }
            else if (healthManager.enemiesLeft > 0 && healthManager.playersLeft > 0 && roundOrderTypes[turn] == "Player")
            {
                StartCoroutine(PlayerAttack());
                /*
                if (!eRunning && !pRunning)
                {
                    StartCoroutine(PlayerAttack());
                }
                */
            }
            turn = 0;
        }
        else
        {
            attack.InitiateAttack(currentTurn, targets, inFront);
            GameObject slot = getSlot();
            if (!slot.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Front") &&
                !slot.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Back"))
            {
                if (pRunning) pRunning = false;
                if (eRunning) eRunning = false;
            }
            PlayAttackAnimation(inFront);
            PlayDamagedAnimation(targets);
            AddTurn();
            Beast b = new Beast();
            if(turn+1 >= roundOrder.Count)
            {
                while (!b.Equals(currentTurn))
                {
                    b = roundOrder[turn];
                    turn--;
                }
            }
            TakeTurn();
        }
    }

    //Returns slot GameObject of currentturn.
    GameObject getSlot()
    {
        if (roundOrderTypes[turn] == "Player")
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (slots[x] != null && currentTurn.name == slots[x].name)
                {
                    return playerPadSlots[x];
                }
            }
        }
        else if (roundOrderTypes[turn] == "Enemy")
        {
            for (int x = 0; x < enemySlots.Count; x++)
            {
                if (enemySlots[x] != null && currentTurn.name == enemySlots[x].name)
                {
                    return enemyPadSlots[x];
                }
            }
        }

        return null;
    }

    void PlayAttackAnimation(bool inFront)
    {
        GameObject slot = getSlot();

        if (inFront) slot.GetComponent<Animator>().SetTrigger("Front");
        else slot.GetComponent<Animator>().SetTrigger("Back");
    }

    void PlayDamagedAnimation(List<Beast> targets)
    {
        foreach (Beast target in targets)
        {
            if (roundOrderTypes[turn] == "Player")
            {
                for (int x = 0; x < enemySlots.Count; x++)
                {
                    if (enemySlots[x] != null && enemySlots[x].name == target.name)
                    {
                        enemyPadSlots[x].gameObject.GetComponent<Animator>().SetTrigger("GetHit");
                        break;
                    }
                }
            }
            else if (roundOrderTypes[turn] == "Enemy")
            {
                for (int x = 0; x < slots.Count; x++)
                {
                    if (slots[x] != null && slots[x].name == target.name)
                    {
                        playerPadSlots[x].gameObject.GetComponent<Animator>().SetTrigger("GetHit");
                        break;
                    }
                }
            }
        }
    }

    IEnumerator EnemyAttack()
    {
        eRunning = true;
        yield return new WaitForSeconds(2f);
        //eRunning = false;
        if (attackPool.Count > 0)
            Attack(GetEnemyTarget());
    }

    //Enemy targets a random player from a pool of active player beasts
    Beast GetEnemyTarget()
    {
        int rand = Random.Range(0, attackPool.Count);

        Beast b = attackPool[rand];
        return b;
    }
    Beast GetPlayerTarget()
    {
        int rand = Random.Range(0, enemyAttackPool.Count);

        Beast b = enemyAttackPool[rand];        
        return b;
    }

    IEnumerator PlayerAttack()
    {
        pRunning = true;
        yield return new WaitForSeconds(2f);
        //pRunning = false;
        if (enemyAttackPool.Count > 0)
            Attack(selectedEnemy);
    }

    //Get the row to determine whether the attacker is using an A move or a B move
    bool inFront()
    {
        for(int x = 0; x< slots.Count; x++)
        {
            if(x<3 && (currentTurn.Equals(slots[x]) || currentTurn.Equals(enemySlots[x])))
            {
                return true;
            }
        }
        return false;
    }

    bool guarded(Beast b)
    {
        if (inFront(b))
        {
            return false;
        }
        int slot = -1;
        for (int x = 0; x < slots.Count; x++)
        {
            if ( b.Equals(slots[x]) )
            {
                slot = x;
                break;
            }
            else if (b.Equals(enemySlots[x]))
            {
                slot = x;
                break;
            }
        }
        if (roundOrderTypes[turn] == "Player")
        {
            for (int x = 3; x < enemySlots.Count; x++)
            {
                if(x==slot && enemySlots[x%3] != null && enemySlots[x%3].hitPoints > 0)
                {
                    return true;
                }
            }
        }
        else
        {
            for (int x = 3; x < slots.Count; x++)
            {
                if (x == slot && slots[x % 3] != null && slots[x%3].hitPoints > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool inFront(Beast b)
    {
        for (int x = 0; x < slots.Count; x++)
        {
            if (x < 3 && (b.Equals(slots[x]) || b.Equals(enemySlots[x])))
            {
                return true;
            }
        }
        return false;
    }

    //Remove the desired beast by setting its active variable to false and removing image
    public void RemoveBeast(Beast target)
    {
        totalBeasts --;

        if(roundOrderTypes[turn] != "Player")
        {
            if (enemiesTurnsTaken.Count() <= 0)
            {
                playersTurnsTaken.Add(0);
                playersTurnsTaken.Add(0);
                playersTurnsTaken.Add(0);
                playersTurnsTaken.Add(0);
            }
            for (int x = 0; x < players.Count; x++)
            {
                if (target.Equals(players[x]))
                {
                    playersActive[x] = false;
                    attackPool.Remove(players[x]);
                    loadMission.RemoveImage(players[x], "Player");
                    turn -= playersTurnsTaken[x];
                }
            }
        }
        else if (roundOrderTypes[turn] != "Enemy")
        {
            if(enemiesTurnsTaken.Count() <= 0)
            {
                enemiesTurnsTaken.Add(0);
                enemiesTurnsTaken.Add(0);
                enemiesTurnsTaken.Add(0);
                enemiesTurnsTaken.Add(0);
            }
            for (int x = 0; x < enemies.Count; x++)
            {
                if (target.Equals(enemies[x]))
                {
                    enemiesActive[x] = false;
                    enemyAttackPool.Remove(enemies[x]);
                    loadMission.RemoveImage(enemies[x], "Enemy");
                    turn -= enemiesTurnsTaken[x];
                }
            }
        }

        //Set selected enemy to lowest hp enemy by default if the previously selected enemy was killed.
        if (target.Equals(selectedEnemy))
        {
            if (enemyAttackPool.Count > 0)
            {
                selectedEnemy = enemyAttackPool[0];
                for (int x = 0; x < enemyAttackPool.Count; x++)
                {
                    if (enemyAttackPool[x].hitPoints < selectedEnemy.hitPoints)
                    {
                        selectedEnemy = enemyAttackPool[x];
                    }
                }
            }
        }

        LoadOrder(); //Re Create the round order
    }

    //Add turn to keep track of how many times a beast has went in the round
    //Used to keep track of how much the turn variable has to be edited by when a beast gets knocked out
    void AddTurn()
    {
        if (playersTurnsTaken.Count < 4)
        {
            playersTurnsTaken.Clear();
            playersTurnsTaken.Add(0);
            playersTurnsTaken.Add(0);
            playersTurnsTaken.Add(0);
            playersTurnsTaken.Add(0);
        }
        if (enemiesTurnsTaken.Count < 4)
        {
            enemiesTurnsTaken.Clear();
            enemiesTurnsTaken.Add(0);
            enemiesTurnsTaken.Add(0);
            enemiesTurnsTaken.Add(0);
            enemiesTurnsTaken.Add(0);
        }

        if (roundOrderTypes[turn] == "Player")
        {
            for(int x= 0; x < players.Count; x++)
            {
                if (currentTurn == players[x]) playersTurnsTaken[x] += 1;
            }
        }
        else
        {
            for (int x = 0; x < enemies.Count; x++)
            {
                if (currentTurn == enemies[x]) enemiesTurnsTaken[x] += 1;
            }
        }
    }

    string GetImage(Beast beast)
    {
        return beast.static_img;
    }

    void ClearTurns()
    {   
            playersTurnsTaken.Clear();
            playersTurnsTaken.Add(0);
            playersTurnsTaken.Add(0);
            playersTurnsTaken.Add(0);
            playersTurnsTaken.Add(0);
            enemiesTurnsTaken.Clear();
            enemiesTurnsTaken.Add(0);
            enemiesTurnsTaken.Add(0);
            enemiesTurnsTaken.Add(0);
            enemiesTurnsTaken.Add(0);  
    }  
    Beast getWeakestFriend()
    {
        Beast b = currentTurn;
        if(roundOrderTypes[turn] == "Player")
        {
            for (int x =0;x< 4;x++)
            {
                if(players[x] != null && b != null && playersActive[x] && ((double)players[x].hitPoints/ (double)players[x].maxHP) < ((double)b.hitPoints/ (double)b.maxHP))
                {
                    print(roundOrderTypes[turn]);
                    print(players[x].name);
                    b = players[x];
                }
            }
        }
        else 
        {
            for (int x =0; x< 4; x++)
            {
                if (enemies[x] != null && b != null && enemiesActive[x] && ((double)enemies[x].hitPoints/ (double)enemies[x].maxHP) < ((double)b.hitPoints/ (double)b.maxHP))
                {
                    b = enemies[x];
                }
            }
        }
        return b;
    }
}
