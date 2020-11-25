using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class SummonBookLoader : MonoBehaviour
{
    static public string beastName;
    static public int counter = 0;

    public BeastManager beastManager;

    public List<GameObject> beastTexts;
    public List<Image> slots;

    List<Beast> sorted = new List<Beast>();
    public List<string> summonedNames = new List<string>();
    public List<string> summonedImages = new List<string>();
    public List<Beast> summoned = new List<Beast>();

    public List<GameObject> poolSlots;
    public GameObject back;
    public GameObject forward;

    void Start()
    {
        if (!beastManager.isLoaded())
        {
            beastManager.Awake();
        }
        BeastList bl = BeastManager.beastsList;
        for (int x = 0; x < bl.Beasts.Count; x++)
        {
            summonedNames.Add(bl.Beasts[x].name);
            summonedImages.Add(bl.Beasts[x].static_img);
            summoned.Add(bl.Beasts[x]);
            sorted.Add(bl.Beasts[x]);
        }

        SetImages();
    }

    //Fill up the image slots with your summoned beasts
    void SetImages()
    {
        for (int x = 0 + (counter * 6); x < slots.Count + (counter * 6); x++)
        {
            if (sorted.Count >= x + 1)
            {
                slots[x % 6].gameObject.SetActive(true);
                slots[x % 6].sprite = Resources.Load<Sprite>(summonedImages[x]);
                beastTexts[x % 6].GetComponent<Text>().text = summonedNames[x];

                //Make not summoned beasts transparent
                if (sorted[x].tier == 0)
                {
                    var tempColor = slots[x % 6].color;
                    tempColor.a = .5f;
                    slots[x % 6].color = tempColor;
                }
                
                else
                {
                    var tempColor = slots[x % 6].color;
                    tempColor.a = 1f;
                    slots[x % 6].color = tempColor;
                }
                
            }
            else
            {
                slots[x % 6].gameObject.SetActive(false);
                beastTexts[x % 6].GetComponent<Text>().text = "";
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
        if ((counter * 6) + 6 >= sorted.Count)
        {
            forward.SetActive(false);
        }
        else if ((counter * 6) + 6 < sorted.Count)
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

    //Loads animation scene if beast is summoned and summon page if it isn't
    public void OnClick()
    {
        for(int x = 1; x <= poolSlots.Count; x++)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Slot" + x)
            {
                beastName = summonedNames[(counter * 6) + x - 1];
                if (sorted[(counter * 6) + x - 1].tier == 0)
                {
                    SceneManager.LoadScene(beastName + "Page1");
                }
                else
                {
                    SceneManager.LoadScene("BeastView");
                }
            }
        }
    }

    public void SortImagesDropdown(int value)
    {
        if(value == 0)
        {
            SortByTier();
        }
        else if (value == 1)
        {
            SortByType();
        }
        else if (value == 2)
        {
            SortByName();
        }
    }

    void SortByTier()
    {
        sorted.Clear();
        summonedImages.Clear();
        summonedNames.Clear();

        for (int x = 5; x >= 0; x--)
        {
            for (int y = 0; y < summoned.Count; y++)
            {
                if (summoned[y].tier == x)
                {
                    sorted.Add(summoned[y]);
                    summonedImages.Add(summoned[y].static_img);
                    summonedNames.Add(summoned[y].name);
                }
            }
        }
        
        SetImages();
    }

    //Not sure how to do this better...
    void SortByType()
    {
        sorted.Clear();
        summonedImages.Clear();
        summonedNames.Clear();

        for (int x = 0; x <= Enum.GetNames(typeof(Beast.types)).Length; x++)
        {
            for (int y = 0; y < summoned.Count; y++)
            {
                if ((int)summoned[y].type == x) 
                {
                    sorted.Add(summoned[y]);
                    summonedImages.Add(summoned[y].static_img);
                    summonedNames.Add(summoned[y].name);
                }
            }
        }

        SetImages();
    }

    void SortByName()
    {
        sorted.Clear();

        List<string> names = new List<string>();

        foreach(Beast b in summoned)
        {
            names.Add(b.name);
        }

        for(int x = 0; x < names.Count; x++)
        {
            for(int y = 0; y < summoned.Count; y++)
            {
                if(names[x] == summoned[y].name)
                {
                    sorted.Add(summoned[y]);
                }
            }
        }

        summonedImages.Sort();
        summonedNames.Sort();
        
        SetImages();
    }
}
