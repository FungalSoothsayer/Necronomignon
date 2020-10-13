using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonBookLoader : MonoBehaviour
{
    public BeastManager beastManager;

    public Image gaiaImage;
    public Image cthulhuImage;
    public Image trogdorImage;
    public Image behemothImage;
    public Image naglfarImage;
    public Image sunbatherImage;

    void Start()
    {
        if (beastManager.getFromName("Cthulhu").tier > 0) cthulhuImage.sprite = Resources.Load<Sprite>("Boss Cthulhu-3");
        else cthulhuImage.sprite = Resources.Load<Sprite>("Boss Cthulhu-1");
    }
}
