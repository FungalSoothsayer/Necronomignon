using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainPrep : MonoBehaviour
{
    public Button img;
    public new Text name;
    public LoadScenes loadScenes;
    public List<Image> pentagrams;

    Beast b;

    // Start is called before the first frame update
    void Start()
    {
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Static_Images/" + BeastManager.getFromNameS(SummonManager.name).static_img);
        name.text = SummonManager.name;

        b = BeastManager.getFromNameS(name.text);

        for(int x = 0; x < b.tier; x++)
        {
            pentagrams[x].gameObject.SetActive(true);
        }
    }

    public void CheckToTrain()
    {

        if (b.tier < 5)
        {
            loadScenes.LoadSelect("BeastQuiz");
        }

    }
}
