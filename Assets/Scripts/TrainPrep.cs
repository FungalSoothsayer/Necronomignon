﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script used to show the beast
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
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Static_Images/EmptyRectangle");

        name.text = SummonManager.name;
        GameObject beastPrefab = (GameObject)Instantiate(Resources.Load($"Prefabs/Beasts/{SummonManager.name}"));
        beastPrefab.transform.SetParent(GameObject.Find($"BeastImage").transform);
        beastPrefab.transform.localPosition = new Vector3(0, 0);
        beastPrefab.transform.localRotation = Quaternion.identity;
        beastPrefab.transform.localScale = new Vector3(10, 10);

        Animator animator = beastPrefab.GetComponent<Animator>();
        animator.enabled = false;

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
