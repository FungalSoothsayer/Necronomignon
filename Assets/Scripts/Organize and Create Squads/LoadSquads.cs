using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSquads : MonoBehaviour
{
    public SquadData squadData;

    public Text squad1Text;
    public Text squad2Text;

    /*public GameObject squad1Slot1;
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
    public GameObject squad2Slot6;*/
    public List<GameObject> squad1Slots;
    public List<GameObject> squad2Slots;

   /* public Image s1s1;
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
    public Image s2s6;*/
    public List<Image> S1S;
    public List<Image> S2S;


    void Start()
    {
        foreach(GameObject slot in squad1Slots)
        {
            slot.SetActive(false);
        }
        foreach (GameObject slot in squad2Slots)
        {
            slot.SetActive(false);
        }
        /*squad1Slot1.SetActive(false);
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
        squad2Slot6.SetActive(false);*/

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
        List<Beast> toLoad = new List<Beast>();
        toLoad = squadData.GetSquadList(1);

        for(int x = 0; x < S1S.Count; x++)
        {
            if (toLoad[x] != null)
            {
                S1S[x].sprite = Resources.Load<Sprite>(GetImage(toLoad[x]));
                squad1Slots[x].SetActive(true);
            }
        }

        /*if (toLoad[0] != null)
        {
            s1s1.sprite = Resources.Load<Sprite>(GetImage(toLoad[0]));
            squad1Slot1.SetActive(true);
        }
        if (toLoad[1] != null)
        {
            s1s2.sprite = Resources.Load<Sprite>(GetImage(toLoad[1]));
            squad1Slot2.SetActive(true);
        }
        if (toLoad[2] != null)
        {
            s1s3.sprite = Resources.Load<Sprite>(GetImage(toLoad[2]));
            squad1Slot3.SetActive(true);
        }
        if (toLoad[3] != null)
        {
            s1s4.sprite = Resources.Load<Sprite>(GetImage(toLoad[3]));
            squad1Slot4.SetActive(true);
        }
        if (toLoad[4] != null)
        {
            s1s5.sprite = Resources.Load<Sprite>(GetImage(toLoad[4]));
            squad1Slot5.SetActive(true);
        }
        if (toLoad[5] != null)
        {
            s1s6.sprite = Resources.Load<Sprite>(GetImage(toLoad[5]));
            squad1Slot6.SetActive(true);
        }*/
    }

    void LoadSquad2Images()
    {
        List<Beast> toLoad = new List<Beast>();
        toLoad = squadData.GetSquadList(2);

        for (int x = 0; x < S2S.Count; x++)
        {
            if (toLoad[x] != null)
            {
                S2S[x].sprite = Resources.Load<Sprite>(GetImage(toLoad[x]));
                squad2Slots[x].SetActive(true);
            }
        }

        /*if (toLoad[0] != null)
        {
            s2s1.sprite = Resources.Load<Sprite>(GetImage(toLoad[0]));
            squad2Slot1.SetActive(true);
        }
        if (toLoad[1] != null)
        {
            s2s2.sprite = Resources.Load<Sprite>(GetImage(toLoad[1]));
            squad2Slot2.SetActive(true);
        }
        if (toLoad[2] != null)
        {
            s2s3.sprite = Resources.Load<Sprite>(GetImage(toLoad[2]));
            squad2Slot3.SetActive(true);
        }
        if (toLoad[3] != null)
        {
            s2s4.sprite = Resources.Load<Sprite>(GetImage(toLoad[3]));
            squad2Slot4.SetActive(true);
        }
        if (toLoad[4] != null)
        {
            s2s5.sprite = Resources.Load<Sprite>(GetImage(toLoad[4]));
            squad2Slot5.SetActive(true);
        }
        if (toLoad[5] != null)
        {
            s2s6.sprite = Resources.Load<Sprite>(GetImage(toLoad[5]));
            squad2Slot6.SetActive(true);
        }*/
    }

    string GetImage(Beast beast)
    {
        return beast.static_img;
    }
}
