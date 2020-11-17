﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SummonBookLoader : MonoBehaviour
{
    public BeastManager beastManager;

    public List<GameObject> beastTexts;
    public List<Image> slots;

    public List<string> summonedNames = new List<string>();
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
            summonedNames.Add(bl.Beasts[x].name);
            summonedImages.Add(bl.Beasts[x].static_img);
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
                slots[x % 6].sprite = Resources.Load<Sprite>(summonedImages[x]);
                beastTexts[x % 6].GetComponent<Text>().text = summonedNames[x];
                if (summoned[x].tier == 0)
                {
                    var tempColor = slots[x].color;
                    tempColor.a = .5f;
                    slots[x].color = tempColor;
                }
            }
            else
            {
                slots[x % 6].sprite = Resources.Load<Sprite>("EmptyRectangle");
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
        if ((counter * 6) + 6 >= summoned.Count)
        {
            forward.SetActive(false);
        }
        else if ((counter * 6) + 6 < summoned.Count)
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

    public void OnClick()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "Slot1")
        {
            string name = summoned[(counter * 6)].name;
            if(summoned[(counter * 6)].tier == 0)
            {
                SceneManager.LoadScene(name + "Main");
            }
            else
            {
                SceneManager.LoadScene(name + "View");
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Slot2")
        {
            string name = summoned[(counter * 6) + 1].name;
            if (summoned[(counter * 6)].tier == 0)
            {
                SceneManager.LoadScene(name + "Main");
            }
            else
            {
                SceneManager.LoadScene(name + "View");
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Slot3")
        {
            string name = summoned[(counter * 6) + 2].name;
            if (summoned[(counter * 6)].tier == 0)
            {
                SceneManager.LoadScene(name + "Main");
            }
            else
            {
                SceneManager.LoadScene(name + "View");
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Slot4")
        {
            string name = summoned[(counter * 6) + 3].name;
            if (summoned[(counter * 6)].tier == 0)
            {
                SceneManager.LoadScene(name + "Main");
            }
            else
            {
                SceneManager.LoadScene(name + "View");
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Slot5")
        {
            string name = summoned[(counter * 6) + 4].name;
            if (summoned[(counter * 6)].tier == 0)
            {
                SceneManager.LoadScene(name + "Main");
            }
            else
            {
                SceneManager.LoadScene(name + "View");
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Slot6")
        {
            string name = summoned[(counter * 6) + 5].name;
            if (summoned[(counter * 6)].tier == 0)
            {
                SceneManager.LoadScene(name + "Main");
            }
            else
            {
                SceneManager.LoadScene(name + "View");
            }
        }
    }
}
