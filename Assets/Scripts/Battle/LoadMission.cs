using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject slot1HealthBar;
    public GameObject slot2HealthBar;
    public GameObject slot3HealthBar;
    public GameObject slot4HealthBar;
    public GameObject slot5HealthBar;
    public GameObject slot6HealthBar;

    // HEALTH BARS: 
    public HealthBar slot1Health;
    public HealthBar slot2Health;
    public HealthBar slot3Health;
    public HealthBar slot4Health;
    public HealthBar slot5Health;
    public HealthBar slot6Health;
    List<HealthBar> playerHealthBars = new List<HealthBar>(6);
    public HealthBar eslot1Health;
    public HealthBar eslot2Health;
    public HealthBar eslot3Health;
    public HealthBar eslot4Health;
    public HealthBar eslot5Health;
    public HealthBar eslot6Health;
    List<HealthBar> enemyHealthBars = new List<HealthBar>(6);

    // DAMAGE BARS
    public DamageOutput pSlot1Dmg;
    public DamageOutput pSlot2Dmg;
    public DamageOutput pSlot3Dmg;
    public DamageOutput pSlot4Dmg;
    public DamageOutput pSlot5Dmg;
    public DamageOutput pSlot6Dmg;

    public DamageOutput eSlot1Dmg;
    public DamageOutput eSlot2Dmg;
    public DamageOutput eSlot3Dmg;
    public DamageOutput eSlot4Dmg;
    public DamageOutput eSlot5Dmg;
    public DamageOutput eSlot6Dmg;


    public Image slot1Img;
    public Image slot2Img;
    public Image slot3Img;
    public Image slot4Img;
    public Image slot5Img;
    public Image slot6Img;
    public List<Image> playerSlotImg = new List<Image>(6);

    public Beast slot1;
    public Beast slot2;
    public Beast slot3;
    public Beast slot4;
    public Beast slot5;
    public Beast slot6;
    public List<Beast> playerSlot = new List<Beast>(6);
    public Image enemySlot1Img;
    public Image enemySlot2Img;
    public Image enemySlot3Img;
    public Image enemySlot4Img;
    public Image enemySlot5Img;
    public Image enemySlot6Img;
    public List<Image> enemySlotImg = new List<Image>(6);
    public Beast enemySlot1;
    public Beast enemySlot2;
    public Beast enemySlot3;
    public Beast enemySlot4;
    public Beast enemySlot5;
    public Beast enemySlot6;
    public List<Beast> enemySlot = new List<Beast>(6);

    List<Beast> thisSquad = new List<Beast>();
    List<Beast> toLoad = new List<Beast>();
    List<Beast> enemyToLoad = new List<Beast>();
    List<Beast> enemySquad = new List<Beast>();

    List<HealthBar> activePlayersHealth = new List<HealthBar>();
    List<HealthBar> activeEnemiesHealth = new List<HealthBar>();

    List<DamageOutput> playerDamageBar = new List<DamageOutput>();
    List<DamageOutput> activePlayerDamageBar = new List<DamageOutput>();

    List<DamageOutput> enemyDamageBar = new List<DamageOutput>();
    List<DamageOutput> activeEnemyDamageBar = new List<DamageOutput>();
    
    int squadMissing = 0;

    void Start()
    {
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
        playerSlotImg.Add(slot1Img);
        playerSlotImg.Add(slot2Img);
        playerSlotImg.Add(slot3Img);
        playerSlotImg.Add(slot4Img);
        playerSlotImg.Add(slot5Img);
        playerSlotImg.Add(slot6Img);
        enemySlotImg.Add(enemySlot1Img);
        enemySlotImg.Add(enemySlot2Img);
        enemySlotImg.Add(enemySlot3Img);
        enemySlotImg.Add(enemySlot4Img);
        enemySlotImg.Add(enemySlot5Img);
        enemySlotImg.Add(enemySlot6Img);
        //sorry for bad code
        playerHealthBars.Add(slot1Health);
        playerHealthBars.Add(slot2Health);
        playerHealthBars.Add(slot3Health);
        playerHealthBars.Add(slot4Health);
        playerHealthBars.Add(slot5Health);
        playerHealthBars.Add(slot6Health);
        enemyHealthBars.Add(eslot1Health);
        enemyHealthBars.Add(eslot2Health);
        enemyHealthBars.Add(eslot3Health);
        enemyHealthBars.Add(eslot4Health);
        enemyHealthBars.Add(eslot5Health);
        enemyHealthBars.Add(eslot6Health);


        playerDamageBar.Add(pSlot1Dmg);
        playerDamageBar.Add(pSlot2Dmg);
        playerDamageBar.Add(pSlot3Dmg);
        playerDamageBar.Add(pSlot4Dmg);
        playerDamageBar.Add(pSlot5Dmg);
        playerDamageBar.Add(pSlot6Dmg);

        enemyDamageBar.Add(eSlot1Dmg);
        enemyDamageBar.Add(eSlot2Dmg);
        enemyDamageBar.Add(eSlot3Dmg);
        enemyDamageBar.Add(eSlot4Dmg);
        enemyDamageBar.Add(eSlot5Dmg);
        enemyDamageBar.Add(eSlot6Dmg);



        slot1HealthBar.SetActive(false);
        slot2HealthBar.SetActive(false);
        slot3HealthBar.SetActive(false);
        slot4HealthBar.SetActive(false);
        slot5HealthBar.SetActive(false);
        slot6HealthBar.SetActive(false);

        enemyToLoad = missionList.enemies;
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
                enemySlotImg[x].sprite = Resources.Load<Sprite>(enemyToLoad[x].static_img);
                enemySlot.Add(enemyToLoad[x]);
                enemySquad.Add(enemyToLoad[x]);
                activeEnemiesHealth.Add(enemyHealthBars[x]);
                activeEnemyDamageBar.Add(enemyDamageBar[x]);
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
        playerPad.SetActive(true);
        for (int x = 0; x < toLoad.Count; x++)
        {
            if (toLoad[x] != null && toLoad[x].speed == 0)
            {
                toLoad[x] = null;
            }
            if (toLoad[x] != null)
            {
                playerSlotImg[x].sprite = Resources.Load<Sprite>(toLoad[x].static_img);
                playerSlot.Add(beastManager.getFromName(toLoad[x].name));
                thisSquad.Add(beastManager.getFromName(toLoad[x].name));

                activePlayersHealth.Add(playerHealthBars[x]);
                
                activePlayerDamageBar.Add(playerDamageBar[x]);
                

            }
            else
            {
                playerSlotImg[x].gameObject.SetActive(false);
                playerSlot.Add(null);
                activePlayersHealth.Add(null);
                activePlayerDamageBar.Add(null);
            }
            
        }
        slot1HealthBar.SetActive(true);
        slot2HealthBar.SetActive(true);
        slot3HealthBar.SetActive(true);
        slot4HealthBar.SetActive(true);
        slot5HealthBar.SetActive(true);
        slot6HealthBar.SetActive(true);

        slot1 = playerSlot[0];
        slot2 = playerSlot[1];
        slot3 = playerSlot[2];
        slot4 = playerSlot[3];
        slot5 = playerSlot[4];
        slot6 = playerSlot[5];
        enemySlot1 = enemySlot[0];
        enemySlot2 = enemySlot[1];
        enemySlot3 = enemySlot[2];
        enemySlot4 = enemySlot[3];
        enemySlot5 = enemySlot[4];
        enemySlot6 = enemySlot[5];



        battleManager.SendLists(thisSquad, enemySquad, activePlayersHealth, activeEnemiesHealth, activePlayerDamageBar, activeEnemyDamageBar);
        battleManager.GetSlots(playerSlot[0], playerSlot[1], playerSlot[2], playerSlot[3], playerSlot[4], playerSlot[5], enemySlot[0], enemySlot[1], enemySlot[2], enemySlot[3], enemySlot[4], enemySlot[5]);
    }

    //Get the images from the resources folder to be loaded
    string GetImage(string beast)
    {
        if (beast == "Gaia") return "Boss Nature Titan Tellia-4";
        else if (beast == "Cthulhu") return "Boss Cthulhu-3";
        else if (beast == "Trogdor") return "Boss Mythical Stag Kyris-3";
        else if (beast == "Behemoth") return "Boss Wolfbull Demon Goliath-4";
        else if (beast == "Naglfar") return "Dragons Hydra-3";
        else if (beast == "Sunbather") return "Boss Darklord Excelsios-1";
        else return "";
    }
    //Remove image when beast is knocked out
    public void RemoveImage(Beast toRemove, string owner)
    {
        GetImageToRemove(toRemove, owner).gameObject.SetActive(false);
    }
    //Get the slot to remove the image from
    Image GetImageToRemove(Beast beast, string owner)
    {
        print(beast);
        for (int x = 0; x < 6; x++)
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
                    return enemySlotImg[x];
            }

        }
        return null;
    }
}
