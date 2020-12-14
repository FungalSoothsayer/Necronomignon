using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainPrep : MonoBehaviour
{
    public Image img;
    public new Text name;
    // Start is called before the first frame update
    void Start()
    {
        img.sprite = Resources.Load<Sprite>("Static_Images/" + BeastManager.getFromNameS(SummonManager.name).static_img);
        name.text = SummonManager.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
