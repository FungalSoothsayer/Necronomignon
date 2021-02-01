using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saving : MonoBehaviour
{

    public GameObject loadSaveDialog;
    public GameObject loadSaveText;
    public Text txtLoadSave;

    // Start is called before the first frame update
    void Start()
    {
        //Variables for dialog box when game is loaded or saved
        loadSaveDialog = GameObject.Find("loadSaveDialog");
        loadSaveText = GameObject.Find("loadSaveText");

        txtLoadSave = (Text) loadSaveText.GetComponent(typeof(Text));
        
        if(loadSaveDialog != null)
            loadSaveDialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveAll()
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

        //Dialog box when game is saved
        txtLoadSave.text = "The Game Has Been Saved!";
        loadSaveDialog.SetActive(true);
    }

    public void loadAll()
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

        //Displays dialog box when game is loaded
        txtLoadSave.text = "Your Game Has Been Loaded!";
        loadSaveDialog.SetActive(true);
    }

    public void closeDialog()
    {
        if (loadSaveDialog != null)
            loadSaveDialog.SetActive(false);
    }

}
