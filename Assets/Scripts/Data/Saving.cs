using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveAll()
    {
        EasySave.Save<BeastList>("playerBL",BeastManager.beastsList);
        EasySave.Save<int>("level", LevelChecker.levels);
    }
    public void loadAll()
    {
        BeastManager.beastsList = EasySave.Load<BeastList>("playerBL");
        LevelChecker.levels = EasySave.Load<int>("level");
    }
}
