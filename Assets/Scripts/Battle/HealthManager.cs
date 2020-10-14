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

        player1 = players[0].hitPoints;
        if (players[1] != null)
        { 
            player2 = players[1].hitPoints;
        }
        else playersLeft--;
        if (players[2] != null) 
        {
            player3 = players[2].hitPoints;
        }
        else playersLeft--;
        if (players[3] != null)
        {
            player4 = players[3].hitPoints;
        }
        else playersLeft--;

        if (opposing[0] != null) 
        {
            enemiesLeft += 1;
            enemy1 = opposing[0].hitPoints;
        }
        if (opposing[1] != null)
        {
            enemiesLeft += 1;
            enemy2 = opposing[1].hitPoints;
        }
        if (opposing[2] != null)
        {
            enemiesLeft += 1;
            enemy3 = opposing[2].hitPoints;
        }
        if (opposing[3] != null)
        {
            enemiesLeft += 1;
            enemy4 = opposing[3].hitPoints;
        }
    }

    //Subtract the damage from the target's health
    public void UpdateHealth(Beast target, int damage) 
    {
        if (target == squad[0])
        {
            squad[0].hitPoints -= damage;
            if(squad[0].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast("player1");
            }
            else
            {
                DisplayHealthLeft(target, squad[0].hitPoints);
            }
        }
        else if (target == squad[1])
        {
            squad[1].hitPoints -= damage;
            if (squad[1].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast("player2");
            }
            else
            {
                DisplayHealthLeft(target, squad[1].hitPoints);
            }
        }
        else if (target == squad[2])
        {
            squad[2].hitPoints -= damage;
            if (squad[2].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast("player3");
            }
            else
            {
                DisplayHealthLeft(target, squad[2].hitPoints);
            }
        }
        else if (target == squad[3])
        {
            squad[3].hitPoints -= damage;
            if (squad[3].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                battleManager.RemoveBeast("player4");
                CheckRemainingPlayers();
            }
            else
            {
                DisplayHealthLeft(target, squad[3].hitPoints);
            }
        }
        else if (target == enemies[0])
        {
            enemies[0].hitPoints -= damage;
            if (enemies[0].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                battleManager.RemoveBeast("enemy1");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemies[0].hitPoints);
            }
        }
        else if (target == enemies[1])
        {
            enemies[1].hitPoints -= damage;
            if (enemies[1].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                battleManager.RemoveBeast("enemy2");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemies[1].hitPoints);
            }
        }
        else if (target == enemies[2])
        {
            enemies[2].hitPoints -= damage;
            if (enemies[2].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                battleManager.RemoveBeast("enemy3");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemies[2].hitPoints);
            }
        }
        else if (target == enemies[3])
        {
            enemies[3].hitPoints -= damage;
            if (enemies[3].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                battleManager.RemoveBeast("enemy4");
                CheckRemainingOpposing();
            }
            else
            {
                DisplayHealthLeft(target, enemies[3].hitPoints);
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
        if(playersLeft <= 0)
        {
            Debug.Log("Opposing Team Wins. Better Luck Nex Time.");
            StartCoroutine(LoadMap());
        }
    }

    //Check to see if there are any enemies left, if not end game
    void CheckRemainingOpposing()
    {
        enemiesLeft -= 1;
        if (enemiesLeft <= 0)
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
