using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOutput : MonoBehaviour
{
    public Text damageOut; 

    // Sets the appropriate Damage Output text, to the damage received.
    public void setText(int damage)
    {
        damageOut.text = damage.ToString();
    }
    
}
