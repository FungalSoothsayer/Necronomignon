using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeastStats : MonoBehaviour
{
    public Slider statBar;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){ } 
    

    public void incrementProgress(int progress) {
        statBar.value += progress;
    }

    public void setValue(int value) {
        statBar.value = value;
    }
}
