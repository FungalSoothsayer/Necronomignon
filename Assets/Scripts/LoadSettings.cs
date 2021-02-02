using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSettings : MonoBehaviour
{
    public GameObject settingsPrefab;
    float value = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadSettings() {
        //if settings screen is instantiated stop function
        GameObject go = GameObject.Find("Music");
        
        if (go != null)
        {
            AudioSource[] auso = go.GetComponents<AudioSource>();
            value = auso[0].volume;
        }
        if (GameObject.Find("SettingsScreen") != null) return;
        
        GameObject parent = GameObject.Find("SettingsHolder"); 
        GameObject child = Instantiate(settingsPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        child.name = "SettingsScreen";
        child.transform.SetParent(parent.transform, false);

        setFunctions_OnClick();
        setVolumeSlider();
    }

    public void closeSettings() {
        GameObject settings = GameObject.Find("SettingsScreen");

        //if settings screen is not instantiated stop function
        if (settings == null)return;
        
        Destroy(settings);
    }

    public void setFunctions_OnClick()
    {
        Button CloseSettings = (Button) GameObject.Find("closeSettingsBtn").GetComponent<Button>();
        Button saveBtn = (Button) GameObject.Find("SaveBtn").GetComponent<Button>();
        Button loadBtn = (Button) GameObject.Find("LoadBtn").GetComponent<Button>();


        CloseSettings.onClick.AddListener(closeSettings);
        saveBtn.onClick.AddListener(Saving.saveAll);
        loadBtn.onClick.AddListener(Saving.loadAll);
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
}
