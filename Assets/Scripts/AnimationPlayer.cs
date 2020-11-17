using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPlayer : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image.GetComponent<Animator>().runtimeAnimatorController = Resources.Load
            ("Animations/" + SummonBookLoader.beastName + "/" + SummonBookLoader.beastName + "_Controller") as RuntimeAnimatorController;
    }

    public void FrontAttack()
    {
       image.GetComponent<Animator>().SetTrigger("Front");
    }

    public void BackAttack()
    {
        image.GetComponent<Animator>().SetTrigger("Back");
    }

    public void Damaged()
    {
        image.GetComponent<Animator>().SetTrigger("GetHit");
    }

    public void Death()
    {
        image.GetComponent<Animator>().SetInteger("Health", 0);
    }

    public void Summon()
    {

    }
}
