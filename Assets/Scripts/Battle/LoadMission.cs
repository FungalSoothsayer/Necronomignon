using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//prepares all information and data for battle
public class LoadMission : MonoBehaviour
{
    public SquadData squadData;
    public MissionList missionList;
    public BattleManager battleManager;
    public BeastManager beastManager;

    public GameObject playerPad;
    public GameObject txtChoose;
    public GameObject btnSquad1;
    public GameObject btnSquad2;
    public GameObject txtInfo;
    public GameObject orderBar;
    public GameObject healthDisplay;
    
    public List<GameObject> slotHealthBars;

    // HEALTH BARS: 
    
    public List<HealthBar> playerHealthBars;
   
    public List<HealthBar> enemyHealthBars;

    // DAMAGE BARS
    
    public List<DamageOutput> pSlotDmgs;
    
    public List<DamageOutput> eSlotDmgs;
   
    public List<Image> playerSlotImg;

    
    public List<Beast> playerSlot = new List<Beast>(8);
    
    public List<Image> enemySlotImg;
    
    public List<Beast> enemySlot = new List<Beast>(8);

    List<Beast> thisSquad = new List<Beast>();
    List<Beast> toLoad = new List<Beast>();
    List<Beast> enemyToLoad = new List<Beast>();
    Summoner enemySummoner;
    List<Beast> enemySquad = new List<Beast>();

    List<HealthBar> activePlayersHealth = new List<HealthBar>();
    List<HealthBar> activeEnemiesHealth = new List<HealthBar>();

    List<DamageOutput> activePlayerDamageBar = new List<DamageOutput>();

    List<DamageOutput> activeEnemyDamageBar = new List<DamageOutput>();
    
    int squadMissing = 0;

    void Start()
    {
        
        if(GameObject.Find("btnLvl1"))
        {
            missionList.mission = "random";
        }

        playerPad.SetActive(false);
        if (!squadData.GetSquad1Status())
        {
            btnSquad1.SetActive(false);
            squadMissing += 1;
        }
        if (!squadData.GetSquad2Status())
        {
            btnSquad2.SetActive(false);
            squadMissing += 1;
        }
        if (squadMissing == 2)
        {
            txtInfo.SetActive(true);
        }
        else
        {
            txtInfo.SetActive(false);
        }

        foreach(GameObject go in slotHealthBars)
        {
            go.SetActive(false);
        }

        enemyToLoad = missionList.enemies;
        enemySummoner = missionList.summoner;
        LoadEnemySquadImages();
    }
    //Connected to btnSquad1
    public void SetSquad1()
    {
        toLoad = squadData.GetSquadList(1);
        txtChoose.SetActive(false);
        btnSquad1.SetActive(false);
        btnSquad2.SetActive(false);
        LoadSquadImages();
    }
    //Connected to btnSquad2
    public void SetSquad2()
    {
        toLoad = squadData.GetSquadList(2);
        txtChoose.SetActive(false);
        btnSquad1.SetActive(false);
        btnSquad2.SetActive(false);
        LoadSquadImages();
    }
    //If enemy is there, load the corresponding image
    void LoadEnemySquadImages()
    {
        for (int x = 0; x < enemyToLoad.Count; x++)
        {

            if (enemyToLoad[x] != null)
            {
                enemySlotImg[x].GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/" + enemyToLoad[x].name + "/" + enemyToLoad[x].name + "_Controller") as RuntimeAnimatorController;
                /*Beast b = new Beast();
                b = BeastManager.getFromNameS(enemyToLoad[x].name);*/
                enemySlot.Add(enemyToLoad[x]);
                enemySquad.Add(enemyToLoad[x]);
                activeEnemiesHealth.Add(enemyHealthBars[x]);
                activeEnemyDamageBar.Add(eSlotDmgs[x]);
            }
            else
            {
                enemySlotImg[x].gameObject.SetActive(false);
                enemySlot.Add(null);
                activeEnemiesHealth.Add(null);
                activeEnemyDamageBar.Add(null);
            }
        }
    }
    //If beast is in slot, load the corresponding image
    void LoadSquadImages()
    {
        orderBar.SetActive(true);
        healthDisplay.SetActive(true);
        foreach(Image slot in playerSlotImg)
        {
            slot.gameObject.SetActive(true);
        }

        for (int x = 0; x < toLoad.Count; x++)
        {
            if (toLoad[x] != null && toLoad[x].speed == 0)
            {
                toLoad[x] = null;
            }
            if (toLoad[x] != null)
            {
                playerSlotImg[x].GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/" + toLoad[x].name + "/" + toLoad[x].name + "_Controller") as RuntimeAnimatorController; 
                Beast b = new Beast();
                b = beastManager.getFromName(toLoad[x].name);
                playerSlot.Add(b);
                thisSquad.Add(b);

                activePlayersHealth.Add(playerHealthBars[x]);
                
                activePlayerDamageBar.Add(pSlotDmgs[x]);
            }
            else
            {
                playerSlotImg[x].gameObject.SetActive(false);
                playerSlot.Add(null);
                activePlayersHealth.Add(null);
                activePlayerDamageBar.Add(null);
            } 
        }
        //sets all health bars as active
        foreach(GameObject healthBar in slotHealthBars)
        {
            healthBar.SetActive(true);
        }

        List<Beast> pb = new List<Beast>();
        List<Beast> eb = new List<Beast>();


        for (int x = 0; x< playerSlot.Count; x++)
        {
            //gives the players beasts a boost based on their tier
            if(playerSlot[x] != null)
            {
                playerSlot[x].power += (int)(playerSlot[x].power * (Values.TEIRBOOST * (playerSlot[x].tier - 1)));
                playerSlot[x].defence += (int)(playerSlot[x].defence * (Values.TEIRBOOST * (playerSlot[x].tier - 1)));
                playerSlot[x].speed += (int)(playerSlot[x].speed * (Values.TEIRBOOST * (playerSlot[x].tier - 1)));
                playerSlot[x].dexterity += (int)(playerSlot[x].dexterity * (Values.TEIRBOOST * (playerSlot[x].tier - 1)));
                playerSlot[x].maxHP += (int)(playerSlot[x].maxHP * (Values.TEIRBOOST * (playerSlot[x].tier - 1)));

                print(playerSlot[x].statGradients);
                playerSlot[x].power += playerSlot[x].statGradients.powerGradient * (Player.summoner.getLevel() - 1);
                playerSlot[x].defence += playerSlot[x].statGradients.defenceGradient * (Player.summoner.getLevel() - 1);
                playerSlot[x].speed += playerSlot[x].statGradients.speedGradient * (Player.summoner.getLevel() - 1);
                playerSlot[x].dexterity += playerSlot[x].statGradients.dexGradient * (Player.summoner.getLevel() - 1);
                playerSlot[x].maxHP += playerSlot[x].statGradients.hpGradient * (Player.summoner.getLevel() - 1);
            }
            pb.Add(playerSlot[x]);
        }
        for (int x = 0; x < enemySlot.Count; x++)
        {
            //gives the players beasts a boost based on their tier
            if (enemySlot[x] != null && enemySlot[x].tier>0)
            {
                enemySlot[x].power += (int)(enemySlot[x].power * (Values.TEIRBOOST * (enemySlot[x].tier - 1)));
                enemySlot[x].defence += (int)(enemySlot[x].defence * (Values.TEIRBOOST * (enemySlot[x].tier - 1)));
                enemySlot[x].speed += (int)(enemySlot[x].speed * (Values.TEIRBOOST * (enemySlot[x].tier - 1)));
                enemySlot[x].dexterity += (int)(enemySlot[x].dexterity * (Values.TEIRBOOST * (enemySlot[x].tier - 1)));
                enemySlot[x].maxHP += (int)(enemySlot[x].maxHP * (Values.TEIRBOOST * (enemySlot[x].tier - 1)));

                enemySlot[x].power += enemySlot[x].statGradients.powerGradient * (enemySummoner.getLevel() - 1);
                enemySlot[x].defence += enemySlot[x].statGradients.defenceGradient * (enemySummoner.getLevel() - 1);
                enemySlot[x].speed += enemySlot[x].statGradients.speedGradient * (enemySummoner.getLevel() - 1);
                enemySlot[x].dexterity += enemySlot[x].statGradients.dexGradient * (enemySummoner.getLevel() - 1);
                enemySlot[x].maxHP += enemySlot[x].statGradients.hpGradient * (enemySummoner.getLevel()-1);
            }
            eb.Add(enemySlot[x]);
        }

        battleManager.SendLists(thisSquad, enemySquad, activePlayersHealth, activeEnemiesHealth, activePlayerDamageBar, activeEnemyDamageBar, enemySummoner);
        battleManager.GetSlots(pb, eb);
       // battleManager.GetSlots(playerSlot[0], playerSlot[1], playerSlot[2], playerSlot[3], playerSlot[4], playerSlot[5], enemySlot[0], enemySlot[1], enemySlot[2], enemySlot[3], enemySlot[4], enemySlot[5]);
    }

    //Get the images from the resources folder to be loaded
    string GetImage(Beast beast)
    {
        return beast.static_img;
    }
    //Remove image when beast is knocked out
    public void RemoveImage(Beast toRemove, string owner)
    {
        GetImageToRemove(toRemove, owner).gameObject.GetComponent<Animator>().SetInteger("Health", 0);
        StartCoroutine(PlayDeathAnimation(toRemove, owner));
    }
    //Get the slot to remove the image from
    Image GetImageToRemove(Beast beast, string owner)
    {
        for (int x = 0; x < 11; x++)
        {
            if (owner.Equals("Player"))
            {
                if (beast.Equals(playerSlot[x]))
                {
                    return playerSlotImg[x];
                }
            }
            else
            {
                if (beast.Equals(enemySlot[x]))
                {
                    return enemySlotImg[x];
                }
            }

        }
        return null;
    }
    //plays death animation whenever someone dies
    IEnumerator PlayDeathAnimation(Beast toRemove, string owner)
    {
        yield return new WaitForSeconds(2f);

        GetImageToRemove(toRemove, owner).gameObject.SetActive(false);
    }
}