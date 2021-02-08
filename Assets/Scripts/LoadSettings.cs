﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSettings : MonoBehaviour
{
    public GameObject settingsPrefab;
    public GameObject blurBackground;
    public Dropdown resolutionDropdown;
    public Dropdown fullscreenDropdown;
    public static Button redRoach;
    public Resolution[] resolutions;
    //stores all possible screen resolutions with a 16:9 aspect ratio
    public static List<Resolution> resolutionsList;
    public bool fullscreen = true;
    public bool borderless = true;

    float value = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (blurBackground != null) blurBackground.SetActive(false);

        if (resolutionsList == null){
            resolutionsList = new List<Resolution>();
            getScreenResolutions();
        }
    }

    // Update is called once per frame
    void Update(){}

    public void loadSettings() {
        //if settings screen is instantiated stop function
        GameObject go = GameObject.Find("Music");
        
        if (go != null)
        {
            AudioSource[] auso = go.GetComponents<AudioSource>();
            value = auso[0].volume;
        }
        if (GameObject.Find("SettingsScreen") != null) return;

        blurBackground.SetActive(true);
        
        GameObject parent = GameObject.Find("SettingsHolder"); 
        GameObject child = Instantiate(settingsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
      

        child.name = "SettingsScreen";
        child.transform.SetParent(parent.transform, false);

        resolutionDropdown = GameObject.Find("resolutionDropdown").GetComponent<Dropdown>();
        fullscreenDropdown = GameObject.Find("fullscreenDropdown").GetComponent<Dropdown>();

        setOnClickFunctions();
        setVolumeSlider();
        getFullscreenOption();
        setResolutionsDropdown();
        setRedRoachAsset();
    }
    
    public void setOnClickFunctions()
    {
        Button CloseSettings = (Button) GameObject.Find("closeSettingsBtn").GetComponent<Button>();
        Button saveBtn = (Button) GameObject.Find("SaveBtn").GetComponent<Button>();
        Button loadBtn = (Button) GameObject.Find("LoadBtn").GetComponent<Button>();
        redRoach = (Button) GameObject.Find("Red Roach").GetComponent<Button>();

        CloseSettings.onClick.AddListener(closeSettings);
        saveBtn.onClick.AddListener(Saving.saveAll);
        loadBtn.onClick.AddListener(Saving.loadAll);
        resolutionDropdown.onValueChanged.AddListener(delegate { onResolutionChanged(); });
        fullscreenDropdown.onValueChanged.AddListener(delegate { onFullscreenChanged(); });
        redRoach.onClick.AddListener(Player.activeRedRoach);
    }

    public static void setRedRoachAsset() {
        Sprite temp = Resources.Load<Sprite>("Assets/" + (Player.RedRoach? "clicked_button" : "button"));
        redRoach.image.sprite = temp;
        Debug.Log("sprite " + temp.name);
    }

    public void setVolumeSlider() {
        GameObject go = GameObject.Find("VolumeSlider");
        
        if (go != null)
        {
            Slider slide = go.GetComponent<Slider>();
            slide.value = value;
            go = GameObject.Find("Music");
            AudioSource[] auso = go.GetComponents<AudioSource>();
            auso[auso.Length - 1].volume = slide.value;
        }
    }

    /*
        Gets all supported screen resolutions with a 16:9 aspect ratio and stores them
    */
    public void getScreenResolutions() {
        //16:9 ratio
        double defaultRatio = Math.Truncate(((double)9 / 16)*1000);
        resolutions = Screen.resolutions;
        
        foreach (Resolution res in 
            resolutions){
            double currRatio = Math.Truncate(((double)res.height / res.width)*1000);

            if (currRatio == defaultRatio)
                resolutionsList.Add(res);
        }
    }

    public void setResolutionsDropdown() {
        if (fullscreen) resolutionDropdown.interactable = false;

        resolutionDropdown.options.Clear();
        
        for(int i = 0; i < resolutionsList.Count; i++){
            String temp = resolutionsList[i].width + " x " + resolutionsList[i].height;

            resolutionDropdown.options.Add(new Dropdown.OptionData() { text = temp });

            Debug.Log("curr " + Screen.currentResolution.ToString()) ;
            Debug.Log(resolutionsList[i].ToString()) ;

            if (resolutionsList[i].Equals(Screen.currentResolution))
                resolutionDropdown.value = i;
        }

    }   
    
    public void getFullscreenOption() {
        switch (fullscreenDropdown.value) {
                //fullscreen
            case 0: fullscreen = true;
                break;
                //windowed
            case 1: fullscreen = false; borderless = false;
                break;
                //windowed borderless
            case 2: fullscreen = false; borderless = true;
                break;
        }
    }

    public void closeSettings()
    {
        GameObject settings = GameObject.Find("SettingsScreen");

        //if settings screen is not instantiated stop function
        if (settings == null) return;

        blurBackground.SetActive(false);

        Destroy(settings);
    }


    public void onFullscreenChanged() {
        getFullscreenOption();

        //toggles fullscreen mode not sure if it works tho
        Screen.fullScreen = (fullscreen) ? Screen.fullScreen : !Screen.fullScreen;

        resolutionDropdown.interactable = fullscreen? false : true;    
    }

    public void onResolutionChanged() {
        Resolution temp = resolutionsList[resolutionDropdown.value];

        Screen.SetResolution(temp.width, temp.height, borderless ? FullScreenMode.MaximizedWindow : FullScreenMode.Windowed);
    }
}
