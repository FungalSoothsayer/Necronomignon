using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainPrep : MonoBehaviour
{
    public Button img;
    public new Text name;
    public LoadScenes loadScenes;
    // Start is called before the first frame update
    void Start()
    {
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Static_Images/" + BeastManager.getFromNameS(SummonManager.name).static_img);
        name.text = SummonManager.name;
    }

    public void CheckToTrain()
    {
        Beast b = BeastManager.getFromNameS(name.text);
        if(b.tier < 5)
        {
            loadScenes.LoadSelect("BeastQuiz");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
