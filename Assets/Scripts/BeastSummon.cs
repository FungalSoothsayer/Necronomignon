using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeastSummon : MonoBehaviour
{
    public BeastDatabase beastDatabase;

    public bool summoned = false;
    public int rating = 0;

    public Image beastImage;
    public Image statGraph;

    public GameObject postSummonedItems;
    public GameObject graph;
    public GameObject pent1;
    public GameObject pent2;
    public GameObject pent3;
    public GameObject pent4;
    public GameObject pent5;

    void Start()
    {
        //Get the active scene and store it
        Scene scene = SceneManager.GetActiveScene();

        //Store the beast's rating and summoned status that the current scene is for
        rating = GetCurrentBeastRating(scene);
        summoned = GetCurrentBeastStatus(scene);

        ChangeImage(scene);
        ChangeRatingImages();

        //If the beast is summoned, add the graph, train button, and tier text to page
        if (summoned)
        {
            postSummonedItems.SetActive(true);
        }
        else
        {
            postSummonedItems.SetActive(false);
        }
    }

    //Return the rating for the beast that the current scene is for
    int GetCurrentBeastRating(Scene currentScene)
    {
        if (currentScene.name == "CthulhuMain")
        {
            return beastDatabase.GetRating("Cthulhu");
        }
        else
        {
            return 0;
        }
    }

    //Return the summoned status for the beast that the current scene is for
    bool GetCurrentBeastStatus(Scene currentScene)
    {
        if (currentScene.name == "CthulhuMain")
        {
            return beastDatabase.GetStatus("Cthulhu");
        }
        else
        {
            return false;
        }
    }

    //Check what scene it is and then change the beast image according to the summoned status
    void ChangeImage(Scene currentScene)
    {
        if (currentScene.name == "CthulhuMain")
        {
            if (summoned)
            {
                beastImage.sprite = Resources.Load<Sprite>("Boss Cthulhu-3");
                if(rating == 1)
                {
                    statGraph.sprite = Resources.Load<Sprite>("4864465");
                }
                else if(rating == 5)
                {
                    statGraph.sprite = Resources.Load<Sprite>("4872265");
                }
            }
            else
            {
                beastImage.sprite = Resources.Load<Sprite>("Boss Cthulhu-1");
                graph.SetActive(false);
            }
        }
    }

    void ChangeRatingImages()
    {
        if (rating < 1) pent1.SetActive(false);
        if (rating < 2) pent2.SetActive(false);
        if (rating < 3) pent3.SetActive(false);
        if (rating < 4) pent4.SetActive(false);
        if (rating < 5) pent5.SetActive(false);
    }
}
