using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePoolLoader : MonoBehaviour
{
    public List<Image> slots;

    public List<string> summonedImages = new List<string>();
    public List<Beast> summoned = new List<Beast>();

    public BeastManager beastManager;
    public CreateManager createManager;

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
        for (int x = 0;x < bl.Beasts.Count; x++)
        {
            bl.Beasts[x].setAttacks();
            if(bl.Beasts[x].tier > 0)
            {
                summonedImages.Add(bl.Beasts[x].static_img);
                summoned.Add(bl.Beasts[x]);
            }
        }

        SetImages();
    }

    void Update()
    {
        
    }

    //Fill up the image slots with your summoned beasts
    void SetImages()
    {


        for(int x = 0+ (counter * 9); x < slots.Count + (counter * 9); x++)
        {
            if (summoned.Count >= x+1)
            {
                slots[x%9].gameObject.SetActive(true);
                print(x % 9);
                slots[x%9].sprite = Resources.Load<Sprite>(summonedImages[x]);
            }
            else
            {
                print(x % 9);
                slots[x%9].sprite = Resources.Load<Sprite>("EmptyRectangle");
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
        if (counter + 9 >= summoned.Count)
        {
            forward.SetActive(false);
        }
        else if (counter + 9 < summoned.Count)
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
        if(str == "Forward")
        {
            counter++;
        }
        else if(str == "Back")
        {
            counter--;
        }

        SetImages();
    }

    //When a beast is removed from the grid, place the image pack into the pool
    public void PutImageBack()
    {
        int index = createManager.selectedIndex;

        slots[index].gameObject.SetActive(true);
        slots[index].sprite = Resources.Load<Sprite>(summonedImages[index]);
        
    }
}
