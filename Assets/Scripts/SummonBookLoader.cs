using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonBookLoader : MonoBehaviour
{
    public BeastManager beastManager;

    public List<string> beastImages;
    public List<GameObject> beastTexts;

    public List<Image> slots;

    public List<string> summonedImages = new List<string>();
    public List<Beast> summoned = new List<Beast>();

    public List<GameObject> poolSlots;
    public GameObject back;
    public GameObject forward;

    public int counter = 0;

    void Start()
    {
        if (!beastManager.isLoaded())
        {
            beastManager.Awake();
        }
        BeastList bl = BeastManager.beastsList;
        for (int x = 0; x < bl.Beasts.Count; x++)
        {
            bl.Beasts[x].setAttacks();
            summonedImages.Add(bl.Beasts[x].static_img);
            beastImages.Add(bl.Beasts[x].static_img);
            summoned.Add(bl.Beasts[x]);
        }

        SetImages();
    }

    //Fill up the image slots with your summoned beasts
    void SetImages()
    {
        for (int x = 0 + (counter * 6); x < slots.Count + (counter * 6); x++)
        {
            if (summoned.Count >= x + 1)
            {
                slots[x % 6].gameObject.SetActive(true);
                print(x % 6);
                slots[x % 6].sprite = Resources.Load<Sprite>(summonedImages[x]);
            }
            else
            {
                print(x % 6);
                slots[x % 6].sprite = Resources.Load<Sprite>("EmptyRectangle");
            }
        }

        if (counter == 0)
        {
            back.SetActive(false);
        }
        else if (counter > 0)
        {
            back.SetActive(true);
        }
        if (counter + 6 >= summoned.Count)
        {
            forward.SetActive(false);
        }
        else if (counter + 6 < summoned.Count)
        {
            forward.SetActive(true);
        }
    }

    public void Forward()
    {
        changeImages("Forward");
    }

    public void Back()
    {
        changeImages("Back");
    }

    public void changeImages(string str)
    {
        if (str == "Forward")
        {
            counter++;
        }
        else if (str == "Back")
        {
            counter--;
        }

        SetImages();
    }
}
