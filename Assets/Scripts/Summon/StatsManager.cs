using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public List<BeastStats> statsList;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        statsList[1].incrementProgress(20);
        //Scene scene = SceneManager.GetActiveScene();
        //while (BeastSummon.beastName == null) { Debug.Log("no beast"); }
        Debug.Log("hi" + BeastSummon.currentBeast.name);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
