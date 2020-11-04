using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPlayer : MonoBehaviour
{
    Animator anim;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        anim = image.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("Animations/Wyvern/Idle/Idle") as RuntimeAnimatorController;
    }

    public void FrontRowAnimation()
    {
        anim = image.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("Animations/Wyvern/Front/Front") as RuntimeAnimatorController;
    }

    public void BackRowAnimation()
    {
        anim = image.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("Animations/Wyvern/Back/Back") as RuntimeAnimatorController;
    }

    public void DamagedAnimation()
    {
        anim = image.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("Animations/Wyvern/Damage/Damage") as RuntimeAnimatorController;
    }

    public void DeathAnimation()
    {
        anim = image.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("Animations/Wyvern/Death/Death") as RuntimeAnimatorController;
    }

    public void SpawnAnimation()
    {

    }
}
