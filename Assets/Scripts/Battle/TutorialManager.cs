using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script manages the whole tutorial. Each tutorial scene is hard coded in order
//to keep the whole battle algorithm intact

public class TutorialManager : MonoBehaviour
{
    Scene scene;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Tutorial3")
        {
            StartCoroutine(Tutorial3Start());
        }
    }

    public void Tutorial1Click()
    {
        Debug.Log("Behemoth does Bite Attack.");
        StartCoroutine(WaitAndLoad("Tutorial2"));
    }

    int tutorial2Clicks = 0;

    public void Tutorial2Click1()
    {
        Debug.Log("Behemoth does fireball attack.");
        GameObject.Find("btnKnight").SetActive(false);
        tutorial2Clicks += 1;
        if (tutorial2Clicks == 2) StartCoroutine(WaitAndLoad("Tutorial3"));
    }

    public void Tutorial2Click2()
    {
        Debug.Log("Trogdor does melee attack.");
        GameObject.Find("btnPawn").SetActive(false);
        tutorial2Clicks += 1;
        if (tutorial2Clicks == 2) StartCoroutine(WaitAndLoad("Tutorial3"));
    }

    public void Tutorial3Click()
    {
        Debug.Log("Air Spector does powerful attack!");
        StartCoroutine(WaitAndLoad("Tutorial4"));
    }

    IEnumerator Tutorial3Start()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Rook melee attacked Behemoth.");
        GameObject.Find("btnPawn").GetComponent<Button>().interactable = true;
        GameObject.Find("btnKnight").GetComponent<Button>().interactable = true;
        GameObject.Find("btnRook").GetComponent<Button>().interactable = true;
    }

    public void Tutorial4Click()
    {
        Debug.Log("Gaia uses healing powers to heal it's teammates!");
        StartCoroutine(WaitAndLoad("TutorialEnd"));
    }

    IEnumerator WaitAndLoad(string nextScene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }
}
