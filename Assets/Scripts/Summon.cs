using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public BeastDatabase beastDatabase;

    //Changes the status of the desired beast and set rating to 1
    public void SummonBeast(string beast)
    {
        beastDatabase.ChangeStatus(beast, true);
        beastDatabase.ChangeRating(beast, 1);
    }
}
