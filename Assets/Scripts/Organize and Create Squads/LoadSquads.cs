using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles the images and animations of squads 1 and 2 when they need to be loaded
 */
public class LoadSquads : MonoBehaviour
{
    public SquadData squadData;

    public List<GameObject> squad1Slots;
    public List<GameObject> squad2Slots;
    public List<Image> S1S;
    public List<Image> S2S;

    public Text squad1Text;
    public Text squad2Text;

    public List<string> summonedNames = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        BeastList bl = BeastManager.beastsList;

        for (int x = 0; x < bl.Beasts.Count; x++)
        {
            bl.Beasts[x].setAttacks();
            if (bl.Beasts[x].tier > 0)
            {
                summonedNames.Add(bl.Beasts[x].name);
            }
        }

        foreach (GameObject slot in squad1Slots)
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

    // Loads the animations of the beasts saved into squad 1
    void LoadSquad1Images()
    {
        List<Beast> toLoad = new List<Beast>();
        toLoad = squadData.GetSquadList(1);

        for(int x = 0; x < S1S.Count; x++)
        {
            S1S[x % 11].sprite = Resources.Load<Sprite>("Static_Images/EmptyRectangle");

            print(x);
            if (toLoad[x] != null)
            {

                GameObject beastPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Beasts/" + summonedNames[x]));
                beastPrefab.transform.SetParent(GameObject.Find("Slot" + (x % 11 + 1)).transform);
                beastPrefab.transform.localPosition = new Vector3(0, 0);
                beastPrefab.transform.localRotation = Quaternion.identity;

                Animator animator = beastPrefab.GetComponent<Animator>();
                animator.enabled = false;
            }
        }
    }

    // Loads the animations of the beasts saved into squad 2
    void LoadSquad2Images()
    {
        List<Beast> toLoad = new List<Beast>();
        toLoad = squadData.GetSquadList(2);

        for (int x = 0; x < S2S.Count; x++)
        {
            S2S[x % 11].sprite = Resources.Load<Sprite>("Static_Images/EmptyRectangle");

            if (toLoad[x] != null)
            {

                GameObject beastPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Beasts/" + summonedNames[x]));
                beastPrefab.transform.SetParent(GameObject.Find("Slot" + (x % 11 + 1)).transform);
                beastPrefab.transform.localPosition = new Vector3(0, 0);
                beastPrefab.transform.localRotation = Quaternion.identity;

                Animator animator = beastPrefab.GetComponent<Animator>();
                animator.enabled = false;
            }
        }
    }

    // Returns the static image assosiated with the selected beast
    string GetImage(Beast beast)
    {
        return beast.static_img;
    }
}
