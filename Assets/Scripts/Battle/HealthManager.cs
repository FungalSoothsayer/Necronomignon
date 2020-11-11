using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public BattleManager battleManager;

    public int playersLeft = 4;
    public int enemiesLeft = 0;

    List<HealthBar> playerHealthBars = new List<HealthBar>();
    List<HealthBar> enemyHealthBars = new List<HealthBar>();


    List<DamageOutput> playerDamageBar = new List<DamageOutput>();
    List<DamageOutput> enemyDamageBar = new List<DamageOutput>();


    List<Beast> squad = new List<Beast>();
    List<Beast> enemies = new List<Beast>();

    //Get the health for each beast in play from BeastDatabase
    public void GetHealth(List<Beast> players, List<Beast> opposing, List<HealthBar> activePlayersHealth, List<HealthBar> activeEnemiesHealth, List<DamageOutput> activePlayerDamage, List<DamageOutput> activeEnemyDamage)
    {
        for (int i = 5; i >= 0; i--)
        {
            if (activePlayersHealth[i] == null)
            {
                activePlayersHealth.RemoveAt(i);
            }

            if(activePlayerDamage[i] == null)
            {
                activePlayerDamage.RemoveAt(i);
            }
        }

        for (int i = 5; i >= 0; i--)
        {
            if (activeEnemiesHealth[i] == null)
            {
                activeEnemiesHealth.RemoveAt(i);
            }

            if(activeEnemyDamage[i] == null)
            {
                activeEnemyDamage.RemoveAt(i);
            }
        }

        squad = players;
        enemies = opposing;
        playerHealthBars = activePlayersHealth;
        enemyHealthBars = activeEnemiesHealth;

        

        playerDamageBar = activePlayerDamage;
        enemyDamageBar = activeEnemyDamage;



        activePlayersHealth[0].SetMaxHealth(players[0].maxHP);
        
        if (players[1] != null)
        {
            activePlayersHealth[1].SetMaxHealth(players[1].maxHP);
        }
        else playersLeft--;
        if (players[2] != null)
        {
            activePlayersHealth[2].SetMaxHealth(players[2].maxHP);
        }
        else playersLeft--;
        if (players[3] != null)
        {
            activePlayersHealth[3].SetMaxHealth(players[3].maxHP);
        }
        else playersLeft--;
        if (opposing[0] != null)
        {
            enemiesLeft += 1;
            activeEnemiesHealth[0].SetMaxHealth(opposing[0].maxHP);
        }
        if (opposing[1] != null)
        {
            enemiesLeft += 1;
            activeEnemiesHealth[1].SetMaxHealth(opposing[1].maxHP);
        }
        if (opposing[2] != null)
        {
            enemiesLeft += 1;
            activeEnemiesHealth[2].SetMaxHealth(opposing[2].maxHP);
        }
        if (opposing[3] != null)
        {
            enemiesLeft += 1;
            activeEnemiesHealth[3].SetMaxHealth(opposing[3].maxHP);
        }
    }

    //Subtract the damage from the target's health
    public void UpdateHealth(Beast target, int damage)
    {

        for(int x = 0; x< 4; x++)
        {
            if (target == squad[x%squad.Count])
            {
                squad[x % squad.Count].hitPoints -= damage;
                playerHealthBars[x % squad.Count].SetHealth(squad[x % squad.Count].hitPoints);

                playerDamageBar[x % squad.Count].setText(damage);

                if (squad[x % squad.Count].hitPoints <= 0)
                {
                    Debug.Log(target.name + " is knocked out.");
                    CheckRemainingPlayers();
                    battleManager.RemoveBeast(squad[x % squad.Count]);
                }
                else
                {

                    DisplayHealthLeft(target, squad[x % squad.Count].hitPoints);
                }
            }
            else if(target == enemies[x])
            {
                enemies[x].hitPoints -= damage;
                enemyHealthBars[x].SetHealth(enemies[x].hitPoints);

                enemyDamageBar[x].setText(damage);

                if (enemies[x].hitPoints <= 0)
                {
                    Debug.Log(target.name + " is knocked out.");
                    CheckRemainingOpposing();
                    battleManager.RemoveBeast(enemies[x]);
                }
                else
                {
                    DisplayHealthLeft(target, enemies[x].hitPoints);
                }
            }
        }

        /*if (target == squad[0])
        {
            squad[0].hitPoints -= damage;
            playerHealthBars[0].SetHealth(squad[0].hitPoints);
            
            playerDamageBar[0].setText(damage);

            if (squad[0].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast(squad[0]);
            }
            else
            {
                
                DisplayHealthLeft(target, squad[0].hitPoints);
            }
        }
        else if (target == squad[1])
        {
            squad[1].hitPoints -= damage;
            playerHealthBars[1].SetHealth(squad[1].hitPoints);
           
            playerDamageBar[1].setText(damage);

            if (squad[1].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast(squad[1]);
            }
            else
            {             
                DisplayHealthLeft(target, squad[1].hitPoints);
            }
        }
        else if (target == squad[2])
        {
            squad[2].hitPoints -= damage;
            playerHealthBars[2].SetHealth(squad[2].hitPoints);
           
            playerDamageBar[2].setText(damage);
            

            if (squad[2].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast(squad[2]);
            }
            else
            {
                DisplayHealthLeft(target, squad[2].hitPoints);
            }
        }
        else if (target == squad[3])
        {
            squad[3].hitPoints -= damage;
            playerHealthBars[3].SetHealth(squad[3].hitPoints);
           
            playerDamageBar[3].setText(damage);

            if (squad[3].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingPlayers();
                battleManager.RemoveBeast(squad[3]);
            }
            else
            {
                DisplayHealthLeft(target, squad[3].hitPoints);
            }
        }
        else if (target == enemies[0])
        {
            enemies[0].hitPoints -= damage;
            enemyHealthBars[0].SetHealth(enemies[0].hitPoints);
            
            enemyDamageBar[0].setText(damage);

            if (enemies[0].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingOpposing();
                battleManager.RemoveBeast(enemies[0]);
            }
            else
            {
                DisplayHealthLeft(target, enemies[0].hitPoints);
            }
        }
        else if (target == enemies[1])
        {
            enemies[1].hitPoints -= damage;
            enemyHealthBars[1].SetHealth(enemies[1].hitPoints);
            enemyDamageBar[1].setText(damage);

            if (enemies[1].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingOpposing();
                battleManager.RemoveBeast(enemies[1]);
            }
            else
            {
                DisplayHealthLeft(target, enemies[1].hitPoints);
            }
        }
        else if (target == enemies[2])
        {
            enemies[2].hitPoints -= damage;
            enemyHealthBars[2].SetHealth(enemies[2].hitPoints);
            
            enemyDamageBar[2].setText(damage);

            if (enemies[2].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingOpposing();
                battleManager.RemoveBeast(enemies[2]);
            }
            else
            {
                DisplayHealthLeft(target, enemies[2].hitPoints);
            }
        }
        else if (target == enemies[3])
        {
            enemies[3].hitPoints -= damage;
            enemyHealthBars[3].SetHealth(enemies[3].hitPoints);
            
            enemyDamageBar[3].setText(damage);

            if (enemies[3].hitPoints <= 0)
            {
                Debug.Log(target.name + " is knocked out.");
                CheckRemainingOpposing();
                battleManager.RemoveBeast(enemies[3]);
            }
            else
            {
                DisplayHealthLeft(target, enemies[3].hitPoints);
            }
        }*/
    }
    public void heal(Beast target, double heal)
    {
        for(int x = 0; x < 4; x++)
        {
            if(target == squad[x])
            {
                squad[x].hitPoints += int.Parse(Math.Floor(heal)+"");
                if (squad[x].hitPoints > squad[x].maxHP)
                {
                    squad[x].hitPoints = squad[x].maxHP;
                }
                playerHealthBars[x].SetHealth(squad[x].hitPoints);
            }
            else if(target == enemies[x])
            {
                enemies[x].hitPoints += int.Parse(Math.Floor(heal)+"");
                if (enemies[x].hitPoints > enemies[x].maxHP)
                {
                    enemies[x].hitPoints = enemies[x].maxHP;
                }
                enemyHealthBars[x].SetHealth(enemies[x].hitPoints);
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
        if (playersLeft <= 0)
        {
            Debug.Log("Opposing Team Wins. Better Luck Nex Time.");
            StartCoroutine(LoadMap());
        }
    }
    //Check to see if there are any enemies left, if not end game
    void CheckRemainingOpposing()
    {
        enemiesLeft --;
        if (enemiesLeft <= 0)
        {
            Debug.Log("Congratulations! You Win!");
            StartCoroutine(LoadMap());
        }
    }
    //After 1 second load the Map scene
    IEnumerator LoadMap()
    {
        print(enemiesLeft +" enemies");
        print(playersLeft + " friends");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Map");
    }
}