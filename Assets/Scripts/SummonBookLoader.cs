using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonBookLoader : MonoBehaviour
{
    public BeastDatabase beastDatabase;

    public Image gaiaImage;
    public Image cthulhuImage;
    public Image trogdorImage;
    public Image behemothImage;
    public Image naglfarImage;
    public Image sunbatherImage;

    void Start()
    {
        if (beastDatabase.GetStatus("Cthulhu")) cthulhuImage.sprite = Resources.Load<Sprite>("Boss Cthulhu-3");
        else cthulhuImage.sprite = Resources.Load<Sprite>("Boss Cthulhu-1");
    }
}
