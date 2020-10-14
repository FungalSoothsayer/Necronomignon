//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    public HealthManager healthManager;
    public Attack attack;
    public LoadMission loadMission;

    public Text txtTurn;

    List<Beast> players = new List<Beast>();
    List<Beast> enemies = new List<Beast>();
    public List<Beast> roundOrder = new List<Beast>();
    public List<string> roundOrderTypes = new List<string>();
    List<Beast> attackPool = new List<Beast>();

    List<HealthBar> playerHealthBars = new List<HealthBar>();
    List<HealthBar> enemyHealthBars = new List<HealthBar>();


    public int turn = 0;
    int totalMoves;
    int totalBeasts = 8;

    public Beast currentTurn;

    Beast slot1;
    Beast slot2;
    Beast slot3;
    Beast slot4;
    Beast slot5;
    Beast slot6;
    public Beast enemySlot1;
    public Beast enemySlot2;
    public Beast enemySlot3;
    public Beast enemySlot4;
    public Beast enemySlot5;
    public Beast enemySlot6;

    public bool player1Active = true;
    public bool player2Active = true;
    public bool player3Active = true;
    public bool player4Active = true;
    public bool enemy1Active = true;
    public bool enemy2Active = true;
    public bool enemy3Active = true;
    public bool enemy4Active = true;

    int player1TurnsTaken;
    int player2TurnsTaken;
    int player3TurnsTaken;
    int player4TurnsTaken;
    int enemy1TurnsTaken;
    int enemy2TurnsTaken;
    int enemy3TurnsTaken;
    int enemy4TurnsTaken;

    //Get lists from LoadMission and add the players to the attack pool
    public void SendLists(List<Beast> thisSquad, List<Beast> enemySquad, List<HealthBar> activePlayersHealth, List<HealthBar> activeEnemiesHealth)
    {
        playerHealthBars = activePlayersHealth;
        enemyHealthBars = activeEnemiesHealth;
        
        players = thisSquad;
        players.Add(null);
        players.Add(null);
        players.Add(null);
        players.Add(null);
        players.Add(null);
        players.Add(null);
        players.Add(null);
        enemies = enemySquad;
        attackPool.Add(players[0]);
        if (players[1] != null)
        {
            
            attackPool.Add(players[1]);
        }
        else
        {
            player2Active = false;
            totalBeasts--;
        }
        if (players[2] != null)
        {
            
            attackPool.Add(players[2]);
        }
        else
        {
            player3Active = false;
            totalBeasts--;
        }
        if (players[3] != null)
        {
            
            attackPool.Add(players[3]);
        }
        else
        {
            player4Active = false;
            totalBeasts--;
        }

        healthManager.GetHealth(players, enemies, playerHealthBars, enemyHealthBars);
        LoadOrder();
    }

    //Get the slot info for each beast from LoadMission
    public void GetSlots(Beast s1, Beast s2, Beast s3, Beast s4, Beast s5, Beast s6, Beast e1, Beast e2, Beast e3, Beast e4, Beast e5, Beast e6)
    {
        slot1 = s1;
        slot2 = s2;
        slot3 = s3;
        slot4 = s4;
        slot5 = s5;
        slot6 = s6;
        enemySlot1 = e1;
        enemySlot2 = e2;
        enemySlot3 = e3;
        enemySlot4 = e4;
        enemySlot5 = e5;
        enemySlot6 = e6;
    }

    //Create attack order
    void LoadOrder()
    {
        int moves1 = 0;
        int moves2 = 0;
        int moves3 = 0;
        int moves4 = 0;
        int moves5 = 0;
        int moves6 = 0;
        int moves7 = 0;
        int moves8 = 0;

        //Get each players moves per round
        if (player1Active) moves1 = players[0].number_MOVES;
        if (player2Active && players[1] != null) moves2 = players[1].number_MOVES;
        if (player3Active && players[2] != null) moves3 = players[2].number_MOVES;
        if (player4Active && players[3] != null) moves4 = players[3].number_MOVES;
        if (enemy1Active) moves5 = enemies[0].number_MOVES;
        if (enemy2Active) moves6 = enemies[1].number_MOVES;
        if (enemy3Active) moves7 = enemies[2].number_MOVES;
        if (enemy4Active) moves8 = enemies[3].number_MOVES;

        totalMoves = moves1 + moves2 + moves3 + moves4 + moves5 + moves6 + moves7 + moves8;

        //Get each player's speed
        int speed1 = players[0].speed;
        int speed2 = 0;
        int speed3 = 0;
        int speed4 = 0;
        int speed5 = enemies[0].speed;
        int speed6 = enemies[1].speed;
        int speed7 = enemies[2].speed;
        int speed8 = enemies[3].speed;

        if(players[1] != null)
        {
            
            speed2 = players[1].speed;
        }
        if (players[2] != null)
        {
            
            speed3 = players[2].speed;
        }
        if (players[3] != null)
        {
            
            speed4 = players[3].speed;
        }

        int i = 0;

        List<string> wave = new List<string>();

        //Create an array with each speed and sort it highest to lowest
        int[] speeds = { speed1, speed2, speed3, speed4, speed5, speed6, speed7, speed8 };
        System.Array.Sort(speeds);
        System.Array.Reverse(speeds);

        //Clear the previous round order
        roundOrder.Clear();
        roundOrderTypes.Clear();

        //Loop through the speed array and find the corresponding beast and add them to the round order
        while (i < totalMoves)
        {
            for (int x = 0; x < 8; x++)
            {
                if (player1Active && speeds[x] == speed1 && moves1 > 0 && !InWave("Player " + players[0].name, wave))
                {
                    print(players[0].name);
                    roundOrder.Add(players[0]);
                    roundOrderTypes.Add("Player");
                    wave.Add("Player " + players[0].name);
                    moves1--;
                    i++;
                }
                else if (player2Active && speeds[x] == speed2 && moves2 > 0 && !InWave("Player " + players[1].name, wave))
                {
                    print(players[1].name);
                    roundOrder.Add(players[1]);
                    roundOrderTypes.Add("Player");
                    wave.Add("Player " + players[1].name);
                    moves2--;
                    i++;
                }
                else if (player3Active && speeds[x] == speed3 && moves3 > 0 && !InWave("Player " + players[2].name, wave))
                {
                    print(players[2].name);
                    roundOrder.Add(players[2]);
                    roundOrderTypes.Add("Player");
                    wave.Add("Player " + players[2].name);
                    moves3--;
                    i++;
                }
                else if (player4Active && speeds[x] == speed4 && moves4 > 0 && !InWave("Player " + players[3].name, wave))
                {
                    print(players[3].name);
                    roundOrder.Add(players[3]);
                    roundOrderTypes.Add("Player");
                    wave.Add("Player " + players[3].name);
                    moves4--;
                    i++;
                }
                else if (enemy1Active && speeds[x] == speed5 && moves5 > 0 && !InWave("Enemy " + enemies[0].name, wave))
                {
                    print(enemies[0].name);
                    roundOrder.Add(enemies[0]);
                    roundOrderTypes.Add("Enemy");
                    wave.Add("Enemy " + enemies[0].name);
                    moves5--;
                    i++;
                }
                else if (enemy2Active && speeds[x] == speed6 && moves6 > 0 && !InWave("Enemy " + enemies[1].name, wave))
                {
                    print(enemies[1].name);
                    roundOrder.Add(enemies[1]);
                    roundOrderTypes.Add("Enemy");
                    wave.Add("Enemy " + enemies[1].name);
                    moves6--;
                    i++;
                }
                else if (enemy3Active && speeds[x] == speed7 && moves7 > 0 && !InWave("Enemy " + enemies[2].name, wave))
                {
                    print(enemies[2].name);
                    roundOrder.Add(enemies[2]);
                    roundOrderTypes.Add("Enemy");
                    wave.Add("Enemy " + enemies[2].name);
                    moves7--;
                    i++;
                }
                else if (enemy4Active && speeds[x] == speed8 && moves8 > 0 && !InWave("Enemy " + enemies[3].name, wave))
                {
                    print(enemies[3].name);
                    roundOrder.Add(enemies[3]);
                    roundOrderTypes.Add("Enemy");
                    wave.Add("Enemy " + enemies[3].name);
                    moves8--;
                    i++;
                }
            }
            wave.Clear();
        }

        currentTurn = roundOrder[turn];
        //print(currentTurn);
        txtTurn.text = roundOrderTypes[0] + " " + currentTurn.name + "'s turn \n HP left: "+currentTurn.hitPoints;
        if(roundOrderTypes[turn] == "Enemy" && attackPool.Count > 0)
        {
            Attack(GetEnemyTarget());
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
            StartCoroutine(EnemyAttack());
        }
    }

    public void Attack(Beast target)
    {
        bool inFront = false;
        if(slot1 != null && slot1.Equals(currentTurn) && roundOrderTypes[turn] == "Player")
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
        }
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
                Attack(GetEnemyTarget());
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
        yield return new WaitForSeconds(1f);
        if(attackPool.Count>0)
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

    //Get the row to determine whether the attacker is using an A move or a B move
    string GetRow()
    {
        if (currentTurn == slot1 || currentTurn == slot3 || currentTurn == slot5
        || currentTurn == enemySlot2 || currentTurn == enemySlot4 || currentTurn == enemySlot6)
        {
            return "B";
        }
        else
        {
            return "A";
        }
    }

    //Remove the desired beast by setting its active variable to false and removing image
    public void RemoveBeast(string beastID)
    {
        totalBeasts -= 1;
        if (beastID == "player1")
        {
            player1Active = false;
            attackPool.Remove(players[0]);
            loadMission.RemoveImage(players[0], "Player");
            turn -= player1TurnsTaken;
        }
        else if (beastID == "player2")
        {
            player2Active = false;
            attackPool.Remove(players[1]);
            loadMission.RemoveImage(players[1], "Player");
            turn -= player2TurnsTaken;
        }
        else if (beastID == "player3")
        {
            player3Active = false;
            attackPool.Remove(players[2]);
            loadMission.RemoveImage(players[2], "Player");
            turn -= player3TurnsTaken;
        }
        else if (beastID == "player4")
        {
            player4Active = false;
            attackPool.Remove(players[3]);
            loadMission.RemoveImage(players[3], "Player");
            turn -= player4TurnsTaken;
        }
        else if (beastID == "enemy1")
        {
            enemy1Active = false;
            loadMission.RemoveImage(enemies[0], "Enemy");
            turn -= enemy1TurnsTaken;
        }
        else if (beastID == "enemy2")
        {
            enemy2Active = false;
            loadMission.RemoveImage(enemies[1], "Enemy");
            turn -= enemy2TurnsTaken;
        }
        else if (beastID == "enemy3")
        {
            enemy3Active = false;
            loadMission.RemoveImage(enemies[2], "Enemy");
            turn -= enemy3TurnsTaken;
        }
        else if (beastID == "enemy4")
        {
            enemy4Active = false;
            loadMission.RemoveImage(enemies[3], "Enemy");
            turn -= enemy4TurnsTaken;
        }

        LoadOrder(); //Re Create the round order
    }

    //Add turn to keep track of how many times a beast has went in the round
    //Used to keep track of how much the turn variable has to be edited by when a beast gets knocked out
    void AddTurn()
    {
        if (roundOrderTypes[turn] == "Player")
        {
            if (currentTurn == players[0]) player1TurnsTaken += 1;
            else if (currentTurn == players[1]) player2TurnsTaken += 1;
            else if (currentTurn == players[2]) player3TurnsTaken += 1;
            else if (currentTurn == players[3]) player4TurnsTaken += 1;
        }
        else
        {
            if (currentTurn == enemies[0]) enemy1TurnsTaken += 1;
            else if (currentTurn == enemies[1]) enemy2TurnsTaken += 1;
            else if (currentTurn == enemies[2]) enemy3TurnsTaken += 1;
            else if (currentTurn == enemies[3]) enemy4TurnsTaken += 1;
        }
    }

    void ClearTurns()
    {
        player1TurnsTaken = 0;
        player2TurnsTaken = 0;
        player3TurnsTaken = 0;
        player4TurnsTaken = 0;
        enemy1TurnsTaken = 0;
        enemy2TurnsTaken = 0;
        enemy3TurnsTaken = 0;
        enemy4TurnsTaken = 0;
    }


    /*
    public HealthManager healthManager;
    public BeastDatabase beastDatabase;
    public Attack attack;
    public LoadMission loadMission;

    public Text txtTurn;

    public List<Beast> players = new List<Beast>(8);

    public List<Beast> roundOrder = new List<Beast>(); //attack order per round

    public List<string> roundOrderTypes = new List<string>();

    public List<Beast> attackPool = new List<Beast>(); //

    public List<Beast> playerSlot = new List<Beast>(6); //what slot your beasts are in

    public List<Beast> enemySlot = new List<Beast>(6); //what slot your enemys beasts are in

    public List<bool> playerActive = new List<bool>(8); //if your beast is still alive

    public List<int> playersTurnTaken = new List<int>();


    public int turn = 0;
    int totalMoves;
//    int totalBeasts = 8;

    public Beast currentTurn;


    string slot1;
    string slot2;
    string slot3;
    string slot4;
    string slot5;
    string slot6;
    public string enemySlot1;
    public string enemySlot2;
    public string enemySlot3;
    public string enemySlot4;
    public string enemySlot5;
    public string enemySlot6;

    public bool player1Active = true;
    public bool player2Active = true;
    public bool player3Active = true;
    public bool player4Active = true;
    public bool enemy1Active = true;
    public bool enemy2Active = true;
    public bool enemy3Active = true;
    public bool enemy4Active = true;

    int player1TurnsTaken;
    int player2TurnsTaken;
    int player3TurnsTaken;
    int player4TurnsTaken;
    int enemy1TurnsTaken;
    int enemy2TurnsTaken;
    int enemy3TurnsTaken;
    int enemy4TurnsTaken;

    //Get lists from LoadMission and add the players to the attack pool
    public void SendLists(List<Beast> thisSquad, List<Beast> enemySquad)
    {

        for (int x = 0; x < thisSquad.Count; x++)
        {

            players.Add(thisSquad[x]);

            if (players.Count == thisSquad.Count && thisSquad.Count < 4 && x < thisSquad.Count)
            {
                if (thisSquad.Count < 2)
                {
                    players.Add(null);
                    attackPool.Add(null);
                }
                if (thisSquad.Count < 3)
                {
                    players.Add(null);
                    attackPool.Add(null);
                }

                players.Add(null);
                attackPool.Add(null);

            }
            attackPool.Add(players[x]);
        }
        for (int x = 4; x < 4 + enemySquad.Count; x++)
        {
            players.Add(enemySquad[x % 4]);

            if (players.Count - 4 == enemySquad.Count && enemySquad.Count < 4 && x % 4 < enemySquad.Count)
            {
                if (enemySquad.Count < 2)
                {
                    players.Add(null);
                    attackPool.Add(null);
                }
                if (enemySquad.Count < 3)
                {
                    players.Add(null);
                    attackPool.Add(null);
                }

                players.Add(null);
                attackPool.Add(null);

            }
        }
        for (int x = 0; x < players.Count; x++)
        {
            if (players[x] != null)
                playerActive.Add(true);
            else
                playerActive.Add(false);
        }

        //attack.healthManager.GetHealth(players);
        LoadOrder();
        ClearTurns();
    }

    //Get the slot info for each beast from LoadMission
    public void GetSlots(List<Beast> playerSlot, List<Beast> enemySlot) 
    {
        this.playerSlot = playerSlot;
        this.enemySlot = enemySlot;
    }

    //Create attack order
    void LoadOrder()
    {
        List<int> moves = new List<int>();

        //Get each players moves per round
        for (int x = 0; x < players.Count; x++)
        {
            if (playerActive[x] && players[x] != null)
            { moves.Add(players[x].number_MOVES); }
            else if (players[x] == null)
            {
                moves.Add(0);
            }
        }

        for (int x = 0; x < moves.Count; x++)
        {
            totalMoves += moves[x];
        }

        //Get each player's speed
        int[] speed = new int[8];

        for (int x = 0; x < players.Count; x++)
        {
            if (players[x] != null)
            {
                speed[x] = players[x].speed;
            }
            else
                speed[x] = 0;
        }

        int i = 0;

        List<Beast> wave = new List<Beast>();

        //Create an array with each speed and sort it highest to lowest

        System.Array.Sort(speed);
        System.Array.Reverse(speed);

        //Clear the previous round order
        roundOrder.Clear();
        roundOrderTypes.Clear();

        //Loop through the speed array and find the corresponding beast and add them to the round order
        while (i < totalMoves)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < players.Count; y++)
                {

                    if (players[y] != null && playerActive[y] && players[y].speed == speed[x] && moves[y] > 0 && !InWave(players[y], wave))
                    {
                        roundOrder.Add(players[y]);
                        if (y < 4)
                            roundOrderTypes.Add("Player");
                        else
                            roundOrderTypes.Add("Enemy");
                        wave.Add(players[y]);
                        moves[y]--;
                        i++;
                        break;
                    }
                    else if (players[y] != null)
                    {
                        i++;
                    }
                    else
                        roundOrder.Add(null);
                }
            }
            wave.Clear();
        }

        currentTurn = roundOrder[turn];
        txtTurn.text = roundOrderTypes[0] + " " + currentTurn + "'s turn";
    }

    //Check to see of beast is used in the loop yet
    bool InWave(Beast beast, List<Beast> wave)
    {
        for (int i = 0; i < wave.Count; i++)
        {
            if (wave[i].Equals(beast))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeTurn()
    {
        print("ROUND ORDER COUNT IN TAKE TURNS: "+roundOrder.Count);
        while (roundOrder.Count - 2 >= turn)
        {
            turn++;
            if (roundOrder[turn % 8] != null)
            {
                currentTurn = roundOrder[turn % 8];
                txtTurn.text = roundOrderTypes[turn] + " " + currentTurn + " 's turn";
                StartCoroutine(EnemyAttack());
                break;
            }
        }
    }

    public void Attack(Beast target)
    {
        //Check to see if the round is still going and then run an attack
        if (turn == totalMoves - 1)
        {
            attack.InitiateAttack(currentTurn, target);
            Debug.Log("Round Ended");
            ClearTurns();
            currentTurn = roundOrder[0];
            txtTurn.text = roundOrderTypes[0] + " " + currentTurn + "'s turn";
            turn = 0;
        }
        else
        {
            attack.InitiateAttack(currentTurn, target);
            AddTurn();
            TakeTurn();
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1f);
        Attack(GetEnemyTarget());
    }

    //Enemy targets a random player from a pool of active player beasts
    Beast GetEnemyTarget()
    {
        while (true)
        {
            int rand = 0;
            for (int x = 0; x < players.Count; x++)
            {
                if (players[x] != null && currentTurn != null && currentTurn.Equals(players[x]))
                {
                    if (x < 4)
                    {
                        rand = Random.Range(players.Count - 4, players.Count-1);
                    }
                    else
                    {
                        rand = Random.Range(0, players.Count - 3);
                    }
                }
            }

            if (players[rand] != null && playerActive[rand])
            {
                return players[rand];
            }
        }
    }

    //Get the row to determine whether the attacker is using an A move or a B move
    Move GetRow()
    {
        for (int x = 0; x < playerSlot.Count; x++)
        {
            if (currentTurn == playerSlot[x] || currentTurn == enemySlot[x])
            {
                if (x < 4)
                    return currentTurn.Move_A;
                else
                    return currentTurn.Move_B;
            }
        }
        return null; //beacause Manoli is super needy
    }

    //Remove the desired beast by setting its active variable to false and removing image
    public void RemoveBeast(Beast beast)
    {
        for (int x = 0; x < players.Count; x++)
        {

            if (players[x] != null && beast.id == players[x].id)
            {
                playerActive[x] = false;
                attackPool[x] = null;
               // loadMission.RemoveImage(players[x], "Player"); COME BACK AND FIX INSIDE LOADMISSION CLASS
                turn -= playersTurnTaken[x];
                break;
            }
        }
        LoadOrder(); //Re Create the round order
    }

    //Add turn to keep track of how many times a beast has went in the round
    //Used to keep track of how much the turn variable has to be edited by when a beast gets knocked out
    void AddTurn()
    {
        for (int x = 0; x < players.Count; x++)
        {
            if (currentTurn.Equals(players[x])) playersTurnTaken[x] += 1;
        }
    }

    void ClearTurns()
    {
        if (playersTurnTaken.Count < 8)
        {
            for (int x = 0; x < players.Count; x++)
            {
                playersTurnTaken.Add(0);
            }
        }
        else
        {
            for (int x = 0; x < players.Count; x++)
            {
                playersTurnTaken[x] = 0;
            }
        }
    }*/
}
