//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//manages the main battle mechanics
public class BattleManager : MonoBehaviour
{
    public bool eRunning = false;
    public bool pRunning = false;

    //These are the slot objects that hold the images for the Beast
    public List<GameObject> playerPadSlots = new List<GameObject>(Values.SLOTMAX);
    public List<GameObject> enemyPadSlots = new List<GameObject>(Values.SLOTMAX);

    //These are Scripts given in unity 
    //NOTE: this script will crash if these scripts are not added before runtime
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
    int totalBeasts = Values.SQUADMAX * 2;

    public Beast currentTurn;
    public Beast selectedEnemy;
    public Beast selectedFriend;

    public List<Image> orderBar = new List<Image>();
    public List<Image> orderBarOutlines = new List<Image>();

    public List<Beast> slots = new List<Beast>();
    public List<Beast> enemySlots;

    public List<bool> playersActive = new List<bool>();
    public List<bool> enemiesActive= new List<bool>();

    List<int> playersTurnsTaken = new List<int>();
    List<int> enemiesTurnsTaken = new List<int>();

    //Get lists from LoadMission and add the players to the attack pool
    public void SendLists(List<Beast> thisSquad, List<Beast> enemySquad, List<HealthBar> activePlayersHealth, List<HealthBar> activeEnemiesHealth, List<DamageOutput> activePlayerDamage, List<DamageOutput> activeEnemyDamage)
    {
        selectedEnemy = enemySquad[0];
        for (int x = 0; x < enemySquad.Count; x++)
        {
            if (enemySquad[x].hitPoints < selectedEnemy.hitPoints)
            {
                selectedEnemy = enemySquad[x];
            }
        }

        enemies = enemySquad;
        while (enemies.Count < Values.SQUADMAX) { 
            enemies.Add(null);
        }
        while (enemiesActive.Count < Values.SQUADMAX)
        {
            enemiesActive.Add(true);
        }

        for (int x =0; x < Values.SQUADMAX; x++)
        {
            if (enemies[x] != null)
            {
                enemyAttackPool.Add(enemies[x]);
            }
            else
            {
                enemiesActive[x] = false;
                totalBeasts--;
            }
        }

        players = thisSquad;
        while (players.Count < Values.SQUADMAX)
        {
            players.Add(null);
        }
        while (playersActive.Count < Values.SQUADMAX)
        {
            playersActive.Add(true);
        }

        for (int x = 0; x < Values.SQUADMAX; x++)
        {
            if (players[x] != null)
            {

                attackPool.Add(players[x]);
            }
            else
            {
                playersActive[x] = false;
                totalBeasts--;
            }
        }
        

        for(int x = 0; x < Values.SQUADMAX; x++)
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
        for (int x = 0; x < Values.SLOTMAX; x++)
        {
            slots.Add(s[x]);
            enemySlots.Add(e[x]);
        }
    }
    //old method to be removed
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
    //This seems to be a big source of issues as it creats what has been dubbed "the Ultimate Crash"
    //The cause is unknown and this method is entegeral so either the issue must be found or the method must be re created from scratch
    void LoadOrder()
    {
        int[] moves = new int[Values.SQUADMAX * 2];
        //this loop takes the default amount of moves for the beast as well as the move the beast will be using
        for(int x = 0; x < Values.SQUADMAX; x++)
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
                moves[x+ Values.SQUADMAX] = enemies[x].number_MOVES;
                enemies[x].setAttacks();
                if (inFront(enemies[x]))
                {
                    moves[x + Values.SQUADMAX] += enemies[x].Move_A.number_of_moves;
                }
                else
                {
                    moves[x + Values.SQUADMAX] += enemies[x].Move_B.number_of_moves;
                }

            }
        }

        totalMoves = moves.Sum();

        List<int> Speed = new List<int>();
        //this loop sorts by speed both freindly and enemy beasts with ties being broken by the player
        for (int x = 0; x < Values.SQUADMAX*2; x++)
        {
            
            if (x < Values.SQUADMAX && players[x] != null)
            {
                Speed.Add((int)Mathf.Floor((float)players[x].speed));
            }
            else if (x >= Values.SQUADMAX && enemies[x % Values.SQUADMAX] != null)

            {
                Speed.Add((int)Mathf.Floor((float)enemies[x % Values.SQUADMAX].speed ));
            }
            else
            {
                Speed.Add(0);
            }
        }

       // bool[] beastActive = { playersActive[0], playersActive[1], playersActive[2], playersActive[3], enemiesActive[0], enemiesActive[1], enemiesActive[2], enemiesActive[3] };
        List<bool> beastActive = new List<bool>();
        for(int x = 0; x < Values.SQUADMAX*2; x++)
        {
            if (x < Values.SQUADMAX)
            {
                beastActive.Add(playersActive[x]);
            }
            else
            {
                beastActive.Add(enemiesActive[x % Values.SQUADMAX]);
            }
        }
        int i = 0;

        List<string> wave = new List<string>();

        //Create an array with each speed and sort it highest to lowest
        Speed.Sort();
        Speed.Reverse();

        //Clear the previous round order
        roundOrder.Clear();
        roundOrderTypes.Clear();
        //Loop through the speed array and find the corresponding beast and add them to the round order
        //may also be the root of 'ultiamte crash'
        while (i < totalMoves)
        {
            for(int y = 0; y < Values.SQUADMAX*2; y++)
            {
                for(int x = 0; x < Values.SQUADMAX * 2; x++)
                {
                    if (x< Values.SQUADMAX && beastActive[x] && Speed[y] == players[x].speed && moves[x] > 0 && !InWave("Player " + players[x].name, wave))
                    {
                        roundOrder.Add(players[x]);
                        roundOrderTypes.Add("Player");
                        wave.Add("Player " + players[x].name);
                        moves[x]--;
                        i++;
                        break;
                    }
                    else if(x>= Values.SQUADMAX && beastActive[x] && Speed[y] == enemies[x% Values.SQUADMAX].speed && moves[x] > 0 && !InWave("Enemy " + enemies[x % Values.SQUADMAX].name, wave))
                    {
                        roundOrder.Add(enemies[x % Values.SQUADMAX]);
                        roundOrderTypes.Add("Enemy");
                        wave.Add("Enemy " + enemies[x % Values.SQUADMAX].name);
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
        while((1+turn) > roundOrderTypes.Count)
        {
            turn--;
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
    //keeps the order bar showing the correct order of attack
    void UpdateOrderBar()
    {
        currentTurn = roundOrder[turn];

        foreach(Image outline in orderBarOutlines)
        {
            outline.gameObject.SetActive(true);
        }

        for (int x = 0; x < orderBar.Count; x++)
        {
            try
            {
                orderBar[x].sprite = Resources.Load<Sprite>("Static_Images/"+GetImage(roundOrder[x + turn]));

                if (roundOrderTypes[x + turn].Equals("Player"))
                {
                    orderBarOutlines[x].GetComponent<Outline>().effectColor = Color.blue;
                }
                else if (roundOrderTypes[x + turn].Equals("Enemy"))
                {
                    orderBarOutlines[x].GetComponent<Outline>().effectColor = Color.red;
                }
            }
            catch
            {
                orderBar[x].sprite = Resources.Load<Sprite>("Static_Images/EmptyRectangle");
                orderBarOutlines[x].gameObject.SetActive(false);
            }
        }
    }
    //starts the current players, or enemies attack. 
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
        decroStatAndBuff();
    }

    //This method is responsible for decrementing counters for status effects (excluding Doom) and Buffs & debuffs
    void decroStatAndBuff()
    {
        //this is checking if a status effect is still active and decrimenting its counter if it is
        for (int x = 0; x < currentTurn.statusTurns.Length; x++)
        {
            if (currentTurn.statusTurns[x] > 0 && (int)Move.types.Corrupt != x)
            {
                currentTurn.statusTurns[x]--;
            }
            if (currentTurn.statusTurns[x] > 0 && x == (int)Move.types.Burn)
            {
                healthManager.UpdateHealth(currentTurn, 5);
            }
            if (currentTurn.statusTurns[x] > 0 && x == (int)Move.types.Poison)
            {
                healthManager.UpdateHealth(currentTurn, (int)Mathf.Ceil((float)currentTurn.hitPoints * .05f));
            }
        }
        //this checks for buffs and decriments them all by 1. if the buff counter hits 0 it is removed
        for (int x = 0; x < currentTurn.buffs.Count; x++)
        {

            if (currentTurn.buffs[x].turnsLeft <= 0)
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

    //this method finds which targets are availible to a beast using a slash attack
    //it takes into consideration the position of the attacker and the availible targets 
    List<Beast> findRowTargets()
    {
        List<Beast> targets = new List<Beast>();
        int slot = getCurrentBeastSlot();
        if (roundOrderTypes[turn] == "Player")
        {
            //this for loop will aquier upto three targets from a single row
            for(int x = 0; x < enemySlots.Count; x++)
            {
                //this if statment tries to get all three beasts in the front row
                if (x < (Values.SLOTMAX / 2) && enemySlots[x] != null && enemySlots[x].hitPoints > 0)
                {
                    if(slot+1 == x || slot == x || slot-1 == x)
                    {
                        targets.Add(enemySlots[x]);
                    }
                }
                //this else if checks to see if any targets from the front row have been added and if so
                //breaks the loop, if not addes the beasts from the back row

                else if(x>=(Values.SLOTMAX/2) && enemySlots[x] != null && enemySlots[x].hitPoints > 0){
                    print("help");
                    //this is the dynamic if to check for beasts in the front, I had to have it work dynamically or else it would never work
                    if (x == (Values.SLOTMAX/2) && targets.Count  >= 1)
                    {
                        break;
                    }


                    if (slot% (Values.SLOTMAX/2) + 1 == x% (Values.SLOTMAX / 2) || slot% (Values.SLOTMAX / 2) == x % (Values.SLOTMAX / 2) || slot % (Values.SLOTMAX / 2) - 1 == x % (Values.SLOTMAX / 2))
                    {
                        print("slot " + x);
                        targets.Add(enemySlots[x]);
                    }
                }
            }
            //this is to cover situations that would normally have no availible target
            if (targets.Count <= 0)
            {
                for (int x = 0; x < enemySlots.Count; x++)
                {
                    if (x < (Values.SLOTMAX/2) && enemySlots[x] != null && enemySlots[x].hitPoints > 0)
                    {
                            targets.Add(enemySlots[x]);                        
                    }
                    else if (x >= (Values.SLOTMAX/2) && enemySlots[x] != null && enemySlots[x].hitPoints > 0)

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
                if (x < (Values.SLOTMAX/2) && slots[x] != null && slots[x].hitPoints >0)

                {
                    if (slot + 1 == x || slot == x || slot - 1 == x)
                    {
                        targets.Add(slots[x]);
                    }
                }
                else if (x >= (Values.SLOTMAX/2) && slots[x] != null && slots[x].hitPoints > 0)
                {
                    if (targets.Count - (x - (Values.SLOTMAX/2)) >= 1)

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
                    if (x < (Values.SLOTMAX/2) && slots[x] != null && slots[x].hitPoints > 0)
                    {
                        targets.Add(slots[x]);
                    }
                    else if (x >= (Values.SLOTMAX/2) && slots[x] != null && slots[x].hitPoints > 0)

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
    //this method goes through both slots untill it finds a mathching beast to the current turn and returning it's slot, and if it finds nothing it returns -1
    int getCurrentBeastSlot()
    {
        int slot = -1;
        //loops through the slots for player and enemy beasts untill it finds thematching one
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
    //this method goes through both slots untill it finds a mathching beast the given beast and returning it's slot, and if it finds nothing it returns -1
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
    //this method finds which targets are availible to a beast using a stab attack
    //it takes into consideration the position of the attacker and the availible targets 
    List<Beast> findColumnTargets()
    {
        List<Beast> targets = new List<Beast>();
        int slot = getCurrentBeastSlot();
        if (roundOrderTypes[turn] == "Enemy")
        {
            //this switch finds the column the attacker is in and find the most sutible target column determined by distance
            //the aligned cloumn is always prioritised 
            switch (slot % (Values.SMALLSLOT / 2))
            {
                case 0:
                    do
                    {
                        if (slots[slot % (Values.SMALLSLOT / 2)] != null && slots[slot % (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(slots[slot % (Values.SMALLSLOT / 2)]);
                        }
                        if (slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)] != null && slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 1:
                    do
                    {
                        if (slots[slot % (Values.SMALLSLOT / 2)] != null && slots[slot % (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(slots[slot % (Values.SLOTMAX/2)]);
                        }
                        if (slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)] != null && slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 2:
                    do
                    {
                        if (slots[slot % (Values.SMALLSLOT / 2)] != null && slots[slot % (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(slots[slot % (Values.SLOTMAX/2)]);
                        }
                        if (slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)] != null && slots[(slot % (Values.SMALLSLOT / 2)) +(Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(slots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)]);
                        }
                        slot--;
                    } while (targets.Count < 1);
                    break;

            }
        }
        else
        {
            switch (slot % (Values.SMALLSLOT / 2))
            {
                case 0:
                    do
                    {
                        if (enemySlots[slot % (Values.SMALLSLOT / 2)] != null && enemySlots[slot % (Values.SMALLSLOT / 2)].hitPoints >0)
                        {
                            targets.Add(enemySlots[slot % (Values.SLOTMAX/2)]);
                        }
                        if (enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)] != null && enemySlots[(slot % (Values.SMALLSLOT / 2))  +  (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(enemySlots[slot % (Values.SMALLSLOT / 2) + (Values.SMALLSLOT / 2)]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 1:
                    do
                    {
                        if (enemySlots[slot % (Values.SMALLSLOT / 2)] != null && enemySlots[slot % (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(enemySlots[slot % (Values.SMALLSLOT / 2)]);
                        }
                        if (enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)] != null && enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)]);
                        }
                        slot++;
                    } while (targets.Count < 1);
                    break;
                case 2:
                    do
                    {
                        if (enemySlots[slot % (Values.SMALLSLOT / 2)] != null && enemySlots[slot % (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(enemySlots[slot % (Values.SMALLSLOT / 2)]);
                        }
                        if (enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)] != null && enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)].hitPoints > 0)
                        {
                            targets.Add(enemySlots[(slot % (Values.SMALLSLOT / 2)) + (Values.SMALLSLOT / 2)]);
                        }
                        slot--;
                    } while (targets.Count < 1);
                    break;

            }
        }
        return targets;
    }
    //this method determins whether the attacker is in the front or back row and adjusts targets as neccisary 
    //it also initiates attacks
    public void Attack(Beast target)
    {
        bool inFront = this.inFront();
        bool guarded = this.guarded(target);

        bool cancelGuard = false;

        List<Beast> targets = new List<Beast>();

        targets.Add(target);


        //the if and else blocks here are identicle except for Move_A is switched with Move_B
        if (inFront)
        {
            //determins if the move is healing and gets a freindly target based on lowest health proportional to max health
            if (currentTurn.Move_A.healing)
            {
                targets.Clear();
                targets.Add(this.getWeakestFriend());
                cancelGuard = true;
            }
            //changes targets to the front row (if there are front row targets) or back row(if there are no front row targets)
            else if (currentTurn.Move_A.rowAttack)
            {
                targets.Clear();
                targets = findRowTargets();
                cancelGuard = true;
            }
            //changes the targets to the column directly in front of the best, adjusting to the next column over when directly in front is unavailible
            else if (currentTurn.Move_A.columnAttack)
            {
                targets.Clear();
                targets = findColumnTargets();
                cancelGuard = true;
            }
            //certain attacks hit multiple times (think fury swipe from pokemon) this checks how many times to do the same attack
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
        //this checks if there is a beast blocking the current target as well making sure there's nothing that would need to cancel the block 
        //like healing for example
        if (guarded && !cancelGuard)
        {
            int slot = getCurrentBeastSlot(targets[targets.Count-1]);
            targets.Clear();
            Beast b = new Beast();
            if (roundOrderTypes[turn] == "Player")
            {
                //this takes the slot of the targeted beast and finds the beast directly in front of it
                for (int x = (Values.SLOTMAX/2); x < enemySlots.Count; x++)
                {
                    if (x == slot && enemySlots[x % (Values.SLOTMAX/2)] != null && enemySlots[x % (Values.SLOTMAX/2)].hitPoints > 0)
                    {
                        b = enemySlots[x % (Values.SLOTMAX/2)];
                    }
                }
            }
            else
            {
                for (int x = (Values.SLOTMAX/2); x < slots.Count; x++)
                {
                    if (x == slot && slots[x % (Values.SLOTMAX/2)] != null && slots[x % (Values.SLOTMAX/2)].hitPoints > 0)
                    {
                        b = slots[x % (Values.SLOTMAX/2)];
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
            else if (!inFront && currentTurn.Move_B.multiAttack)
            {
                int ran = Random.Range(1, 5);
                for (; ran > 0; ran--)
                {
                    targets.Add(targets[0]);
                }
            }
        }
        //this method currently needs improvment
        //this method checks if the current turn beast is confused and will make them target a friendly beast
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
        //this checks if the current beast has a doom target
        //this is to ensure that whomever has been targeted is still alive as to not kill them twice
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
                
            }
            else if (healthManager.enemiesLeft > 0 && healthManager.playersLeft > 0 && roundOrderTypes[turn] == "Player")
            {
                StartCoroutine(PlayerAttack());
                
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
            //loops untill it finds the matching beast
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
    //plays the attacking animation for either front or back row attack depending on the bool
    void PlayAttackAnimation(bool inFront)
    {
        GameObject slot = getSlot();

        if (inFront) slot.GetComponent<Animator>().SetTrigger("Front");
        else slot.GetComponent<Animator>().SetTrigger("Back");
    }
    //this plays the damage animation for one or many beasts
    void PlayDamagedAnimation(List<Beast> targets)
    {
        foreach (Beast target in targets)
        {
            if (roundOrderTypes[turn] == "Player")
            {
                //loops through the slots until it finds a matching beast to the target
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
    //starts the attacking cycle for the enemy
    IEnumerator EnemyAttack()
    {
        eRunning = true;
        yield return new WaitForSeconds(2f);
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
    //gets a random target from the enemy team
    Beast GetPlayerTarget()
    {
        int rand = Random.Range(0, enemyAttackPool.Count);

        Beast b = enemyAttackPool[rand];        
        return b;
    }
    //starts the attacking cycle for the player
    IEnumerator PlayerAttack()
    {
        pRunning = true;
        yield return new WaitForSeconds(2f);
        //pRunning = false;
        if (enemyAttackPool.Count > 0)
            Attack(selectedEnemy);
    }

    //Get the row to determine whether the current turn beast is using an A move or a B move
    bool inFront()
    {
        for(int x = 0; x< slots.Count; x++)
        {
            if(x<(Values.SMALLSLOT/2) && (currentTurn.Equals(slots[x]) || currentTurn.Equals(enemySlots[x])))
            {
                return true;
            }
        }
        return false;
    }
    //determins if the target has a given beast in front of them 
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
            for (int x = (Values.SMALLSLOT/2); x < enemySlots.Count; x++)
            {
                if(x==slot && enemySlots[x%(Values.SMALLSLOT / 2)] != null && enemySlots[x%(Values.SMALLSLOT / 2)].hitPoints > 0)
                {
                    return true;
                }
            }
        }
        else
        {
            for (int x = (Values.SMALLSLOT / 2); x < slots.Count; x++)
            {
                if (x == slot && slots[x % (Values.SMALLSLOT / 2)] != null && slots[x%(Values.SMALLSLOT / 2)].hitPoints > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    //Get the row to determine whether a given beast is in the front or back row
    bool inFront(Beast b)
    {
        for (int x = 0; x < slots.Count; x++)
        {
            if (x < (Values.SMALLSLOT/2) && (b.Equals(slots[x]) || b.Equals(enemySlots[x])))
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
                while (enemiesTurnsTaken.Count < Values.SQUADMAX)
                {
                    enemiesTurnsTaken.Add(0);
                }
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
        if (playersTurnsTaken.Count < Values.SQUADMAX)
        {
            playersTurnsTaken.Clear();
            while (playersTurnsTaken.Count < Values.SQUADMAX)
            {
                playersTurnsTaken.Add(0);
            }
        }
        if (enemiesTurnsTaken.Count < Values.SQUADMAX)
        {
            enemiesTurnsTaken.Clear();
            while (enemiesTurnsTaken.Count < Values.SQUADMAX)
            {
                enemiesTurnsTaken.Add(0);
            }
            
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
    //gets the name of the static image for any given beast
    string GetImage(Beast beast)
    {
        return beast.static_img;
    }
    //sets everyones turns to 0
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
    //looks for which friendly unit has the lowest amount of health proportional to their max health
    Beast getWeakestFriend()
    {
        Beast b = currentTurn;
        if(roundOrderTypes[turn] == "Player")
        {
            for (int x =0;x< Values.SQUADMAX;x++)
            {
                //checks each friendly beasts proportional health remaining and campares it to who ever had the the prieviously lowest proportional health
                //defaults on the healer
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
            for (int x =0; x< Values.SQUADMAX; x++)
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
