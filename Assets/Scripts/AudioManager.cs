using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    //THIS ARRAY HOLDS ALL THE SOUNDS IN THE GAME
    public Sound[] sounds;

    public AudioClip battleTheme;
    public AudioClip mainTheme;

    public String tempName = "";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);


        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.Find("VolumeSlider");
        if (go != null)
        {
            Slider slide = go.GetComponent<Slider>();
            go = GameObject.Find("Music");
            AudioSource[] auso = go.GetComponents<AudioSource>();
            foreach (AudioSource sour in auso)
            {

                sour.volume = slide.value;
            }
        }
    }
    
    public void Play(string name)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        Sound s = Array.Find(sounds, sound => sound.name == name);

        AudioSource audio = objs[0].GetComponent<AudioSource>();

        audio.clip = mainTheme;

        if (name.Equals("Manoli") || name.Equals("RandomFight") || name.Equals("RandomerFight"))
        {
            audio.clip = battleTheme;
            audio.Play();
            tempName = name;

            if(s != null)
            s.source.Play();
        }
        
        else if ((name.Equals("Map") && tempName.Equals("Manoli")) || (name.Equals("Map") && tempName.Equals("RandomFight")) || (name.Equals("Map") && tempName.Equals("RandomerFight")))
        {
            tempName = name;
            audio.Play();
        }

        else
        {
            if (s != null)
            {
                tempName = name;

                s.source.Play();               
            }
        }
    }
    
}
