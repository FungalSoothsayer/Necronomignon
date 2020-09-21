using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSquads : MonoBehaviour
{
    public SquadData squadData;

    public Text squad1Text;
    public Text squad2Text;

    public GameObject squad1Slot1;
    public GameObject squad1Slot2;
    public GameObject squad1Slot3;
    public GameObject squad1Slot4;
    public GameObject squad1Slot5;
    public GameObject squad1Slot6;
    public GameObject squad2Slot1;
    public GameObject squad2Slot2;
    public GameObject squad2Slot3;
    public GameObject squad2Slot4;
    public GameObject squad2Slot5;
    public GameObject squad2Slot6;

    public Image s1s1;
    public Image s1s2;
    public Image s1s3;
    public Image s1s4;
    public Image s1s5;
    public Image s1s6;
    public Image s2s1;
    public Image s2s2;
    public Image s2s3;
    public Image s2s4;
    public Image s2s5;
    public Image s2s6;

    void Start()
    {
        squad1Slot1.SetActive(false);
        squad1Slot2.SetActive(false);
        squad1Slot3.SetActive(false);
        squad1Slot4.SetActive(false);
        squad1Slot5.SetActive(false);
        squad1Slot6.SetActive(false);
        squad2Slot1.SetActive(false);
        squad2Slot2.SetActive(false);
        squad2Slot3.SetActive(false);
        squad2Slot4.SetActive(false);
        squad2Slot5.SetActive(false);
        squad2Slot6.SetActive(false);

        if (squadData.GetSquad1Status())
        {
            squad1Text.text = "Squad 1";
            LoadSquad1Images();
        }

        if (squadData.GetSquad2Status())
        {
            squad2Text.text = "Squad 2";
            LoadSquad2Images();
        }
    }

    void LoadSquad1Images()
    {
        List<string> toLoad = new List<string>();
        toLoad = squadData.GetSquadList(1);

        if (toLoad[0] != "")
        {
            s1s1.sprite = Resources.Load<Sprite>(GetImage(toLoad[0]));
            squad1Slot1.SetActive(true);
        }
        if (toLoad[1] != "")
        {
            s1s2.sprite = Resources.Load<Sprite>(GetImage(toLoad[1]));
            squad1Slot2.SetActive(true);
        }
        if (toLoad[2] != "")
        {
            s1s3.sprite = Resources.Load<Sprite>(GetImage(toLoad[2]));
            squad1Slot3.SetActive(true);
        }
        if (toLoad[3] != "")
        {
            s1s4.sprite = Resources.Load<Sprite>(GetImage(toLoad[3]));
            squad1Slot4.SetActive(true);
        }
        if (toLoad[4] != "")
        {
            s1s5.sprite = Resources.Load<Sprite>(GetImage(toLoad[4]));
            squad1Slot5.SetActive(true);
        }
        if (toLoad[5] != "")
        {
            s1s6.sprite = Resources.Load<Sprite>(GetImage(toLoad[5]));
            squad1Slot6.SetActive(true);
        }
    }

    void LoadSquad2Images()
    {
        List<string> toLoad = new List<string>();
        toLoad = squadData.GetSquadList(2);
        if (toLoad[0] != "")
        {
            s2s1.sprite = Resources.Load<Sprite>(GetImage(toLoad[0]));
            squad2Slot1.SetActive(true);
        }
        if (toLoad[1] != "")
        {
            s2s2.sprite = Resources.Load<Sprite>(GetImage(toLoad[1]));
            squad2Slot2.SetActive(true);
        }
        if (toLoad[2] != "")
        {
            s2s3.sprite = Resources.Load<Sprite>(GetImage(toLoad[2]));
            squad2Slot3.SetActive(true);
        }
        if (toLoad[3] != "")
        {
            s2s4.sprite = Resources.Load<Sprite>(GetImage(toLoad[3]));
            squad2Slot4.SetActive(true);
        }
        if (toLoad[4] != "")
        {
            s2s5.sprite = Resources.Load<Sprite>(GetImage(toLoad[4]));
            squad2Slot5.SetActive(true);
        }
        if (toLoad[5] != "")
        {
            s2s6.sprite = Resources.Load<Sprite>(GetImage(toLoad[5]));
            squad2Slot6.SetActive(true);
        }
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
}
