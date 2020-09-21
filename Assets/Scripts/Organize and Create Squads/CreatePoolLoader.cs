using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePoolLoader : MonoBehaviour
{
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Image slot5;
    public Image slot6;
    public Image slot7;
    public Image slot8;
    public Image slot9;

    public List<string> summonedImages = new List<string>();
    public List<string> summoned = new List<string>();

    public BeastDatabase beastDatabase;
    public CreateManager createManager;

    void Start()
    {
        //Add the image names to a list if they are summoned
        if (beastDatabase.GetStatus("Gaia"))
        {
            summonedImages.Add("Boss Nature Titan Tellia-4");
            summoned.Add("Gaia");
        }
        if (beastDatabase.GetStatus("Cthulhu"))
        {
            summonedImages.Add("Boss Cthulhu-3");
            summoned.Add("Cthulhu");
        }
        if (beastDatabase.GetStatus("Trogdor"))
        {
            summonedImages.Add("Boss Mythical Stag Kyris-3");
            summoned.Add("Trogdor");
        }
        if (beastDatabase.GetStatus("Behemoth"))
        {
            summonedImages.Add("Boss Wolfbull Demon Goliath-4");
            summoned.Add("Behemoth");
        }
        if (beastDatabase.GetStatus("Naglfar"))
        {
            summonedImages.Add("Dragons Hydra-3");
            summoned.Add("Naglfar");
        }
        if (beastDatabase.GetStatus("Sunbather"))
        {
            summonedImages.Add("Boss Darklord Excelsios-1");
            summoned.Add("Sunbather");
        }

        SetImages();
    }

    //Fill up the image slots with your summoned beasts
    void SetImages()
    {
        if(summoned.Count >= 1)
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
        else slot9.sprite = Resources.Load<Sprite>("EmptyRectangle");
    }

    //When a beast is removed from the grid, place the image pack into the pool
    public void PutImageBack()
    {
        switch (createManager.selectedIndex)
        {
            case 0:
                slot1.sprite = Resources.Load<Sprite>(summonedImages[0]);
                break;
            case 1:
                slot2.sprite = Resources.Load<Sprite>(summonedImages[1]);
                break;
            case 2:
                slot3.sprite = Resources.Load<Sprite>(summonedImages[2]);
                break;
            case 3:
                slot4.sprite = Resources.Load<Sprite>(summonedImages[3]);
                break;
            case 4:
                slot5.sprite = Resources.Load<Sprite>(summonedImages[4]);
                break;
            case 5:
                slot6.sprite = Resources.Load<Sprite>(summonedImages[5]);
                break;
            case 6:
                slot7.sprite = Resources.Load<Sprite>(summonedImages[6]);
                break;
            case 7:
                slot8.sprite = Resources.Load<Sprite>(summonedImages[7]);
                break;
            case 8:
                slot9.sprite = Resources.Load<Sprite>(summonedImages[8]);
                break;
        }
    }
}
