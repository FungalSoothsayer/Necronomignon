using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This Script will help us handle animation calls and Triggers whenever it is needed. 
 */
public class AnimationPlayer : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image.GetComponent<Animator>().runtimeAnimatorController = Resources.Load
            ("Animations/" + SummonBookLoader.beastName + "/" + SummonBookLoader.beastName + "_Controller") as RuntimeAnimatorController;
    }

    //Front Row Attack
    public void FrontAttack()
    {
       image.GetComponent<Animator>().SetTrigger("Front");
    }

    //Back Row Attack
    public void BackAttack()
    {
        image.GetComponent<Animator>().SetTrigger("Back");
    }

    //When a Beast Receives Damage
    public void Damaged()
    {
        image.GetComponent<Animator>().SetTrigger("GetHit");
    }

    //On Death
    public void Death()
    {
        image.GetComponent<Animator>().SetInteger("Health", 0);
    }

    // When Summoning a new beast into the field, or for the first time 
    public void Summon()
    {
        //Summon animation will go here when we get them
        Beast b;
        image.sprite = Resources.Load<Sprite>("Static_Images/" + SummonBookLoader.beastName);
    }

    //Back Button(obviously) 
    public void BackButton()
    {
        SceneManager.LoadScene("SummonMain");
    }
}
