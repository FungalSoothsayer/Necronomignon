using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//manages everything related to health
public class HealthManager : MonoBehaviour
{
    public BattleManager battleManager;
    public LevelChecker levelChecker;

    public int playersLeft = Values.SQUADMAX;
    public int enemiesLeft = 0;

    List<HealthBar> playerHealthBars = new List<HealthBar>();
    List<HealthBar> enemyHealthBars = new List<HealthBar>();

    List<DamageOutput> playerDamageBar = new List<DamageOutput>();
    List<DamageOutput> enemyDamageBar = new List<DamageOutput>();

    List<Beast> squad = new List<Beast>();
    List<Beast> enemies = new List<Beast>();

    public List<GameObject> winners = new List<GameObject>();

    public GameObject victoryScreen;

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

        //checks if the player is not null and sets the max health of the health bar
        for(int x = 0; x < activePlayersHealth.Count; x++)
        {
            if (players[x] != null)
            {
                activePlayersHealth[x].SetMaxHealth(players[x].maxHP);
            }
            else playersLeft--;
        }
        for(int x =0; x< activeEnemiesHealth.Count; x++)
        {
            if (opposing[x] != null)
            {
                enemiesLeft += 1;
                activeEnemiesHealth[x].SetMaxHealth(opposing[x].maxHP);
            }
        }
        
    }

    //Subtract the damage from the target's health
    public void UpdateHealth(Beast target, int damage)
    {
        //removes the health from beasts that have been attacked 
        for(int x = 0; x< Values.SQUADMAX; x++)
        {
            
            if (target == squad[x%squad.Count])
            {
                if(squad[x % squad.Count].hitPoints <= 0)
                {
                    battleManager.RemoveBeast(squad[x % squad.Count]);
                    break;
                }
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
                if(enemies[x].hitPoints < 0)
                {
                    battleManager.RemoveBeast(enemies[x]);
                    break;
                }
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

    }
    //adds health to the given beast upto the beasts maxHP
    public void heal(Beast target, double heal)
    {
        //looks for the beast that needs to be healed and heals it up to it's max hp
        for(int x = 0; x < Values.SQUADMAX; x++)
        {
            if(target == squad[x % squad.Count])
            {
                squad[x].hitPoints += int.Parse(Math.Floor(heal)+"");
                if (squad[x].hitPoints > squad[x].maxHP)
                {
                    squad[x].hitPoints = squad[x].maxHP;
                }
                playerHealthBars[x].SetHealth(squad[x].hitPoints);
            }
            else if(target == enemies[x%enemies.Count])
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
    //prints the health left, needs to be updated to implement ui
    void DisplayHealthLeft(Beast target, int healthLeft)
    {
        Debug.Log(target.name + " has " + healthLeft + " health left.");
    }

    //Check to see if there are any players left, if not end game
    void CheckRemainingPlayers()
    {
        playersLeft--;
        if (playersLeft <= 0)
        {
            Debug.Log("Opposing Team Wins. Better Luck Nex Time.");
            StartCoroutine(LoadMap());
        }
    }
    //Check to see if there are any enemies left, if not end game
    void CheckRemainingOpposing()
    {
        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            Debug.Log("Congratulations! You Win!");
            levelChecker.Progess(SceneManager.GetActiveScene().name);
            StartCoroutine(displayVictoryScreen());
        }
    }

    //Display the victory popup with the winning squad and rewards for winning the battle.
    IEnumerator displayVictoryScreen()
    {
        yield return new WaitForSeconds(1.5f);
        victoryScreen.SetActive(true);
        for(int x = 0; x < Values.SQUADMAX; x++)
        {
            if(squad[x] != null)
            {
                winners[x].GetComponent<Animator>().runtimeAnimatorController = Resources.Load
                    ("Animations/" + squad[x].name + "/" + squad[x].name + "_Controller") as RuntimeAnimatorController;
            }
            else
            {
                winners[x].SetActive(false);
            }
        }

        StartCoroutine(winnersAnimations());
    }

    //Play the 'roaring' animation for the winning team.
    IEnumerator winnersAnimations()
    {
        yield return new WaitForSeconds(2f);
        foreach(GameObject g in winners)
        g.GetComponent<Animator>().SetTrigger("Front");
    }

    //Collect rewards after winning a battle.
    public void onCollect()
    {
        victoryScreen.SetActive(false);
        StartCoroutine(LoadMap());
    }

    //After 1 second load the Map scene
    IEnumerator LoadMap()
    {
        yield return new WaitForSeconds(1);
        LoadScenes load = new LoadScenes();
        load.LoadSelect("Map");
            
        //SceneManager.LoadScene("Map");
    }
}