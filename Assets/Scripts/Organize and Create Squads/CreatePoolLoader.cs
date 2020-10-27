using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePoolLoader : MonoBehaviour
{
    public List<Image> slots;

    public List<string> summonedImages = new List<string>();
    public List<Beast> summoned = new List<Beast>();

    public BeastDatabase beastDatabase;
    public BeastManager beastManager;
    public CreateManager createManager;

    void Start()
    {
        if (!beastManager.isLoaded())
        {
            beastManager.Awake();
        }
        BeastList bl = beastManager.beastsList;
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

    //Fill up the image slots with your summoned beasts
    void SetImages()
    {
        for(int x = 0; x < slots.Count; x++)
        {
            if (summoned.Count >= x+1)
            {
                slots[x].sprite = Resources.Load<Sprite>(summonedImages[x]);
            }
            else
            {
                slots[x].sprite = Resources.Load<Sprite>("EmptyRectangle");
            }
        }
        /*if(summoned.Count >= 1)
        {
            slot1.sprite = Resources.Load<Sprite>(summonedImages[0]);
        }
        else slot1.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 2)
        {
            slot2.sprite = Resources.Load<Sprite>(summonedImages[1]);
        }
        else slot2.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 3)
        {
            slot3.sprite = Resources.Load<Sprite>(summonedImages[2]);
        }
        else slot3.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 4)
        {
            slot4.sprite = Resources.Load<Sprite>(summonedImages[3]);
        }
        else slot4.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 5)
        {
            slot5.sprite = Resources.Load<Sprite>(summonedImages[4]);
        }
        else slot5.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 6)
        {
            slot6.sprite = Resources.Load<Sprite>(summonedImages[5]);
        }
        else slot6.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 7)
        {
            slot7.sprite = Resources.Load<Sprite>(summonedImages[6]);
        }
        else slot7.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 8)
        {
            slot8.sprite = Resources.Load<Sprite>(summonedImages[7]);
        }
        else slot8.sprite = Resources.Load<Sprite>("EmptyRectangle");

        if (summoned.Count >= 9)
        {
            slot9.sprite = Resources.Load<Sprite>(summonedImages[8]);
        }
        else slot9.sprite = Resources.Load<Sprite>("EmptyRectangle");*/
    }

    //When a beast is removed from the grid, place the image pack into the pool
    public void PutImageBack()
    {
        int index = createManager.selectedIndex;

        slots[index].gameObject.SetActive(true);
        slots[index].sprite = Resources.Load<Sprite>(summonedImages[index]);
        /*switch (createManager.selectedIndex)
        {
            case 0:
                slot1.gameObject.SetActive(true);
                slot1.sprite = Resources.Load<Sprite>(summonedImages[0]);
                break;
            case 1:
                slot2.gameObject.SetActive(true);
                slot2.sprite = Resources.Load<Sprite>(summonedImages[1]);
                break;
            case 2:
                slot3.gameObject.SetActive(true);
                slot3.sprite = Resources.Load<Sprite>(summonedImages[2]);
                break;
            case 3:
                slot4.gameObject.SetActive(true);
                slot4.sprite = Resources.Load<Sprite>(summonedImages[3]);
                break;
            case 4:
                slot5.gameObject.SetActive(true);
                slot5.sprite = Resources.Load<Sprite>(summonedImages[4]);
                break;
            case 5:
                slot6.gameObject.SetActive(true);
                slot6.sprite = Resources.Load<Sprite>(summonedImages[5]);
                break;
            case 6:
                slot7.gameObject.SetActive(true);
                slot7.sprite = Resources.Load<Sprite>(summonedImages[6]);
                break;
            case 7:
                slot8.gameObject.SetActive(true);
                slot8.sprite = Resources.Load<Sprite>(summonedImages[7]);
                break;
            case 8:
                slot9.gameObject.SetActive(true);
                slot9.sprite = Resources.Load<Sprite>(summonedImages[8]);
                break;
        }*/
    }
}
