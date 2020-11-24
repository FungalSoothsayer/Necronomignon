using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoBattleManager : MonoBehaviour
{
    public LoadDemoBattle loadDemoBattle;
    public SquadData squadData;
    public Attack attack;
    public HealthManager healthManager;

    int squadNumber;

    public List<Image> slotImgs;
    public List<Beast> slots;
    public List<Image> orderImgs;

    List<Beast> thisSquad = new List<Beast>();
    List<Beast> roundOrder = new List<Beast>();
    List<Beast> enemies = new List<Beast>();

    int turn = 0;
    int totalMoves;

    Beast currentTurn;

    void Start()
    {
        Beast b = new Beast();
        b.name = "Target";
        b.hitPoints = 1000000;
        b.defence = 50;
        b.power = 1;
        b.speed = 1;
        b.dexterity = 1;
        b.number_MOVES = 0;
        enemies.Add(b);
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
        squadNumber = loadDemoBattle.GetSquadNumber();
        LoadSquadImages();
        LoadOrder();
    }

    void LoadSquadImages()
    {
        List<Beast> toLoad = new List<Beast>();

        toLoad = squadData.GetSquadList(squadNumber);

        for(int x = 0; x < toLoad.Count; x++)
        {
            if(toLoad[x] != null)
            {
                slots.Add(toLoad[x]);
                slotImgs[x].GetComponent<Animator>().runtimeAnimatorController = 
                    Resources.Load("Animations/" + toLoad[x].name + "/" + toLoad[x].name + "_Controller") as RuntimeAnimatorController;
                thisSquad.Add(toLoad[x]);
            }
            else
            {
                slots.Add(null);
                slotImgs[x].gameObject.SetActive(false);
            }
        }
    }

    string GetImage(Beast beast)
    {
        return beast.static_img;
    }

    void LoadOrder()
    {
        List<int> moves = new List<int>();
        List<int> speeds = new List<int>();

        for (int x = 0; x < thisSquad.Count; x++)
        {
            if( thisSquad[x] != null)
            {
                moves.Add(thisSquad[x].number_MOVES);
                speeds.Add(thisSquad[x].speed);
            }
        }
        foreach(int x in moves)
        {
            totalMoves += x;
        }

        int i = 0;

        speeds.Sort();
        speeds.Reverse();

        while(i < totalMoves)
        {
            for (int x = 0; x < thisSquad.Count; x++)
            {
                for(int y = 0; y < thisSquad.Count; y++)
                {
                    if(speeds[x] == thisSquad[y].speed && moves[y] > 0)
                    {
                        roundOrder.Add(thisSquad[y]);
                        moves[y]--;
                        i++;
                    }
                }
            }
        }
        UpdateOrderBar();
    }

    void UpdateOrderBar()
    {
        currentTurn = roundOrder[turn];
        for (int x = 0; x < orderImgs.Count; x++)
        {
            try
            {
                orderImgs[x].sprite = Resources.Load<Sprite>(GetImage(roundOrder[x + turn]));
            }
            catch
            {
                orderImgs[x].sprite = Resources.Load<Sprite>("EmptyRectangle");
            }
        }
    }

    public void Attack()
    {
        bool inFront = this.inFront();

        GameObject slot = getSlot();
        if (inFront) slot.GetComponent<Animator>().SetTrigger("Front");
        else slot.GetComponent<Animator>().SetTrigger("Back");
        TakeTurn();
    }

    void TakeTurn()
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

    bool inFront()
    {
        for (int x = 0; x < slots.Count; x++)
        {
            if (x < 3 && (currentTurn.Equals(slots[x])))
            {
                return true;
            }
        }
        return false;
    }

    GameObject getSlot()
    {
        for (int x = 0; x < slots.Count; x++)
        {
            if (slots[x] != null && currentTurn.name == slots[x].name)
            {
                return slotImgs[x].gameObject;
            }
        }

        return null;
    }
}
