using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMission : MonoBehaviour
{
    public SquadData squadData;
    public MissionList missionList;
    public BattleManager battleManager;

    public GameObject grid;
    public GameObject txtChoose;
    public GameObject btnSquad1;
    public GameObject btnSquad2;
    public GameObject txtInfo;

    public Image slot1Img;
    public Image slot2Img;
    public Image slot3Img;
    public Image slot4Img;
    public Image slot5Img;
    public Image slot6Img;

    public string slot1;
    public string slot2;
    public string slot3;
    public string slot4;
    public string slot5;
    public string slot6;

    public Image enemySlot1Img;
    public Image enemySlot2Img;
    public Image enemySlot3Img;
    public Image enemySlot4Img;
    public Image enemySlot5Img;
    public Image enemySlot6Img;

    public string enemySlot1;
    public string enemySlot2;
    public string enemySlot3;
    public string enemySlot4;
    public string enemySlot5;
    public string enemySlot6;

    List<string> thisSquad = new List<string>();
    List<string> toLoad = new List<string>();
    List<string> enemyToLoad = new List<string>();
    List<string> enemySquad = new List<string>();

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
        if (enemyToLoad[0] != "")
        {
            enemySlot1Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[0]));
            enemySlot1 = enemyToLoad[0];
            enemySquad.Add(enemyToLoad[0]);
        }
        else enemySlot1Img.gameObject.SetActive(false);
        if (enemyToLoad[1] != "")
        {
            enemySlot2Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[1]));
            enemySlot2 = enemyToLoad[1];
            enemySquad.Add(enemyToLoad[1]);
        }
        else enemySlot2Img.gameObject.SetActive(false);
        if (enemyToLoad[2] != "")
        {
            enemySlot3Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[2]));
            enemySlot3 = enemyToLoad[2];
            enemySquad.Add(enemyToLoad[2]);
        }
        else enemySlot3Img.gameObject.SetActive(false);
        if (enemyToLoad[3] != "")
        {
            enemySlot4Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[3]));
            enemySlot4 = enemyToLoad[3];
            enemySquad.Add(enemyToLoad[3]);
        }
        else enemySlot4Img.gameObject.SetActive(false);
        if (enemyToLoad[4] != "")
        {
            enemySlot5Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[4]));
            enemySlot5 = enemyToLoad[4];
            enemySquad.Add(enemyToLoad[4]);
        }
        else enemySlot5Img.gameObject.SetActive(false);
        if (enemyToLoad[5] != "")
        {
            enemySlot6Img.sprite = Resources.Load<Sprite>(GetImage(enemyToLoad[5]));
            enemySlot6 = enemyToLoad[5];
            enemySquad.Add(enemyToLoad[5]);
        }
        else enemySlot6Img.gameObject.SetActive(false);
    }

    //If beast is in slot, load the corresponding image
    void LoadSquadImages()
    {
        grid.SetActive(true);
        if (toLoad[0] != "")
        {
            slot1Img.sprite = Resources.Load<Sprite>(GetImage(toLoad[0]));
            slot1 = toLoad[0];
            thisSquad.Add(toLoad[0]);
        }
        else slot1Img.gameObject.SetActive(false);
        if (toLoad[1] != "")
        {
            slot2Img.sprite = Resources.Load<Sprite>(GetImage(toLoad[1]));
            slot2 = toLoad[1];
            thisSquad.Add(toLoad[1]);
        }
        else slot2Img.gameObject.SetActive(false);
        if (toLoad[2] != "")
        {
            slot3Img.sprite = Resources.Load<Sprite>(GetImage(toLoad[2]));
            slot3 = toLoad[2];
            thisSquad.Add(toLoad[2]);
        }
        else slot3Img.gameObject.SetActive(false);
        if (toLoad[3] != "")
        {
            slot4Img.sprite = Resources.Load<Sprite>(GetImage(toLoad[3]));
            slot4 = toLoad[3];
            thisSquad.Add(toLoad[3]);
        }
        else slot4Img.gameObject.SetActive(false);
        if (toLoad[4] != "")
        {
            slot5Img.sprite = Resources.Load<Sprite>(GetImage(toLoad[4]));
            slot5 = toLoad[4];
            thisSquad.Add(toLoad[4]);
        }
        else slot5Img.gameObject.SetActive(false);
        if (toLoad[5] != "")
        {
            slot6Img.sprite = Resources.Load<Sprite>(GetImage(toLoad[5]));
            slot6 = toLoad[5];
            thisSquad.Add(toLoad[5]);
        }
        else slot6Img.gameObject.SetActive(false);
        battleManager.SendLists(thisSquad, enemySquad);
        battleManager.GetSlots(slot1, slot2, slot3, slot4, slot5, slot6, enemySlot1, enemySlot2, enemySlot3, enemySlot4, enemySlot5, enemySlot6);
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
    public void RemoveImage(string toRemove, string owner)
    {
        GetImageToRemove(toRemove, owner).gameObject.SetActive(false);
    }

    //Get the slot to remove the image from
    Image GetImageToRemove(string beast, string owner)
    {
        if(owner == "Player")
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
        }
    }
}
