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

    public static void saveAll()
    {
        EasySave.Save<BeastList>("playerBL",BeastManager.beastsList);
        EasySave.Save<int>("level", LevelChecker.levels);
        if (SquadData.squad1Saved)
        {
            EasySave.Save<List<Beast>>("squad1",SquadData.squad1);
        }
        if (SquadData.squad2Saved)
        {
            EasySave.Save<List<Beast>>("squad2", SquadData.squad2);
        }
        EasySave.Save<int>("playerXP", Player.summoner.xp);
    }
    public static void loadAll()
    {
        BeastManager.beastsList = EasySave.Load<BeastList>("playerBL");
        LevelChecker.levels = EasySave.Load<int>("level");
        if (EasySave.Load<List<Beast>>("squad1") != null && EasySave.Load<List<Beast>>("squad1").Count>0)
        {
            SquadData.squad1 = EasySave.Load<List<Beast>>("squad1");
            SquadData.squad1Saved = true;
        }
        if (EasySave.Load<List<Beast>>("squad2") != null && EasySave.Load<List<Beast>>("squad2").Count > 0)
        {
            SquadData.squad2 = EasySave.Load<List<Beast>>("squad2");
            SquadData.squad2Saved = true;
        }
        Player.summoner.xp = EasySave.Load<int>("playerXP");
    }
}
