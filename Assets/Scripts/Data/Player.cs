using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Summoner summoner = new Summoner();
    // Start is called before the first frame update
    void Start()
    {
        print(summoner.getLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
