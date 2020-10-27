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

    List<Beast> players = new List<Beast>();
    List<Beast> enemies = new List<Beast>();
    public List<Beast> roundOrder = new List<Beast>();
    public List<string> roundOrderTypes = new List<string>();
    List<Beast> attackPool = new List<Beast>();
    List<Beast> enemyAttackPool = new List<Beast>();

    public int turn = 0;
    int totalMoves;
    int totalBeasts = 8;

    public Beast currentTurn;
    public Beast selectedEnemy;

    /*Beast slot1;
    Beast slot2;
    Beast slot3;
    Beast slot4;
    Beast slot5;
    Beast slot6;*/
    List<Beast> slots = new List<Beast>();
    /*public Beast enemySlot1;
    public Beast enemySlot2;
    public Beast enemySlot3;
    public Beast enemySlot4;
    public Beast enemySlot5;
    public Beast enemySlot6;*/
    public List<Beast> enemySlots;

    /*public bool player1Active = true;
    public bool player2Active = true;
    public bool player3Active = true;
    public bool player4Active = true;*/
    public bool[] playersActive = { true, true, true, true };
    /*public bool enemy1Active = true;
    public bool enemy2Active = true;
    public bool enemy3Active = true;
    public bool enemy4Active = true;*/
    public bool[] enemiesActive= {true, true, true, true};

    /*int player1TurnsTaken;
    int player2TurnsTaken;
    int player3TurnsTaken;
    int player4TurnsTaken;*/
    List<int> playersTurnsTaken = new List<int>();
    /*int enemy1TurnsTaken;
    int enemy2TurnsTaken;
    int enemy3TurnsTaken;
    int enemy4TurnsTaken;*/
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
        //players.Add(null);
        //players.Add(null);
        //players.Add(null);
        //players.Add(null);

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
        /*slot1 = s1;
        slot2 = s2;
        slot3 = s3;
        slot4 = s4;
        slot5 = s5;
        slot6 = s6;*/
        slots.Clear();
        enemySlots.Clear();
        slots.Add(s1);
        slots.Add(s2);
        slots.Add(s3);
        slots.Add(s4);
        slots.Add(s5);
        slots.Add(s6);
        /*enemySlot1 = e1;
        enemySlot2 = e2;
        enemySlot3 = e3;
        enemySlot4 = e4;
        enemySlot5 = e5;
        enemySlot6 = e6;*/
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

        /*int[] Moves = new int[8];
        //Get each players moves per round

        for (int x = 0; x < 8; x++)
        {
            if (x < 4 && players[x] != null)
            {
                Moves[x] = players[x].number_MOVES;
            }
            else if (x > 3 && enemies[x % 4] != null)
            {
                Moves[x] = enemies[x % 4].number_MOVES;
            }
            else
            {
                Moves[x] = 0;
            }
        }*/


        if (playersActive[0]) moves[0] = players[0].number_MOVES;
        if (playersActive[1] && players[1] != null) moves[1] = players[1].number_MOVES;
        if (playersActive[2] && players[2] != null) moves[2] = players[2].number_MOVES;
        if (playersActive[3] && players[3] != null) moves[3] = players[3].number_MOVES;
        if (enemiesActive[0]) moves[4] = enemies[0].number_MOVES;
        if (enemiesActive[1]) moves[5] = enemies[1].number_MOVES;
        if (enemiesActive[2]) moves[6] = enemies[2].number_MOVES;
        if (enemiesActive[3]) moves[7] = enemies[3].number_MOVES;


        totalMoves = moves.Sum();

        List<int> Speed = new List<int>();

        for(int x = 0; x < 8; x++)
        {
            if(x<4&&players[x] != null)
            {
                Speed.Add(players[x].speed);
            }
            else if(x>3&&enemies[x%4] != null)
            {
                Speed.Add(enemies[x%4].speed);
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
                    if (x<4 && beastActive[x] && Speed[y] == players[x].speed && moves[x] > 0 && !InWave("Player " + players[x].name, wave))
                    {
                        roundOrder.Add(players[x]);
                        roundOrderTypes.Add("Player");
                        wave.Add("Player " + players[x].name);
                        moves[x]--;
                        i++;
                        break;
                    }
                    else if(x>=4 && beastActive[x] && Speed[y] == enemies[x%4].speed && moves[x] > 0 && !InWave("Enemy " + enemies[x % 4].name, wave))
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
        }
        else if (roundOrderTypes[turn] == "Player")
        {
            if (!eRunning && !pRunning)
            {
                StartCoroutine(PlayerAttack());
            }
        }
    }

    public void Attack(Beast target)
    {
        bool inFront = this.inFront();

        /*foreach(Beast b in slots)
        {
            if(b!=null && slots.Equals(currentTurn) && roundOrderTypes[turn] == "Player")
            {
                inFront = true;
            }
        }
        foreach (Beast b in enemySlots)
        {
            if (b != null && slots.Equals(currentTurn) && roundOrderTypes[turn] == "Enemy")
            {
                inFront = true;
            }
        }*/

        /*if (slot1 != null && slot1.Equals(currentTurn) && roundOrderTypes[turn] == "Player")
        {
            inFront = true;
        }
        if (slot2 != null && slot2.Equals(currentTurn) && roundOrderTypes[turn] == "Player")
        {
            inFront = true;
        }
        if (slot3 != null && slot3.Equals(currentTurn) && roundOrderTypes[turn] == "Player")
        {
            inFront = true;
        }
        if (enemySlot1 != null && enemySlot1.Equals(currentTurn) && roundOrderTypes[turn] == "Enemy")
        {
            inFront = true;
        }
        if (enemySlot2 != null && enemySlot2.Equals(currentTurn) && roundOrderTypes[turn] == "Enemy")
        {
            inFront = true;
        }
        if (enemySlot3 != null && enemySlot3.Equals(currentTurn) && roundOrderTypes[turn] == "Enemy")
        {
            inFront = true;
        }*/
        //Check to see if the round is still going and then run an attack
        if (turn >= totalMoves - 1)
        {
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
            attack.InitiateAttack(currentTurn, target, inFront);
            AddTurn();
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
/*
        print(attackPool.Count);
        if (attackPool.Count <= 1)
            while (attackPool[rand] == null)
        {
            
                attackPool.RemoveAt(rand);
            rand = Random.Range(0, attackPool.Count - 1);
        }*/
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

    //Enemy targets a random player from a pool of active player beasts
    /*
    Beast GetPlayerTarget()
    {
        int rand = Random.Range(0, enemyAttackPool.Count);
        Beast b = enemyAttackPool[rand];
        return b;
    }
    */

    //Get the row to determine whether the attacker is using an A move or a B move
    bool inFront()
    {

        for(int x = 0; x< slots.Count; x++)
        {
            if(x<=3 && currentTurn == slots[x] || currentTurn == enemySlots[x])
            {
                return true;
            }
        }
        return false;

        /*if (currentTurn == slot1 || currentTurn == slot3 || currentTurn == slot5
        || currentTurn == enemySlot2 || currentTurn == enemySlot4 || currentTurn == enemySlot6)
        {
            return "B";
        }
        else
        {
            return "A";
        }*/
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

        /*for (int x = 0; x < enemies.Count; x++)
        {
            if (target.Equals(players[x]) && roundOrderTypes[turn] != "Player")
            {
                playersActive[x] = false;
                attackPool.Remove(players[x]);
                loadMission.RemoveImage(players[x], "Player");
                turn -= playersTurnsTaken[x];
            }
            else if (target.Equals(enemies[x]) && roundOrderTypes[turn] != "Enemies")
            {
                enemiesActive[x] = false;
                enemyAttackPool.Remove(enemies[x]);
                loadMission.RemoveImage(enemies[x], "Enemy");
                turn -= enemiesTurnsTaken[x];
            }
        }*/
        /*if (target == players[0])
        {
            player1Active = false;
            attackPool.Remove(players[0]);
            loadMission.RemoveImage(players[0], "Player");
            turn -= player1TurnsTaken;
        }
        else if (target == players[1])
        {
            player2Active = false;
            attackPool.Remove(players[1]);
            loadMission.RemoveImage(players[1], "Player");
            turn -= player2TurnsTaken;
        }
        else if (target == players[2])
        {
            player3Active = false;
            attackPool.Remove(players[2]);
            loadMission.RemoveImage(players[2], "Player");
            turn -= player3TurnsTaken;
        }
        else if (target == players[3])
        {
            player4Active = false;
            attackPool.Remove(players[3]);
            loadMission.RemoveImage(players[3], "Player");
            turn -= player4TurnsTaken;
        }
        else if (target == enemies[0])
        {
            enemy1Active = false;
            enemyAttackPool.Remove(enemies[0]);
            loadMission.RemoveImage(enemies[0], "Enemy");
            turn -= enemy1TurnsTaken;
        }
        else if (target == enemies[1])
        {
            enemy2Active = false;
            enemyAttackPool.Remove(enemies[1]);
            loadMission.RemoveImage(enemies[1], "Enemy");
            turn -= enemy2TurnsTaken;
        }
        else if (target == enemies[2])
        {
            enemy3Active = false;
            enemyAttackPool.Remove(enemies[2]);
            loadMission.RemoveImage(enemies[2], "Enemy");
            turn -= enemy3TurnsTaken;
        }
        else if (target == enemies[3])
        {
            enemy4Active = false;
            enemyAttackPool.Remove(enemies[3]);
            loadMission.RemoveImage(enemies[3], "Enemy");
            turn -= enemy4TurnsTaken;
        }*/

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
            /*if (currentTurn == players[0]) player1TurnsTaken += 1;
            else if (currentTurn == players[1]) player2TurnsTaken += 1;
            else if (currentTurn == players[2]) player3TurnsTaken += 1;
            else if (currentTurn == players[3]) player4TurnsTaken += 1;*/
        }
        else
        {
            for (int x = 0; x < enemies.Count; x++)
            {
                if (currentTurn == enemies[x]) enemiesTurnsTaken[x] += 1;
            }
            /*if (currentTurn == enemies[0]) enemy1TurnsTaken += 1;
            else if (currentTurn == enemies[1]) enemy2TurnsTaken += 1;
            else if (currentTurn == enemies[2]) enemy3TurnsTaken += 1;
            else if (currentTurn == enemies[3]) enemy4TurnsTaken += 1;*/
        }
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


   
}
