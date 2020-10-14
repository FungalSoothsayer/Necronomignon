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

    public GameObject grid;
    public GameObject txtChoose;
    public GameObject btnSquad1;
    public GameObject btnSquad2;
    public GameObject txtInfo;
<<<<<<< HEAD
=======

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
>>>>>>> parent of 52bf3f3... Finished health bars

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

    int squadMissing = 0;

    void Start()
    {
        grid.SetActive(false);
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

<<<<<<< HEAD
=======
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

>>>>>>> parent of 52bf3f3... Finished health bars
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
        /*
           if (enemyToLoad[0] != "")
        {
            enemySlot1Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[0]));
            enemySlot1 = enemyToLoad[0];
            enemySquad.Add(enemyToLoad[0]);
        }
        else enemySlot1Img.gameObject.SetActive(false);
        */

        for (int x = 0; x < enemyToLoad.Count; x++)
        {
            
            if (enemyToLoad[x] != null)
            {
                enemySlotImg[x].sprite = Resources.Load<Sprite>(enemyToLoad[x].static_img);
                enemySlot.Add(enemyToLoad[x]);
                enemySquad.Add(enemyToLoad[x]);
            }
            else
            {
                enemySlotImg[x].gameObject.SetActive(false);
                enemySlot.Add(null);
            }
        }
    }

    //If beast is in slot, load the corresponding image
    void LoadSquadImages()
    {
        grid.SetActive(true);
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
            }
            else
            {
                playerSlotImg[x].gameObject.SetActive(false);
                playerSlot.Add(null);
            }
        }
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

        battleManager.SendLists(thisSquad, enemySquad);
        battleManager.GetSlots(playerSlot[0], playerSlot[1], playerSlot[2], playerSlot[3], playerSlot[4], playerSlot[5], enemySlot[0], enemySlot[1], enemySlot[2], enemySlot[3], enemySlot[4], enemySlot[5]);

        //battleManager.SendLists(thisSquad, enemySquad);
        //battleManager.GetSlots(playerSlot, enemySlot);
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
        for(int x = 0; x < 6; x++)
        {


            if(owner.Equals("Player"))
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

        /*      if(owner == "Player")
              {
                  if (beast == slot1) return slot1Img;
                  else if (beast == slot2) return slot2Img;
                  else if (beast == slot3) return slot3Img;
                  else if (beast == slot4) return slot4Img;
                  else if (beast == slot5) return slot5Img;
                  else return slot6Img;
              }
              else
              {
                  if (beast == enemySlot1) return enemySlot1Img;
                  else if (beast == enemySlot2) return enemySlot2Img;
                  else if (beast == enemySlot3) return enemySlot3Img;
                  else if (beast == enemySlot4) return enemySlot4Img;
                  else if (beast == enemySlot5) return enemySlot5Img;
                  else return enemySlot6Img;
              }*/
    }
}
