using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider statBar;

    // Start is called before the first frame update
    void Start(){
        statBar.maxValue = 200;
    }

    // Update is called once per frame
    void Update(){ } 
    

    public void incrementProgress(int progress) {
        statBar.value += progress;
    }
    
    public int Value {
        set => statBar.value = value;
    }

    public int MaxValue
    {
        set => statBar.maxValue = value;
    }
    
    //public void setValue(int value) {
    //    statBar.value = value;
    //}

    //public void setMaxValue(int val) {
    //    statBar.maxValue = val;
    //}
}
