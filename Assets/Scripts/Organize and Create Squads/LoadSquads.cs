using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSquads : MonoBehaviour
{
    public SquadData squadData;

    public Text squad1Text;
    public Text squad2Text;

    public List<GameObject> squad1Slots;
    public List<GameObject> squad2Slots;

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
                S1S[x].GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/" + toLoad[x].name + "/" + toLoad[x].name + "_Controller") as RuntimeAnimatorController;
                squad1Slots[x].SetActive(true);
            }
        }
    }

    void LoadSquad2Images()
    {
        List<Beast> toLoad = new List<Beast>();
        toLoad = squadData.GetSquadList(2);

        for (int x = 0; x < S2S.Count; x++)
        {
            if (toLoad[x] != null)
            {
                S2S[x].GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/" + toLoad[x].name + "/" + toLoad[x].name + "_Controller") as RuntimeAnimatorController;
                squad2Slots[x].SetActive(true);
            }
        }
    }

    string GetImage(Beast beast)
    {
        return beast.static_img;
    }
}
