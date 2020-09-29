using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public BeastDatabase beastDatabase;
    public BattleManager battleManager;

    public int player1 = 0;
    public int player2 = 0;
    public int player3 = 0;
    public int player4 = 0;
    public int playersLeft = 4;

    public int enemy1 = 0;
    public int enemy2 = 0;
    public int enemy3 = 0;
    public int enemy4 = 0;
    public int enemiesLeft = 0;

    List<Beast> squad = new List<Beast>();
    List<Beast> enemies = new List<Beast>();

    //Get the health for each beast in play from BeastDatabase
    public void GetHealth(List<Beast> players, List<Beast> opposing)
    {
        squad = players;
        enemies = opposing;

        player1 = beastDatabase.GetHitPoints(players[0].name);
        player2 = beastDatabase.GetHitPoints(players[1].name);
        player3 = beastDatabase.GetHitPoints(players[2].name);
        player4 = beastDatabase.GetHitPoints(players[3].name);

        if(opposing[0].name != "") 
        {
            enemiesLeft += 1;
            enemy1 = beastDatabase.GetHitPoints(opposing[0].name);
        }
        if (opposing[1].name != "")
        {
            enemiesLeft += 1;
            enemy2 = beastDatabase.GetHitPoints(opposing[1].name);
        }
        if (opposing[2].name != "")
        {
            enemiesLeft += 1;
            enemy3 = beastDatabase.GetHitPoints(opposing[2].name);
        }
        if (opposing[3].name != "")
        {
            enemiesLeft += 1;
            enemy4 = beastDatabase.GetHitPoints(opposing[3].name);
        }
    }

    //Subtract the damage from the target's health
    public void UpdateHealth(Beast target, int damage) 
    {
        if (target == squad[0])
        {
            player1 -= damage;
            if(player1 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                CheckRemainingPlayers();
                //battleManager.RemoveBeast("player1");
            }
            else
            {
                DisplayHealthLeft(target, player1);
            }
        }
        else if (target == squad[1])
        {
            player2 -= damage;
            if (player2 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                CheckRemainingPlayers();
                //battleManager.RemoveBeast("player2");
            }
            else
            {
                DisplayHealthLeft(target, player2);
            }
        }
        else if (target == squad[2])
        {
            player3 -= damage;
            if (player3 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                CheckRemainingPlayers();
                //battleManager.RemoveBeast("player3");
            }
            else
            {
                DisplayHealthLeft(target, player3);
            }
        }
        else if (target == squad[3])
        {
            player4 -= damage;
            if (player4 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                //battleManager.RemoveBeast("player4");
                CheckRemainingPlayers();
            }
            else
            {
                DisplayHealthLeft(target, player4);
            }
        }
        else if (target == enemies[0])
        {
            enemy1 -= damage;
            if (enemy1 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                //battleManager.RemoveBeast("enemy1");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemy1);
            }
        }
        else if (target == enemies[1])
        {
            enemy2 -= damage;
            if (enemy2 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                //battleManager.RemoveBeast("enemy2");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemy2);
            }
        }
        else if (target == enemies[2])
        {
            enemy3 -= damage;
            if (enemy3 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                //battleManager.RemoveBeast("enemy3");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemy3);
            }
        }
        else if (target == enemies[3])
        {
            enemy4 -= damage;
            if (enemy4 <= 0)
            {
                Debug.Log(target + " is knocked out.");
                //battleManager.RemoveBeast("enemy4");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemy4);
            }
        }
    }

    void DisplayHealthLeft(Beast target, int healthLeft)
    {
        Debug.Log(target.name + " has " + healthLeft + " health left.");
    }

    //Check to see if there are any players left, if not end game
    void CheckRemainingPlayers() 
    {
        playersLeft -= 1;
        if(playersLeft == 0)
        {
            Debug.Log("Opposing Team Wins. Better Luck Nex Time.");
            StartCoroutine(LoadMap());
        }
    }

    //Check to see if there are any enemies left, if not end game
    void CheckRemainingOpposing()
    {
        enemiesLeft -= 1;
        if (enemiesLeft == 0)
        {
            Debug.Log("Congratulations! You Win!");
            StartCoroutine(LoadMap());
        }
    }

    //After 1 second load the Map scene
    IEnumerator LoadMap()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Map");
    }
}
