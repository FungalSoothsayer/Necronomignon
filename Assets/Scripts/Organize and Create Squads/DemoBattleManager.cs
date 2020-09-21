using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoBattleManager : MonoBehaviour
{
    public LoadDemoBattle loadDemoBattle;
    public SquadData squadData;
    public BeastDatabase beastDatabase;
    public Attack attack;
    public HealthManager healthManager;

    int squadNumber;

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

    public Image order1;
    public Image order2;
    public Image order3;
    public Image order4;
    public Image order5;
    public Image order6;
    public Image order7;
    public Image order8;
    public Image order9;

    List<string> thisSquad = new List<string>();
    List<string> roundOrder = new List<string>();
    List<string> enemies = new List<string>();

    int turn = 0;
    int totalMoves;

    string currentTurn;

    void Start()
    {
        enemies.Add("Target");
        enemies.Add("");
        enemies.Add("");
        enemies.Add("");
        squadNumber = loadDemoBattle.GetSquadNumber();
        LoadSquadImages();
        LoadOrder();
        healthManager.GetHealth(thisSquad, enemies);
    }

    void LoadSquadImages()
    {
        List<string> toLoad = new List<string>();
        toLoad = squadData.GetSquadList(squadNumber);

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
    }

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

    void LoadOrder()
    {
        int moves1 = beastDatabase.GetMoves(thisSquad[0]);
        int moves2 = beastDatabase.GetMoves(thisSquad[1]);
        int moves3 = beastDatabase.GetMoves(thisSquad[2]);
        int moves4 = beastDatabase.GetMoves(thisSquad[3]);
        totalMoves = moves1 + moves2 + moves3 + moves4;

        int speed1 = beastDatabase.GetSpeed(thisSquad[0]);
        int speed2 = beastDatabase.GetSpeed(thisSquad[1]);
        int speed3 = beastDatabase.GetSpeed(thisSquad[2]);
        int speed4 = beastDatabase.GetSpeed(thisSquad[3]);

        int i = 0;

        int[] speeds = { speed1, speed2, speed3, speed4 };
        System.Array.Sort(speeds);
        System.Array.Reverse(speeds);
        
        while(i < totalMoves)
        {
            for (int x = 0; x < 4; x++)
            {
                if (speeds[x] == speed1 && moves1 > 0)
                {
                    roundOrder.Add(thisSquad[0]);
                    moves1--;
                    i++;
                }
                else if (speeds[x] == speed2 && moves2 > 0)
                {
                    roundOrder.Add(thisSquad[1]);
                    moves2--;
                    i++;
                }
                else if (speeds[x] == speed3 && moves3 > 0)
                {
                    roundOrder.Add(thisSquad[2]);
                    moves3--;
                    i++;
                }
                else if (speeds[x] == speed4 && moves4 > 0)
                {
                    roundOrder.Add(thisSquad[3]);
                    moves4--;
                    i++;
                }
            }
        }
        UpdateOrderBar();
    }

    void UpdateOrderBar()
    {
        currentTurn = roundOrder[turn];
        try
        {
            order1.sprite = Resources.Load<Sprite>(GetImage(roundOrder[0 + turn]));
        }
        catch
        {
            order1.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order2.sprite = Resources.Load<Sprite>(GetImage(roundOrder[1 + turn]));
        }
        catch
        {
            order2.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order3.sprite = Resources.Load<Sprite>(GetImage(roundOrder[2 + turn]));
        }
        catch
        {
            order3.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order4.sprite = Resources.Load<Sprite>(GetImage(roundOrder[3 + turn]));
        }
        catch
        {
            order4.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order5.sprite = Resources.Load<Sprite>(GetImage(roundOrder[4 + turn]));
        }
        catch
        {
            order5.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order6.sprite = Resources.Load<Sprite>(GetImage(roundOrder[5 + turn]));
        }
        catch
        {
            order6.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order7.sprite = Resources.Load<Sprite>(GetImage(roundOrder[6 + turn]));
        }
        catch
        {
            order7.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order8.sprite = Resources.Load<Sprite>(GetImage(roundOrder[7 + turn]));
        }
        catch
        {
            order8.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
        try
        {
            order9.sprite = Resources.Load<Sprite>(GetImage(roundOrder[8 + turn]));
        }
        catch
        {
            order9.sprite = Resources.Load<Sprite>("EmptyRectangle");
        }
    }

    public void TakeTurn()
    {
        if (turn == totalMoves - 1)
        {
            Debug.Log("Round Ended");
            turn = 0;
            UpdateOrderBar();
        }
        else
        {
            turn++;
            UpdateOrderBar();
            Debug.Log(currentTurn + "'s turn");
        }

    }

    //public void Attack()
    //{
    //    if (turn == totalMoves - 1)
    //    {
    //        attack.InitiateAttack(currentTurn, "Target", GetRow());
    //        Debug.Log("Round Ended");
    //        turn = 0;
    //        UpdateOrderBar();
    //    }
    //    else 
    //    {
    //        attack.InitiateAttack(currentTurn, "Target", GetRow());
    //        TakeTurn();
    //    }
    //}

    //string GetRow()
    //{
    //    if (currentTurn == slot1 || currentTurn == slot3 || currentTurn == slot5)
    //    {
    //        return "B";
    //    }
    //    else
    //    {
    //        return "A";
    //    }
    //}
}
