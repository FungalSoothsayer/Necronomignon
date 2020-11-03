using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOutput : MonoBehaviour
{
    public Text damageOut;

    public Animator anim;
    
    void Start()
    {
        
    }
    
    // Sets the appropriate Damage Output text, to the damage received.
    public void setText(int damage)
    {
        // initializes Animator component
        anim = GetComponent<Animator>();



            damageOut.gameObject.SetActive(true);

            anim.SetBool("isActive", true);

            damageOut.text = damage.ToString();



        // Checks if the current Animation is running, returns bool 
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("damageOutput"))
        {
            // Anim Goes from Active to Idle 
            anim.SetBool("isActive", false);
            damageOut.gameObject.SetActive(false);
        }



    



    }

   
}
