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

    public HealthManager healthManager;
    public Attack attack;
    public LoadMission loadMission;

    public Text txtTurn;

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
        if(selectedEnemy == null && enemyAttackPool.Count > 0)
        {
            selectedEnemy = enemyAttackPool[0];
        }

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
                print(moves[x]);
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
                print(moves[x+4]);

            }
        }

        totalMoves = moves.Sum();
        print(totalMoves);

        List<int> Speed = new List<int>();
        List<float> playZap = new List<float>();
        List<float> enemZap = new List<float>();

        for (int x = 0; x < 8; x++)
        {
            if (x < 4 && players[x] != null)
            {
                if (x < 4 && players[x].statusTurns[(int)Beast.types.Air] > 0)
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
                if (x >= 4 && enemies[x % 4].statusTurns[(int)Beast.types.Air] > 0)
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
            }
            if (x < 4 && players[x] != null)
            {
                Speed.Add((int)Mathf.Floor((float)players[x].speed * playZap[x]));
            }
            else if (x > 3 && enemies[x % 4] != null)
            {
                Speed.Add((int)Mathf.Floor((float)enemies[x % 4].speed * enemZap[x % 4]));
            }
            else
            {
                Speed.Add(0);
            }
        }

        //int[] Speed = { speed1, speed2, speed3, speed4, speed5, speed6, speed7, speed8 };
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
                    if (x<4 && beastActive[x] && Speed[y] == players[x].speed*playZap[x] && moves[x] > 0 && !InWave("Player " + players[x].name, wave))
                    {
                        roundOrder.Add(players[x]);
                        roundOrderTypes.Add("Player");
                        wave.Add("Player " + players[x].name);
                        moves[x]--;
                        i++;
                        break;
                    }
                    else if(x>=4 && beastActive[x] && Speed[y] == enemies[x%4].speed*enemZap[x%4] && moves[x] > 0 && !InWave("Enemy " + enemies[x % 4].name, wave))
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

        currentTurn = roundOrder[turn];
        //print(currentTurn);
        txtTurn.text = roundOrderTypes[0] + " " + currentTurn.name + "'s turn \n HP left: "+currentTurn.hitPoints;
        if (roundOrderTypes[turn] == "Enemy" && attackPool.Count > 0)
        {
            if (!eRunning && !pRunning)
            {
                print("before enem");
                StartCoroutine(EnemyAttack());
                print("after enem");
            }
        }
        else if (roundOrderTypes[turn] == "Player" && enemyAttackPool.Count > 0)
        {
            if (!eRunning && !pRunning)
            {
                print("before play");
                StartCoroutine(PlayerAttack());
                print("after play");
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
            if (currentTurn.statusTurns[x] > 0)
            {
                currentTurn.statusTurns[x]--;
                justNow = true;
            }
            if(currentTurn.statusTurns[x] > 0 && x == (int)Beast.types.Fire)
            {
                healthManager.UpdateHealth(currentTurn, 5);
            }
            if(currentTurn.statusTurns[x] == 0 && x == (int)Beast.types.Air && justNow)
            {
                //LoadOrder();
            }
            if(currentTurn.statusTurns[x] > 0 && x == (int)Beast.types.Earth)
            {
                healthManager.UpdateHealth(currentTurn,(int) Mathf.Ceil((float)currentTurn.hitPoints*.05f));
            }
        }
    }

    public void Attack(Beast target)
    {
        print(currentTurn);
        bool inFront = this.inFront();

        if (inFront)
        {
            if (currentTurn.Move_A.healing)
            {
                target = this.getWeakestFriend();
            }
        }
        else if (!inFront)
        {
            if (currentTurn.Move_B.healing)
            {
                target = this.getWeakestFriend();
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
                target = currentTurn.cursed;
            }
        }

        //Check to see if the round is still going and then run an attack
        if (turn >= totalMoves - 1)
        {
            print("bm 435");
            attack.InitiateAttack(currentTurn, target, inFront);
            Debug.Log("Round Ended");
            ClearTurns();
            currentTurn = roundOrder[0];
            txtTurn.text = roundOrderTypes[0] + " " + currentTurn + "'s turn";
            turn = 0;
            if (healthManager.playersLeft > 0 && healthManager.enemiesLeft > 0 && roundOrderTypes[turn] == "Enemy")
            {
                if (!eRunning && !pRunning)
                {
                    StartCoroutine(EnemyAttack());
                }
            }
            else if (healthManager.enemiesLeft > 0 && healthManager.playersLeft > 0 && roundOrderTypes[turn] == "Player")
            {
                if (!eRunning && !pRunning)
                {
                    StartCoroutine(PlayerAttack());
                }
            }
        }
        else
        {
            print("bm 459");
            attack.InitiateAttack(currentTurn, target, inFront);
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

    IEnumerator EnemyAttack()
    {
        eRunning = true;
        yield return new WaitForSeconds(1.5f);
        eRunning = false;
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

    IEnumerator PlayerAttack()
    {
        pRunning = true;
        yield return new WaitForSeconds(1.5f);
        pRunning = false;
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

    bool inFront(Beast b)
    {

        for (int x = 0; x < slots.Count; x++)
        {
            if (x < 3 && b.Equals(slots[x]) || b.Equals(enemySlots[x]))
            {
                return true;
            }
        }
        return false;
    }

    //Remove the desired beast by setting its active variable to false and removing image
    public void RemoveBeast(Beast target)
    {
        if(target == selectedEnemy)
        {
            selectedEnemy = null;
        }

        totalBeasts -= 1;

        if(roundOrderTypes[turn] != "Player")
        {
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
                    print(enemiesTurnsTaken.Count());
                    turn -= enemiesTurnsTaken[x];
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
            for(int x =0;x< 4;x++)
            {
                if(players[x] != null && b != null && playersActive[x] && ((double)players[x].hitPoints/ (double)players[x].maxHP) < ((double)b.hitPoints/ (double)b.maxHP))
                {
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
